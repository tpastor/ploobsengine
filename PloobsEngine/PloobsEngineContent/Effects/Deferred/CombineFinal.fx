texture colorMap;
texture lightMap;
texture EXTRA1;
float3 ambientColor;

sampler extraSampler = sampler_state
{
    Texture = (EXTRA1);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = ANISOTROPIC;
    MinFilter = ANISOTROPIC;
    Mipfilter = POINT;
};

sampler colorSampler = sampler_state
{
    Texture = (colorMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = ANISOTROPIC;
    MinFilter = ANISOTROPIC;
    Mipfilter = LINEAR;
};

sampler lightSampler = sampler_state
{
    Texture = (lightMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = ANISOTROPIC;
    MinFilter = ANISOTROPIC;
    Mipfilter = LINEAR;
};

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

float2 halfPixel;
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord;// - halfPixel;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	float procces = tex2D(extraSampler,input.TexCoord).a;
	float3 diffuseColor = tex2D(colorSampler,input.TexCoord).rgba;	
	if(procces > 0.9)
	{
		return float4(diffuseColor,0);
	}
	else	
	{		
		float4 light = tex2D(lightSampler,input.TexCoord + 2* halfPixel);		
		float3 diffuseLight = light.rgb;
		float specularLight = light.a;
		return float4((diffuseColor * (diffuseLight + ambientColor)+ specularLight),0);
    }
    
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}


