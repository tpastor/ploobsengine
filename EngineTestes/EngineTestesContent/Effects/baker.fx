float4x4 world;
float4x4 view;
float4x4 projection;
bool lightMapping;
float2 halfPixel;
float2 uvcorrection;
 

texture diffuse;
sampler TexSampler = sampler_state
{
   Texture = <diffuse>;
   MinFilter = anisotropic;
   MagFilter = anisotropic;
   MipFilter = LINEAR;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

struct VertexShaderInput
{
    float3 position : POSITION0;
	float3 normal : NORMAL;
    float2 texCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float3  WorldNormal : TEXCOORD0;
	float4  WorldPosition : TEXCOORD1;
	float2  TexCoords: TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

     float4x4 wvp = mul(mul(world, view), projection);

     output.Position = mul(float4(input.position, 1.0), wvp);

     output.WorldNormal =  mul(input.normal, world);
     float4 worldPosition =  mul(float4(input.position, 1.0), world);
     output.WorldPosition = worldPosition / worldPosition.w;

     output.TexCoords.x = input.texCoord.x * uvcorrection.x;
     output.TexCoords.y = input.texCoord.y * uvcorrection.y;  
	 
     if (lightMapping){
		float2 screenPosTexCoord = float2(input.texCoord.x-0.5f, -input.texCoord.y+0.5f) * 2;
        output.Position = float4(screenPosTexCoord,0,1);
		//output.Position.x =  output.Position.x - 2*halfPixel.x;
		//output.Position.y =  output.Position.y + 2*halfPixel.y;
     }

     return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    return tex2D(TexSampler,input.TexCoords);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
