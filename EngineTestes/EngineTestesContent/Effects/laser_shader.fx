float4x4 world;
float4x4 wvp;
struct LaserVertexToPixel
{
    float4 Position     : POSITION;
    float3 Normal       : TEXCOORD0;
    float3 Position3D   : TEXCOORD1;
    float4 Orig_pos     : TEXCOORD2;
};

struct PixelToFrame
{  
    float4 Color        : COLOR0;
};

//= Laser bolt variables
uniform extern float4 laser_bolt_color;
uniform extern float3 center_to_viewer;

//++=============================================================================================================================================
// Vertex Shaders 
//++=============================================================================================================================================

LaserVertexToPixel Laser_VertexShader( float4 inPos : POSITION0, float3 inNormal: NORMAL0, float2 inTexCoords : TEXCOORD0, float4 Color : COLOR0)
{
    LaserVertexToPixel Output = (LaserVertexToPixel)0;
   
    Output.Position   = mul(inPos, wvp);
    // we'll normalize the normal in the pixel shader.  No guarantee it will be normal coming out of the raterizer so
    // no point in normalizing it twice by doing it here and in the pixel shader.
    Output.Normal     = (mul(inNormal, (float3x3) world));
    
    Output.Position3D = mul(inPos, world);	
    
    Output.Orig_pos = inPos;

    return Output;
};

//++=============================================================================================================================================
// Pixel Shaders 
//++=============================================================================================================================================
float whiteAttenuation = 1.75;
float falloffAttenuation1 = .175;
float falloffAttenuation2 = 1.25;

PixelToFrame Laserbolt_PixelShader (LaserVertexToPixel PSIn)
{
      PixelToFrame Output = (PixelToFrame)0;

      float cosang;
      float white_heat;
      float4 the_color;
    
       PSIn.Normal = normalize(PSIn.Normal);
	   float3 dir = normalize(center_to_viewer - PSIn.Position3D);
     
       // determines whether we're processing at the center (1) or the edges (near 0)
       cosang = abs((dot(dir,PSIn.Normal))); 
      
       // width of the white stripe down the middle controlled by the exponent
       white_heat =  pow(cosang,whiteAttenuation);  
       
       // bit of alpha blending on the ends	   
       float endfade = 1 - sqrt(PSIn.Orig_pos.x * PSIn.Orig_pos.x + PSIn.Orig_pos.z * PSIn.Orig_pos.z);
    
       // falloff controlled by the exponent
       endfade =  pow(endfade,falloffAttenuation1);
     
       // add in the white heat
       the_color =  laser_bolt_color + white_heat ;
 
      //alpha on the edges.. again, fadeoff controlled by the exponent
      the_color.a = pow(cosang,falloffAttenuation2);
     
	  the_color.a = min(the_color.a,endfade);
	    
      Output.Color = the_color;
   return Output;
};

//++=============================================================================================================================================
// Techniques 
//++=============================================================================================================================================

technique laserbolt_technique
{
    pass laser_pass
    {
         VertexShader = compile vs_3_0 Laser_VertexShader();
         PixelShader = compile ps_3_0 Laserbolt_PixelShader();
    }
}
