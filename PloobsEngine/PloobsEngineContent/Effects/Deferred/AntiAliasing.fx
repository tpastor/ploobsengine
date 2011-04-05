
float weight;
float2 pixel_size;

sampler TextureSampler : register(s0);

const float2 delta[8] =
 {
 float2(-1,1),float2(1,-1),float2(-1,1),float2(1,1),
 float2(-1,0),float2(1,0),float2(0,-1),float2(0,1)
 };


texture normal;
sampler normalSampler = sampler_state
{
   Texture = <normal>;
   MinFilter = POINT;
   MagFilter = POINT;
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

float4 PShader(float2 texCoord : TEXCOORD0) : COLOR0
 {
  
 float4 tex = tex2D(normalSampler ,texCoord);
 float factor = 0.0f;

 for( int i=0;i<4;i++ )
 {
	 float4 t = tex2D(normalSampler ,texCoord+ delta[i]*pixel_size);
	 t -= tex;
	 factor += dot(t,t);
 }
 factor = min(1.0,factor)*weight;
 float4 color = float4(0.0,0.0,0.0,0.0);

 for( int i=0;i<8;i++ )
 {
	color += tex2D(TextureSampler,texCoord + delta[i]*pixel_size*factor);
 }
 color += 2.0*tex2D(TextureSampler,texCoord);
 return color*(1.0/10.0);
 
 } 
 
 technique AntiAliasing
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PShader();
    }
}