texture cene;
sampler ceneSampler = sampler_state
{
   Texture = <cene>;
   MinFilter = POINT;
   MagFilter = POINT;
   MipFilter = POINT;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

float3 objPos[15];
float num = 10;
float glowSize[15] ;
float4x4 xViewProjection;
float3 xCameraPos;



struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float2 Pos : TEXCOORD1;
};

float2 halfPixel;

VertexShaderOutput VShader( float4 Pos: POSITION, float2 Tex : TEXCOORD)
{
	VertexShaderOutput output;
    output.Position = float4(Pos);
    output.Pos = float4(Pos);
    output.TexCoord = Tex - halfPixel;
    return output;
}

float4 PShader(VertexShaderOutput input) : COLOR
{	
     float4 baseColor = tex2D(ceneSampler, input.TexCoord);                            
     float2 screenPos =  input.Pos;
     
     for (int i=0; i<10; i++)
     {
         float4 lightScreenPos = mul(float4(objPos[i],1),xViewProjection);
         lightScreenPos /= lightScreenPos.w;
         
         float dist = distance(screenPos.xy, lightScreenPos.xy);
         float radius = glowSize[i]/distance(xCameraPos, objPos[i]);
         if (dist < radius)
         {
             baseColor.rgb += (radius-dist)*1.3f;
        }
    }

    return baseColor;	
	
}

technique GlowShader
{
	pass P0
	{	
		VertexShader = compile vs_4_0 VShader();
		PixelShader = compile ps_4_0 PShader();
	}
}


