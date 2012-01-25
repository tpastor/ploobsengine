float4x4 World;
float4x4 View;
float4x4 Projection;
float specularIntensity = 0.8f;
float specularPower = 0.5f; 
float reflectionIndex = 0.5f;
float id;

texture Cubemap;
sampler SamplerCubemap = sampler_state
{
	Texture = <Cubemap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
};


texture Texture;
sampler diffuseSampler = sampler_state
{
    Texture = (Texture);
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
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2;        
    float3 Vector_refl : TEXCOORD3;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                             //pass the texture coordinates further
    output.Normal =mul(input.Normal,World);                       //get normal into world space        
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;   
    
    float3 posicionCamara = mul(-View._m30_m31_m32, transpose(View));    
    output.Vector_refl = reflect(normalize(worldPosition - posicionCamara),normalize(mul(input.Normal,World)));       
    return output;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 LightOcclusion : COLOR3;
};

PixelShaderOutput PixelShaderFunctionPerfectMirror(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Color = texCUBE(SamplerCubemap,input.Vector_refl);					   //output Color
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = input.Depth.x / input.Depth.y;                              //output Depth
	output.LightOcclusion =  0;           
        
    return output;
}

PixelShaderOutput PixelShaderFunctionReflexiveSurface(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Color = reflectionIndex * texCUBE(SamplerCubemap,input.Vector_refl) + (1 - reflectionIndex)* tex2D(diffuseSampler, input.TexCoord);   //output Color    
    
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = input.Depth.x / input.Depth.y;                              //output Depth  
	output.LightOcclusion =  0;       
	output.LightOcclusion.a =  id/ 255.0f;        
        
    return output;
}


technique PerfectMirror
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionPerfectMirror();
    }
}

technique ReflexiveSurface
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionReflexiveSurface();
    }
}

///////////////////////////////////////////////////////////////////////////////////
