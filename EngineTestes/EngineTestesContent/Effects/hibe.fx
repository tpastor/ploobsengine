float4x4 World;
float4x4 View;
float4x4 Projection;

texture Texture;
sampler cenaSampler = sampler_state
{
   Texture = <Texture>;   
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
    MagFilter = ANISOTROPIC;
    MinFilter = ANISOTROPIC;
    Mipfilter = LINEAR;
};

texture light;
sampler lightSampler = sampler_state
{
   Texture = <light>;   
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
    MagFilter = POINT;
    MinFilter = POINT;    
};



struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;        	  
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;        
	float4 ScreenPos : TEXCOORD1	;      
};

const float2 halfPixel;

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);	
	output.ScreenPos = output.Position;	
	output.TexCoord = input.TexCoord;   
    return output;
}



float4 PixelShaderFunction(in float2 TexCoord : TEXCOORD0_centroid, in float4 ScreenPos : TEXCOORD1_centroid	) : COLOR0
{
		ScreenPos.xy /= ScreenPos.w;
		float2 resp = 0.5f * (float2(ScreenPos.x,-ScreenPos.y) + 1);		
		resp +=halfPixel;

		float3 diffuseColor = tex2D(cenaSampler,TexCoord).rgba;	
		float4 light = tex2D(lightSampler,resp);		
		float3 diffuseLight = light.rgb;
		float specularLight = light.a;
		return float4((diffuseColor * (diffuseLight)+ specularLight),0);
    
}

technique Technique1
{
    pass Pass1
    {        
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
