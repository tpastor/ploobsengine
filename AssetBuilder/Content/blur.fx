float BlurDistance = 0.002f;

sampler ColorMapSampler : register(s0);
 
float4 PShader(float2 Tex: TEXCOORD0) : COLOR
{
float4 Color;
 
       Color  = tex2D( ColorMapSampler, float2(Tex.x+BlurDistance, Tex.y+BlurDistance));
       Color += tex2D( ColorMapSampler, float2(Tex.x-BlurDistance, Tex.y-BlurDistance));
       Color += tex2D( ColorMapSampler, float2(Tex.x+BlurDistance, Tex.y-BlurDistance));
	   Color += tex2D( ColorMapSampler, float2(Tex.x-BlurDistance, Tex.y+BlurDistance));
	   Color = Color / 4;
 

return Color;
}
 
technique PostProcess
{
       pass P0
       {
             // A post process shader only needs a pixel shader.
             PixelShader = compile ps_4_0 PShader();
       }
}