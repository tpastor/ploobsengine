//------- XNA interface --------
float alphaTest;
float3 camUp;
float3 forward;
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;
float scaleX = 1;
float scaleY = 1;
float4 atenuation = float4(1,1,1,1);
//------- Texture Samplers --------
sampler textureSampler : register(s0);
struct BBVertexToPixel
{
    float4 Position : POSITION;
    float2 TexCoord    : TEXCOORD0;
    float2 Depth : TEXCOORD1;
};
struct BBPixelToFrame
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 Extra1 : COLOR3;
};

//------- Technique: CylBillboard --------
BBVertexToPixel CylBillboardVS(float4 inPos: POSITION0, float2 inTexCoord: TEXCOORD0 , float3 normal : Normal0)
{
    BBVertexToPixel Output = (BBVertexToPixel)0;

    float3 center = mul(inPos, xWorld);    

    float3 upVector = camUp;
    upVector = normalize(upVector);
    
    float3 right = cross(upVector,forward);
    float3 sideVector = right;
    sideVector = normalize(sideVector);
    

    float3 finalPosition = center;
    finalPosition += (inTexCoord.x-0.5f)*sideVector * scaleX;
    finalPosition += (1.5f-inTexCoord.y*1.5f)*upVector * scaleY;      
    
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

	output.Color = output.Color  * atenuation;
    output.Normal.rgb = 0.5f;                   
    output.Normal.a = 0;													       
    output.Extra1.rgba =  0;  
	output.Depth = PSIn.Depth.x / PSIn.Depth.y;
	output.Extra1.a =  1.0f / 255.0f ;		        
    return output;
}

technique SpheBillboard
{
    pass Pass0
    {        
        VertexShader = compile vs_2_0 CylBillboardVS();
        PixelShader = compile ps_2_0 BillboardPS();        
    }
}
