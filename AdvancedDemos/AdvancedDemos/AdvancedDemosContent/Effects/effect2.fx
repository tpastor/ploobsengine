float2 halfPixel;
float4x4 WorldViewProjection;
float4x4 WorldView;
float DistortionScale;
texture DEPTH;
sampler depthSampler = sampler_state
{
    Texture = (DEPTH);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};

struct PositionNormal
{
   float4 Position : POSITION;
   float3 Normal : NORMAL;
};

struct PositionDisplacement
{
   float4 Position : POSITION;
   float2 Displacement : TEXCOORD1;
   float4 ScreenPosition : TEXCOORD2;
};

PositionDisplacement PullIn_VertexShader(PositionNormal input)
{
   PositionDisplacement output;
    
   output.Position = mul(input.Position, WorldViewProjection);
   output.ScreenPosition = output.Position;
   float3 normalWV = mul(input.Normal, WorldView);
   normalWV.y = -normalWV.y;
   
   float amount = dot(normalWV, float3(0,0,1)) * DistortionScale;
   output.Displacement = float2(.5,.5) + float2(amount * normalWV.xy);

   return output;   
}

float4 DisplacementPassthrough_PixelShader(PositionDisplacement input: TEXCOORD) : COLOR
{  
	input.ScreenPosition.xyz /= input.ScreenPosition.w;
    float2 texCoord = 0.5f * (float2(input.ScreenPosition.x,-input.ScreenPosition.y) + 1);
    //allign texels to pixels
    texCoord +=halfPixel;

	float depth = 1 - tex2D(depthSampler,texCoord).r;
	clip(depth - input.ScreenPosition.z );

    return float4(input.Displacement, 0, 1);
}

technique PullIn
{
    pass
    {
        VertexShader = compile vs_2_0 PullIn_VertexShader();
        PixelShader = compile ps_2_0 DisplacementPassthrough_PixelShader();
    }
}


//
//
//float4x4 WorldViewProjection;
//float4x4 WorldView;
//float DistortionScale;
//
//struct PositionNormal
//{
   //float4 Position : POSITION;
   //float3 Normal : NORMAL;
//};
//
//struct PositionDisplacement
//{
   //float4 Position : POSITION;
   //float2 Displacement : TEXCOORD;
//};
//
//PositionDisplacement PullIn_VertexShader(PositionNormal input)
//{
   //PositionDisplacement output;
//
   //output.Position = mul(input.Position, WorldViewProjection);
   //float3 normalWV = mul(input.Normal, WorldView);
   //normalWV.y = -normalWV.y;
   //
   //float amount = dot(normalWV, float3(0,0,1)) * DistortionScale;
   //output.Displacement = float2(.5,.5) + float2(amount * normalWV.xy);
//
   //return output;   
//}
//
//float4 DisplacementPassthrough_PixelShader(float2 displacement : TEXCOORD) : COLOR
//{  
   //return float4(displacement, 0, 1);
//}
//
//technique PullIn
//{
    //pass
    //{
        //VertexShader = compile vs_2_0 PullIn_VertexShader();
        //PixelShader = compile ps_2_0 DisplacementPassthrough_PixelShader();
    //}
//}
//