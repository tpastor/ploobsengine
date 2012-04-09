sampler g_buffer_norm : register(s0);
sampler depthSampler : register(s1);
sampler g_random : register(s2);

float random_size;
float g_sample_rad;
float g_intensity;
float g_scale;
float g_bias;
float2 g_screen_size;
matrix invertViewProj;
matrix View;


struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 uv : TEXCOORD0;    
};

float2 halfPixel;
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{    
	VertexShaderOutput output;
	

	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
	
    //align texture coordinates
    output.uv = input.TexCoord ;
    return output;
}


float3 getPosition(in float2 uv)
{
	float depth = 1 - tex2D(depthSampler, uv).r;

	// Convert position to world space
	float4 position;

	position.xy = uv.x * 2.0f - 1.0f;
	position.y = -(uv.y * 2.0f - 1.0f);
	position.z = depth;
	position.w = 1.0f;

	position = mul(position, invertViewProj);
	position /= position.w;
	return position;
}

float3 getNormal(in float2 uv)
{
	float3 norm = normalize(tex2D(g_buffer_norm, uv).xyz * 2.0f - 1.0f);
	return mul(norm,View);
}

float2 getRandom(in float2 uv)
{
return normalize(tex2D(g_random, g_screen_size *  uv / random_size).xy * 2.0f - 1.0f);
}

float doAmbientOcclusion(in float2 tcoord,in float2 uv, in float3 p, in float3 cnorm)
{
float3 diff = getPosition(tcoord + uv) - p;
const float3 v = normalize(diff);
const float d = length(diff)*g_scale;
return max(0.0,dot(cnorm,v)-g_bias)*(1.0/(1.0+d))*g_intensity;
}

float4 main(VertexShaderOutput i) : COLOR0
{	
	const float2 vec[4] = {float2(1,0),float2(-1,0),
							float2(0,1),float2(0,-1)};

	float3 p = getPosition(i.uv);
	float3 n = getNormal(i.uv);	
	float2 rand = getRandom(i.uv);

	float ao = 0.0f;
	float rad = g_sample_rad/p.z;

	//**SSAO Calculation**//
	int iterations = 4;
	for (int j = 0; j < iterations; ++j)
	{
	  float2 coord1 = reflect(vec[j],rand)*rad;
	  float2 coord2 = float2(coord1.x*0.707 - coord1.y*0.707,
							  coord1.x*0.707 + coord1.y*0.707);
  
	  ao += doAmbientOcclusion(i.uv,coord1*0.25, p, n);
	  ao += doAmbientOcclusion(i.uv,coord2*0.5, p, n);
	  ao += doAmbientOcclusion(i.uv,coord1*0.75, p, n);
	  ao += doAmbientOcclusion(i.uv,coord2, p, n);
	}
	ao/=(float)iterations;
	//**END**//
	return 1 - ao;
}


technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 main();
    }
}