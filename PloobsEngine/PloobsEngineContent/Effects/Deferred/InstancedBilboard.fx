//------- XNA interface --------
uniform const bool cilindric;
float3 forward;
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;
float3 xAllowedRotDir = float3(0,1,0);
float3 xCamPos;
float4 atenuation = float4(1,1,1,1);
float alphaTest;

//------- Texture Samplers --------
Texture xBillboardTexture;

sampler textureSampler = sampler_state { texture = <xBillboardTexture> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = CLAMP; AddressV = CLAMP;};
struct BBVertexToPixel
{
    float4 Position : POSITION;
    float2 TexCoord    : TEXCOORD0;
    float2 Depth : TEXCOORD1;
};
struct BBPixelToFrame
{
    float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Depth : COLOR2;
    float4 Extra1 : COLOR3;
};

//------- Technique: CylBillboard --------
BBVertexToPixel CylBillboardVS(float4 inPos: POSITION0, float2 inTexCoord: TEXCOORD0 ,float3 POS: TEXCOORD1,float2 SCALE : TEXCOORD2)
{
    BBVertexToPixel Output = (BBVertexToPixel)0;

    float3 center = mul(POS, xWorld);    

    float3 upVector = xAllowedRotDir;
    upVector = normalize(upVector);
    
    float3 sideVector;
    if(cilindric)
    {	
		float3 eyeVector = center - xCamPos;
		sideVector = cross(eyeVector,upVector);
		sideVector = normalize(sideVector);
    }
    else    
    {
		float3 right = cross(upVector,forward);		
		sideVector = normalize(right);    
    }

    float3 finalPosition = center;
    finalPosition += (inTexCoord.x-0.5f)*sideVector * SCALE.x;
    finalPosition += (1.5f-inTexCoord.y*1.5f)*upVector * SCALE.y;       
    
	float4 finalPosition4 = float4(finalPosition, 1);
    
    float4x4 preViewProjection = mul (xView, xProjection);
    Output.Position = mul(finalPosition4, preViewProjection);
    Output.TexCoord = inTexCoord;    
    Output.Depth.x = Output.Position.z;
    Output.Depth.y = Output.Position.w;

    return Output;
}

BBPixelToFrame BillboardPS(BBVertexToPixel PSIn) 
{
    BBPixelToFrame output = (BBPixelToFrame)0;
    
    output.Color = tex2D(textureSampler, PSIn.TexCoord) ;        					        

	if(output.Color.a <= alphaTest)
	{
	   #ifdef XBOX
		clip(-1);
	#else
	   discard;
	#endif
	}

	output.Color  = output.Color  * atenuation;    

    output.Normal.rgb = 0.5f;                   
    output.Normal.a = 0;													       
    output.Extra1.rgba =  0;      
	output.Depth = PSIn.Depth.x / PSIn.Depth.y;
	output.Extra1.a =  1;		
    return output;
}

technique CylBillboard
{
    pass Pass0
    {        
        VertexShader = compile vs_3_0 CylBillboardVS();
        PixelShader = compile ps_3_0 BillboardPS();        
    }
}
