//WIP http://developer.download.nvidia.com/presentations/2010/gdc/Direct3D_Effects.pdf
float3 forward;
float3 upVector;
float4x4 xView;
float4x4 invxView;
float4x4 xProjection;
float scaleX = 1;
float scaleY = 1;
float sphereRadius = 1;

struct BBVertexToPixel
{
    float4 Position : POSITION;
    float2 TexCoord    : TEXCOORD0;    
	float4 pv : TEXCOORD1;
};

//------- Technique: CylBillboard --------
BBVertexToPixel CylBillboardVS(float4 inPos: POSITION0, float2 inTexCoord: TEXCOORD0 )
{
    BBVertexToPixel Output = (BBVertexToPixel)0;

    float3 upVectorN = normalize(upVector);        
	float3 right = cross(upVectorN,forward);		
	float3 sideVector= normalize(right);    

    float3 finalPosition = inPos;
    finalPosition += (inTexCoord.x - 0.5f)*sideVector * scaleX;
    finalPosition += (0.5f - inTexCoord.y)*upVector * scaleY;   
    
    float4 finalPosition4 = float4(finalPosition, 1);
    
    float4x4 preViewProjection = mul (xView, xProjection);
    Output.Position = mul(finalPosition4, preViewProjection);    
    
	Output.pv = mul(finalPosition4, xView);    
    Output.TexCoord.x = inTexCoord.x ;    
    Output.TexCoord.y =  inTexCoord.y;
    
    return Output;
}

float4 particleSpherePS(
	float2 texCoord      : TEXCOORD0,
	float3 eyeSpacePos   : TEXCOORD1	
	) : COLOR0
{			
			// calculate eye-space sphere normal from texture coordinates
			float3 N;
			N.xy = texCoord*2.0-1.0;
			float r2 = dot(N.xy, N.xy);
			if (r2 > 1.0) discard;   // kill pixels outside circle
			N.z = -sqrt(1.0 - r2);
			// calculate depth
			float4 pixelPos = float4(eyeSpacePos + N*sphereRadius, 1.0);
			float4 clipSpacePos = mul(pixelPos, xProjection);
			float fragDepth = clipSpacePos.z / clipSpacePos.w;
			return float4(fragDepth ,fragDepth ,fragDepth,1);
			//return float4(pixelPos.z,pixelPos.z,pixelPos.z,1);
			//return float4(1,0,0,1);
			//return max(0.0, dot(N, float3(-1,-1,-1) )) * float4(0,0.3f,0.5f,1);
			//OUT.fragColor = diffuse * color;
			//return OUT;
}

technique FLUID0
{
    pass Pass0
    {        
        VertexShader = compile vs_3_0 CylBillboardVS();
        PixelShader = compile ps_3_0 particleSpherePS();        
    }
}

/////////////////////////

texture depth;
sampler depthSampler = sampler_state
{
   Texture = <depth>;
   MinFilter = POINT;
   MagFilter = POINT;
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

texture normal;
sampler normalSampler = sampler_state
{
   Texture = <normal>;
   MinFilter = LINEAR;
   MagFilter = LINEAR;
   MipFilter = LINEAR;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};



float2 halfPixel;
matrix InvertViewProjection;

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 texCoord : TEXCOORD0;  
};

VertexShaderOutput VShader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
	Pos.x =  Pos.x - 2*halfPixel.x;
	Pos.y =  Pos.y + 2*halfPixel.y;
    output.Position = float4(Pos);    
    output.texCoord = Tex;    
    return output;
}

float4 Toeye(float2 textcoord)
{	
		float depthVal = tex2D(depthSampler,textcoord).r;

		//compute screen-space position
		float4 position;
		position.x = textcoord.x * 2.0f - 1.0f;
		position.y = -(textcoord.y * 2.0f - 1.0f);
		position.z = depthVal;
		position.w = 1.0f;		
		//transform to world space
		position = mul(position, InvertViewProjection);
		position /= position.w;    
		return position;
}

float4 Pshader(VertexShaderOutput input) : COLOR
{	    
	
		// read eye-space depth from texture
		float depth = tex2D(depthSampler, input.texCoord).x;
		clip(depth - 0.00001f);
		//return float4(depth,depth,depth,1);
		//if (depth == 1) {
		//discard;
		//return float4(0,0,0,1);
		//}
		// calculate eye-space position from depth
		float3 posEye = Toeye(input.texCoord);
		// calculate differences
		float3 ddx = Toeye(input.texCoord + float2(halfPixel.x, 0)* 2) - posEye;
		float3 ddx2 = posEye - Toeye(input.texCoord + float2(-halfPixel.x, 0) * 2);
		if (abs(ddx.z) > abs(ddx2.z)) {
		ddx = ddx2;
		}
		float3 ddy = Toeye(input.texCoord + float2(0, halfPixel.y) * 2) - posEye;
		float3 ddy2 = posEye - Toeye(input.texCoord + float2(0, -halfPixel.y) * 2);
		if (abs(ddy2.z) < abs(ddy.z)) {
		ddy = ddy2;
		}
		// calculate normal
		float3 n = cross(ddx, ddy);
		n = normalize(n);		
		
		//color = (color* 2 - 1);
		//n = max(0.0, dot(n, float4(-1,-1,-1,1) )) * float4(0,0.3f,0.5f,1);
		//n.w = 1;
		//return float4(n,1);
				
		return float4((n + 1) * 0.5f,1);
}

technique FLUID1
{
	pass P0
	{	
		VertexShader = compile vs_3_0 VShader();
		PixelShader = compile ps_3_0 Pshader();
	}
}


/////////////////////////////////// bilateral

float blurDepthFalloff;
float blurScale;
float2 blurDir;

float4 blshader(VertexShaderOutput input) : Color0
{		
		float depth = tex2D(depthSampler, input.texCoord).x;
		clip(depth - 0.00001f);
		float sum = 0;
		float wsum = 0;
		for(float x=-16; x<=16; x+=1.0) {
		float sample = tex2D(depthSampler, input.texCoord + x*blurDir).x;
		// spatial domain
		float r = x * blurScale;
		float w = exp(-r*r);
		// range domain
		float r2 = (sample - depth) * blurDepthFalloff;
		float g = exp(-r2*r2);
		sum += sample * w * g;
		wsum += w * g;
		}
		if (wsum > 0.0) {
		sum /= wsum;
		}		
		return float4(sum,sum,sum,1);
}

#define RADIUS  15
#define KERNEL_SIZE (RADIUS * 2 + 1)
float weights[KERNEL_SIZE];
float2 offsets[KERNEL_SIZE];

float4 PS_GaussianBlur(float2 texCoord : TEXCOORD0) : COLOR0
{
    float color = 0;
    float depth = tex2D(depthSampler, texCoord).x;					
	float s=0;
    for (int i = 0; i < KERNEL_SIZE; ++i)
	{
		float d = tex2D(depthSampler, texCoord + offsets[i] ).x;        		
		float r2 = abs(depth - d) * blurDepthFalloff;
		float g = exp(-r2*r2);
		color +=  d* weights[i] * g;		
		s+=g* weights[i];
	}    
	color = color/s;
	return float4(color,1,1,1);	
    //return color;
}

technique GAUSS
{
    pass p0
    {     
		VertexShader = compile vs_3_0 VShader();
        PixelShader = compile ps_3_0 PS_GaussianBlur();     
    }
}


technique FLUID2
{
    pass p0
    {     
		VertexShader = compile vs_3_0 VShader();
        PixelShader = compile ps_3_0 blshader();     
    }
}

//////////////////////////////////////

float3 camPos;
struct PS_OUTPUT {
	float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Dep : COLOR2;
    float4 EXTRA1 : COLOR3;
	float Depth : DEPTH0;
};


texture Cubemap;
sampler SamplerCubemap = sampler_state
{
	Texture = <Cubemap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
};

PS_OUTPUT Pshader2(VertexShaderOutput input) 
{	    
	PS_OUTPUT o = (PS_OUTPUT) 0;	
	float depth = tex2D(depthSampler, input.texCoord).x;
	clip(depth - 0.00001f);
	
	float3 posEye = Toeye(input.texCoord);
	float3 V = posEye - camPos;

	float3 normal = tex2D(normalSampler, input.texCoord);
	normal = mul(normal,invxView);

	o.Color = float4(0,1,0,1);
	o.Depth = depth;
	o.Dep = depth;
	o.Normal = float4(normal,60);
	o.EXTRA1 = 0;
	return o;
}

technique FLUID3
{
    pass p0
    {     
		VertexShader = compile vs_3_0 VShader();
        PixelShader = compile ps_3_0 Pshader2();     
    }
}