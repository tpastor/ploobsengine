float4x4 World;
float4x4 ViewProjection;
float specularIntensity = 0;
float specularPower = 0; 
float id;
float ambientScale;

sampler diffuseSampler : register(s0);
samplerCUBE map_diffuse : register(s4);

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4  Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2; 
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;    
    float4 worldPosition = mul( float4(input.Position,1), World);
    output.Position  = mul(worldPosition, ViewProjection);    
    output.TexCoord = input.TexCoord;                             //pass the texture coordinates further
    output.Normal =mul(input.Normal,World);                       //get normal into world space    
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;   
    return output;    
    
}
struct PixelShaderOutput
{
    float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Depth : COLOR2;
    float4 Extra1 : COLOR3;
};

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Color = tex2D(diffuseSampler, input.TexCoord);					   //output Color
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
    output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = 1 - input.Depth.x / input.Depth.y;                              //output Depth  
    output.Extra1.rgb =  0;  
    output.Extra1.a =  id/ 255.0f;  
        
    return output;
}

PixelShaderOutput PixelShaderFunction2(VertexShaderOutput input)
{
    PixelShaderOutput output;
    output.Color = tex2D(diffuseSampler, input.TexCoord);					   //output Color
    output.Color.a = specularIntensity;                                        //output SpecularIntensity
	float3 nnormal = normalize(input.Normal);
    output.Normal.rgb = 0.5f * (nnormal + 1.0f);               //transform normal domain
    output.Normal.a = specularPower;                                           //output SpecularPower
    output.Depth = 1 - input.Depth.x / input.Depth.y;                              //output Depth      
	output.Extra1.rgb = texCUBE(map_diffuse , nnormal ) * ambientScale;
    output.Extra1.a =  id/ 255.0f;  
        
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

technique Technique2 
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction2();
    }
}
