
#include "TextureMacros.fxh"

float3 ambientColor;

DECLARE_TEXTURE(light,0);
DECLARE_TEXTURE(extra,1);
DECLARE_TEXTURE(color,2);

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0_centroid;
};

float2 halfPixel;
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord ;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
	float4 extrav =  SAMPLE_TEXTURE(extra,input.TexCoord).rgba;
	int procces = round(extrav.a * 255 );
	float3 diffuseColor = SAMPLE_TEXTURE(color,input.TexCoord).rgb;	
	
	bool DoNotIlluminate = fmod(procces, 2) == 1; 
	bool isBackGround = fmod(procces, 4) >= 2; 
	bool isAmbienteCubeMap = fmod(procces, 16) >= 8; 

	
	if(DoNotIlluminate  || isBackGround)
	{
		return float4(diffuseColor,1);
	}
	else if(isAmbienteCubeMap)
	{
		float4 l4 = SAMPLE_TEXTURE(light,input.TexCoord);		
		float3 diffuseLight = l4.rgb;
		float specularLight = l4.a;		
		return float4( extrav.rgb + (diffuseColor * (diffuseLight)+ specularLight),1);
	}
	else	
	{		
		float4 l4 = SAMPLE_TEXTURE(light,input.TexCoord);		
		float3 diffuseLight = l4.rgb;
		float specularLight = l4.a;
		return float4(ambientColor + (diffuseColor * (diffuseLight)+ specularLight),1);
    }
    
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionNormal();
    }
}


