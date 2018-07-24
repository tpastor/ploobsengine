const float2 halfPixel = 0.5f /float2(800,600);
const float width = 800;

struct VS_INPUT 
{
   float4 Position : POSITION0;
   float2 TexCoord : TEXCOORD0;    
   
};

struct VS_OUTPUT
{
    float4 pos				: POSITION;
    float2 TexCoord			: TEXCOORD0;
};

VS_OUTPUT vs_main( VS_INPUT Input )
{
   VS_OUTPUT Output;

   Output.pos =  Input.Position;
   Output.TexCoord = Input.TexCoord;
   return( Output );
   
}
texture textureSSAO;

sampler2D samplerSSAO = sampler_state
{
	Texture = <textureSSAO>;
    ADDRESSU = CLAMP;
	ADDRESSV = CLAMP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;	
};

float4 ps_main(VS_OUTPUT Input) : COLOR0
{   
    Input.TexCoord += halfPixel;
    float4 depth = tex2D( samplerSSAO, Input.TexCoord);
    float2 blurDirection = float2(1/width,0.0f);
    float color = tex2D( samplerSSAO,  Input.TexCoord).r;
   
   float num = 1;
   int blurSamples = 16; 
   
   for( int i = -blurSamples/2; i <= blurSamples/2; i+=1)
   {
      float4 newTexCoord = float4(Input.TexCoord + i * blurDirection.xy, 0, 0);
      
      float4 sample = tex2D(samplerSSAO, newTexCoord);
      num += (blurSamples/2 - abs(i));
      color += sample * (blurSamples/2 - abs(i)); 
   }

   return color / num;
  //return( float4( 1.0f, 0.0f, 0.0f, 1.0f ) );

}

technique SSAOBlur1
{
    pass P0
    {    
		//cullmode = none;      
        VertexShader = compile vs_4_0 vs_main();
        PixelShader  = compile ps_4_0 ps_main();
    }
}
