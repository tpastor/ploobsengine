float4x4 xWorld;
float4x4 xView;
float4x4 xProjection;

struct VertexToPixel
{
	half4 Position		: POSITION;
	half2 ScreenPos			: TEXCOORD0;
};
struct PixelToFrame
{
	half4 Color 			: COLOR0;
};

//------- Technique: ShadowMap --------
VertexToPixel MyVertexShader(half4 inPos: POSITION0)
{
	VertexToPixel Output = (VertexToPixel)0;	
	float4x4 preViewProjection = mul(xView, xProjection);
	float4x4 preWorldViewProjection = mul(xWorld, preViewProjection);
	Output.Position = mul(inPos, preWorldViewProjection);			
	Output.ScreenPos = Output.Position .zw;	
	return Output;	
	
}


PixelToFrame MyPixelShader(VertexToPixel PSIn) : COLOR0
{
	PixelToFrame Output = (PixelToFrame)0;				
	Output.Color = PSIn.ScreenPos.x/PSIn.ScreenPos.y;			
	return Output;
}

technique ShadowMap
{
	pass Pass0
    {  
    	VertexShader = compile vs_2_0 MyVertexShader();
        PixelShader  = compile ps_2_0 MyPixelShader();
    }
}
