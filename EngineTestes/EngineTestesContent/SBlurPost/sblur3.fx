#define RADIUS  3
#define KERNEL_SIZE (RADIUS * 2 + 1)
float weights[KERNEL_SIZE];
float2 offsets[KERNEL_SIZE];

float2 GBufferPixelSize;
float2 TempBufferRes;
float blurDepthFalloff;

sampler2D depthSampler : register(s0);
sampler2D ssaoSampler : register(s1);

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 TexCoord : TEXCOORD0;	
};

VertexShaderOutput VertexShaderBlur(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord.xy = input.TexCoord + GBufferPixelSize;
	output.TexCoord.zw = input.TexCoord + 0.5f / TempBufferRes;
	return output;
}

/////////////////////////////////// bilateral

float4 PS_GaussianBlurTriple(float4 texCoord : TEXCOORD0) : COLOR0
{
    float3 color = 0;

    float depth = tex2D(depthSampler, texCoord.xy).x;	
						
	float s=0;
    for (int i = 0; i < KERNEL_SIZE; ++i)
	{
		float3 im = tex2D(ssaoSampler, texCoord.zw + offsets[i] );
		float d = tex2D(depthSampler, texCoord.xy + offsets[i] ).x;        		
		float r2 = abs(depth - d) * blurDepthFalloff;
		float g = exp(-r2*r2);
		color +=  im* weights[i] * g;		
		s+=g* weights[i];
	}    
	color = color/s;
	return float4(color,1);	    
}

technique GAUSSTriple
{
    pass p0
    {     
		VertexShader = compile vs_3_0 VertexShaderBlur();
        PixelShader = compile ps_3_0 PS_GaussianBlurTriple();     
    }
}


//////////////////////////////////////

float4 PS_GaussianBlurSingle(float4 texCoord : TEXCOORD0) : COLOR0
{
    float color = 0;
    float depth = tex2D(depthSampler, texCoord.xy).x;	
					
	float s=0;
    for (int i = 0; i < KERNEL_SIZE; ++i)
	{
		float im = tex2D(ssaoSampler, texCoord.zw + offsets[i] ).x;
		float d = tex2D(depthSampler, texCoord.xy + offsets[i] ).x;        		
		float r2 = abs(depth - d) * blurDepthFalloff;
		float g = exp(-r2*r2);
		color +=  im* weights[i] * g;		
		s+=g* weights[i];
	}    
	color = color/s;
	return float4(color,0,0,1);	    
}

technique GAUSSSingle
{
    pass p0
    {     
		VertexShader = compile vs_3_0 VertexShaderBlur();
        PixelShader = compile ps_3_0 PS_GaussianBlurSingle();     
    }
}
