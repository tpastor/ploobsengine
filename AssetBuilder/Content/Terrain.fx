float4x4 World;
float4x4 ViewProjection;
float id;
float3 color;
float blendDistance = 0.99f;
float blendWidth = 0.009f;
float nivelBaseAltura;
float nivelBaseEspalhamento;
float nivelBaixoAltura;
float nivelBaixoEspalhamento;
float nivelMedioAltura;
float nivelMedioEspalhamento;
float nivelAltoAltura;
float nivelAltoEspalhamento;
float nearTextureEspalhamento;
float farTextureEspalhamento;

texture NivelAlto;
sampler NivelAltoSampler = sampler_state
{
    Texture = (NivelAlto);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};
texture NivelMedio;
sampler NivelMedioSampler = sampler_state
{
    Texture = (NivelMedio);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};
texture NivelBaixo;
sampler NivelBaixoSampler = sampler_state
{
    Texture = (NivelBaixo);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};


texture BaseTexture;
sampler diffuseSampler = sampler_state
{
    Texture = (BaseTexture);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4  Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2;     
    float3 InPos : TEXCOORD3;     
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    float4 worldPosition = mul(input.Position, World);    
    output.Position = mul(worldPosition, ViewProjection);
    output.TexCoord = input.TexCoord;                             //pass the texture coordinates further
    output.Normal =mul(input.Normal,(float3x3)World);                       //get normal into world space    
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;
    output.InPos = input.Position;
    return output;    
    
}
struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 Extra1 : COLOR3;
};


PixelShaderOutput PixelShaderFunctionMulti(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Depth = 1-input.Depth.x / input.Depth.y;                                  //output Depth          
    
    
    float p1 = clamp(1 - abs(input.InPos.y  - nivelBaixoAltura)/nivelBaixoEspalhamento,  0,1);
    float p2 = clamp(1 - abs(input.InPos.y  - nivelMedioAltura)/nivelMedioEspalhamento, 0,1);
    float p3 = clamp(1 - abs(input.InPos.y  - nivelBaseAltura)/nivelBaseEspalhamento, 0,1);
    float p4 = clamp(1 - abs(input.InPos.y  - nivelAltoAltura)/nivelAltoEspalhamento, 0,1);
    
    float total = p1 + p2 + p3 +p4;
    p1 /= total;
    p2 /= total;
    p3 /= total;
    p4 /= total;
    
    float4 farColor;    
    farColor = tex2D(diffuseSampler, input.TexCoord / farTextureEspalhamento) * p3;				
    farColor += tex2D(NivelBaixoSampler, input.TexCoord / farTextureEspalhamento) * p1;	
    farColor += tex2D(NivelMedioSampler, input.TexCoord / farTextureEspalhamento) * p2;	
    farColor += tex2D(NivelAltoSampler, input.TexCoord / farTextureEspalhamento) *  p4;	
    
    
    float4 nearColor;
    nearColor = tex2D(diffuseSampler, input.TexCoord /nearTextureEspalhamento)     * p3;				
    nearColor += tex2D(NivelBaixoSampler, input.TexCoord /nearTextureEspalhamento) * p1;	
    nearColor += tex2D(NivelMedioSampler, input.TexCoord /nearTextureEspalhamento ) * p2;	
    nearColor += tex2D(NivelAltoSampler, input.TexCoord /nearTextureEspalhamento)  * p4;	
    
    
	float blendFactor = clamp((output.Depth.r - blendDistance)/blendWidth, 0, 1);    
	output.Color  = lerp(nearColor, farColor, blendFactor);    
	
	
    
    //output.Color = float4(color,0);											   //output Color
    output.Color.a = 0;														   //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);                   //transform normal domain
    output.Normal.a = 0;													       //output SpecularPower    
    output.Extra1.rgb =  0;  
    output.Extra1.a =  id;  
    return output;
}


PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Depth = 1-input.Depth.x / input.Depth.y;                                  //output Depth  
    
    float4 farColor;    
    farColor = tex2D(diffuseSampler, input.TexCoord / farTextureEspalhamento);				
    
    float4 nearColor;
    nearColor = tex2D(diffuseSampler, input.TexCoord / nearTextureEspalhamento);					       
    
    
	float blendFactor = clamp((output.Depth.r - blendDistance)/blendWidth, 0, 1);    	
    output.Color  = lerp(nearColor, farColor, blendFactor);    
    
    //output.Color = float4(color,0);											   //output Color
    output.Color.a = 0;														   //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);                   //transform normal domain
    output.Normal.a = 0;													       //output SpecularPower    
    output.Extra1.rgb =  0;  
    output.Extra1.a =  id / 255.0f;  
    return output;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}

technique MultiTexture
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunctionMulti();
    }
}