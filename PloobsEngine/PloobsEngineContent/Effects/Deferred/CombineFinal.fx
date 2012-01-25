texture colorMap;
texture lightMap;
texture EXTRA1;
float3 ambientColor;

sampler lightSampler = sampler_state
{
    Texture = (lightMap);
	AddressU = CLAMP;
    AddressV = CLAMP;	
};

sampler extraSampler = sampler_state
{
    Texture = (EXTRA1);
    AddressU = CLAMP;
    AddressV = CLAMP;
    
};

sampler colorSampler = sampler_state
{
    Texture = (colorMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
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
	float4 extra =  tex2D(extraSampler,input.TexCoord).rgba;
	int procces = round(extra.a * 255 );
	float3 diffuseColor = tex2D(colorSampler,input.TexCoord).rgb;	
	
	bool DoNotIlluminate = fmod(procces, 2) == 1; 
	bool isBackGround = fmod(procces, 4) >= 2; 
	bool isAmbienteCubeMap = fmod(procces, 16) >= 8; 

	
	if(DoNotIlluminate  || isBackGround)
	{
		return float4(diffuseColor,1);
	}
	else if(isAmbienteCubeMap)
	{
		float4 light = tex2D(lightSampler,input.TexCoord);		
		float3 diffuseLight = light.rgb;
		float specularLight = light.a;
		return float4((diffuseColor * (diffuseLight + extra.rgb)+ specularLight),1);
	}
	else	
	{		
		float4 light = tex2D(lightSampler,input.TexCoord);		
		float3 diffuseLight = light.rgb;
		float specularLight = light.a;
		return float4((diffuseColor * (diffuseLight + ambientColor)+ specularLight),1);
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


