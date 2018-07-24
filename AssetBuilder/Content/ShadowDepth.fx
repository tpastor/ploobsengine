float4x4 WVP;

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
	Output.Position = mul(inPos, WVP);			
	Output.ScreenPos = Output.Position .zw;	
	return Output;	
	
}


PixelToFrame MyPixelShader(VertexToPixel PSIn) : COLOR0
{
	PixelToFrame Output = (PixelToFrame)0;				
	Output.Color = 1-PSIn.ScreenPos.x/PSIn.ScreenPos.y;			
	return Output;
}

technique ShadowMap
{
	pass Pass0
    {  
    	VertexShader = compile vs_4_0 MyVertexShader();
        PixelShader  = compile ps_4_0 MyPixelShader();
    }
}
