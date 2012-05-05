float4x4 ViewProjection;
float4x4 World;
float2 mTexOffset0;
float2 mTexOffset1;

struct TexVertexToPixel
{
    float4 Position   	: POSITION;    
    float4 Color		: COLOR0;
    float4 PositionExport : TEXCOORD0;
    float2 TextureCoords1: TEXCOORD1;
    float2 TextureCoords2: TEXCOORD2;
    
};

struct TexPixelToFrame
{
    float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Depth : COLOR2;
    float4 EXTRA1 : COLOR3;
};


Texture xTexture0;
sampler TextureSampler0 = sampler_state 
{ texture = <xTexture0>; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};

Texture xTexture1;
sampler TextureSampler1 = sampler_state 
{ texture = <xTexture1>; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};


TexVertexToPixel TexturedVS( float4 inPos : POSITION, float3 inNormal: NORMAL, float2 inTexCoords: TEXCOORD0)
{	
	TexVertexToPixel Output = (TexVertexToPixel)0;	
	float4x4 preWorldViewProjection = mul (World, ViewProjection);
    
	Output.Position = mul(inPos, preWorldViewProjection);	
	Output.TextureCoords1 = inTexCoords + mTexOffset0;
	Output.TextureCoords2 = inTexCoords + mTexOffset1;
	Output.PositionExport = inPos ;	
	float3 Normal = normalize(mul(normalize(inNormal), World));			
	return Output;     
}

TexPixelToFrame TexturedPS(TexVertexToPixel PSIn) 
{
	TexPixelToFrame Output = (TexPixelToFrame)0;	
	float4 topColor = float4(0.3f, 0.3f, 0.8f, 1);    
    float4 bottomColor = 1;    	    
    
    float4 baseColor = lerp(bottomColor, topColor, saturate((PSIn.PositionExport.y)/0.4f));
	float4 c1 = tex2D(TextureSampler0, PSIn.TextureCoords1).r;	
	float4 c0 = tex2D(TextureSampler1, PSIn.TextureCoords2).r;		
	Output.Color = lerp(baseColor,1, c0)*0.5f;	
	Output.Color += lerp(baseColor,1, c1)*0.5f;	
	
	//float4 c0 = tex2D(TextureSampler, PSIn.TextureCoords1) ;
    //float4 c1 = tex2D(TextureSampler, PSIn.TextureCoords2);          	
	Output.Normal.rgb = 0.5f;    
    Output.Normal.a = 0.0f;    
    Output.Depth = 0;	
    Output.EXTRA1.rgb = 0;	
    Output.EXTRA1.a = 3.0f / 255.0f;;
	return Output;	
}

technique Textured
{
	pass Pass0
    {   
    	VertexShader = compile vs_2_0 TexturedVS();
        PixelShader  = compile ps_2_0 TexturedPS();
    }
}