///YES, it is a mess code

float weight;
float2 pixel_size;

texture image;
texture depthTex;
texture normalTex;

sampler2D TextureSampler  {
    Texture = <image>;    
};

sampler2D depthMap {
    Texture = <depthTex>;
    AddressU  = Clamp; AddressV = Clamp;
    MipFilter = Point; MinFilter = Point; MagFilter = Point;    
};

sampler2D NormalMap {
    Texture = <normalTex>;
    AddressU = Clamp; AddressV = Clamp;
    MipFilter = Point; MinFilter = Point; MagFilter = Point;    
};


const float2 delta[8] =
 {
 float2(-1,1),float2(1,-1),float2(-1,1),float2(1,1),
 float2(-1,0),float2(1,0),float2(0,-1),float2(0,1)
 };



 ///////////////////////////////////////////////////////

 float4 DL_GetDepth(float2 uv)
 {
	return 1 - tex2D(depthMap,uv);
 }
 float4 DL_GetNormal(float2 uv)
 {
	return tex2D(NormalMap   ,uv);
 }
 
    
/////////TABULA///////////////

float depthSensibility;
float normalSensibility;
  ////////////////////////////  
   // Neighbor offset table  
   ////////////////////////////  
   const static float2 offsets[9] = {  
  float2( 0.0,  0.0), //Center       0  
   float2(-1.0, -1.0), //Top Left     1  
   float2( 0.0, -1.0), //Top          2  
   float2( 1.0, -1.0), //Top Right    3  
   float2( 1.0,  0.0), //Right        4  
   float2( 1.0,  1.0), //Bottom Right 5  
   float2( 0.0,  1.0), //Bottom       6  
   float2(-1.0,  1.0), //Bottom Left  7  
   float2(-1.0,  0.0)  //Left         8  
};  
float DL_GetEdgeWeight(in float2 screenPos)  
{  
  float Depth[9];  
  float3 Normal[9];  
  //Retrieve normal and depth data for all neighbors.  
   for (int i=0; i<9; ++i)  
  {  
    float2 uv = screenPos + offsets[i] * pixel_size;  
    Depth[i] = DL_GetDepth(uv);  //Retrieves depth from MRTs  
    Normal[i]= DL_GetNormal(uv); //Retrieves normal from MRTs  
  }  
  //Compute Deltas in Depth.  
   float4 Deltas1;  
  float4 Deltas2;  
  Deltas1.x = Depth[1];  
  Deltas1.y = Depth[2];  
  Deltas1.z = Depth[3];  
  Deltas1.w = Depth[4];  
  Deltas2.x = Depth[5];  
  Deltas2.y = Depth[6];  
  Deltas2.z = Depth[7];  
  Deltas2.w = Depth[8];  
  //Compute absolute gradients from center.  
  Deltas1 = abs(Deltas1 - Depth[0]);  
  Deltas2 = abs(Depth[0] - Deltas2);  
  //Find min and max gradient, ensuring min != 0  
   float4 maxDeltas = max(Deltas1, Deltas2);  
  float4 minDeltas = max(min(Deltas1, Deltas2), 0.00001);  
  // Compare change in gradients, flagging ones that change  
   // significantly.  
   // How severe the change must be to get flagged is a function of the  
   // minimum gradient. It is not resolution dependent. The constant  
   // number here would change based on how the depth values are stored  
   // and how sensitive the edge detection should be.  
   float4 depthResults = step(minDeltas * depthSensibility, maxDeltas);  
  //Compute change in the cosine of the angle between normals.  
  Deltas1.x = dot(Normal[1], Normal[0]);  
  Deltas1.y = dot(Normal[2], Normal[0]);  
  Deltas1.z = dot(Normal[3], Normal[0]);  
  Deltas1.w = dot(Normal[4], Normal[0]);  
  Deltas2.x = dot(Normal[5], Normal[0]);  
  Deltas2.y = dot(Normal[6], Normal[0]);  
  Deltas2.z = dot(Normal[7], Normal[0]);  
  Deltas2.w = dot(Normal[8], Normal[0]);  
  Deltas1 = abs(Deltas1 - Deltas2);  
  // Compare change in the cosine of the angles, flagging changes  
   // above some constant threshold. The cosine of the angle is not a  
   // linear function of the angle, so to have the flagging be  
   // independent of the angles involved, an arccos function would be  
   // required.  
  float4 normalResults = step(0.4, Deltas1* normalSensibility);  
   
  normalResults = max(normalResults, depthResults);  
  return (normalResults.x + normalResults.y +  
          normalResults.z + normalResults.w) * 0.25;  
}  

void PassThroughVS(inout float4 position : POSITION0,
                   inout float2 texcoord : TEXCOORD0) {

				   position.x =  position.x - pixel_size;
				   position.y =  position.y + pixel_size;
}


/////////////////////////////////////////////////////////////////////
///STALKER
float2 e_barrier = float2(0.8f,0.00001f);  // x=norm(~.8f), y=depth(~.5f)

float2 e_weights= float2(1,1);  // x=norm, y=depth

float2 e_kernel = float2(1,1);   // x=norm, y=depth

const static float2 offs[7] = {  
   float2( 0.0,  0.0), //Center       0  
   float2(-1.0, -1.0), //Top Left     1     
   float2( 1.0,  1.0), //Bottom Right 5  
   float2( 1.0, -1.0), //Top Right    3  
   float2(-1.0,  1.0), //Bottom Left  7  
   float2(-1.0,  0.0),  //Left         8     
   float2( 0.0, -1.0) //Top          2  
};  


float4 main(float2 uv) : COLOR

{

 // Normal discontinuity filter

 float3 nc = tex2D(NormalMap, uv);
 
 float4 nd;

 nd.x = abs(dot(nc, tex2D(NormalMap, uv + offs[1]* pixel_size).xyz));

 nd.y = abs(dot(nc, tex2D(NormalMap, uv + offs[2]* pixel_size).xyz));

 nd.z = abs(dot(nc, tex2D(NormalMap, uv + offs[3]* pixel_size).xyz));

 nd.w = abs(dot(nc, tex2D(NormalMap, uv + offs[4]* pixel_size).xyz));

 nd -= e_barrier.x;

 nd = step(0, nd);

 float ne = saturate(dot(nd, e_weights.x));

 // Opposite coords

 float2 tc5r = -offs[5];

 float2 tc6r = -offs[6];


 // Depth filter : compute gradiental difference:

 // (c-sample1)+(c-sample1_opposite)

 float dc = 1-tex2D(depthMap, uv).r;

 float4 dd;

 dd.x = (1 - tex2D(depthMap, uv + offs[1] * pixel_size).r) +

         (1 - tex2D(depthMap, uv + offs[2]* pixel_size).r);

 dd.y = (1 - tex2D(depthMap, uv + offs[3]* pixel_size).r) +

        (1 - tex2D(depthMap, uv + offs[4]* pixel_size).r);

 dd.z = (1 - tex2D(depthMap, uv + offs[5]* pixel_size).r) +

        (1 - tex2D(depthMap, uv + tc5r* pixel_size).r);

 dd.w = (1 - tex2D(depthMap,uv +  offs[6]* pixel_size).r) +

        (1 - tex2D(depthMap,uv +  tc6r* pixel_size).r);

 dd = abs(2 * dc - dd)- e_barrier.y;

 dd = step(dd, 0);

 float de = saturate(dot(dd, e_weights.y));
 
  
 // Weight

 float w = (1 - de * ne) * e_kernel.x; // 0 - no aa, 1=full aa
 

 //return float4(w,w,w,1);

 // Smoothed color

 // (a-c)*w + c = a*w + c(1-w)

 float2 offset = (uv ) * (1-w);

 float4 s0 = tex2D(TextureSampler, offset + (uv + offs[1] * pixel_size) * w);

 float4 s1 = tex2D(TextureSampler, offset + (uv + offs[2]* pixel_size) * w);

 float4 s2 = tex2D(TextureSampler, offset + (uv + offs[3]* pixel_size) * w);

 float4 s3 = tex2D(TextureSampler, offset + (uv + offs[4]* pixel_size) * w);

 return (s0 + s1 + s2 + s3)/4.h;

}

float4 PShader2(float2 texCoord : TEXCOORD0) : COLOR0
 {
	return main(texCoord);
 }

////////////////////////////////////////////////////////////////////////////////////////////////


float4 PShader(float2 texCoord : TEXCOORD0) : COLOR0
 {
  
 float4 tex = tex2D(NormalMap,texCoord);
 float factor = 0.0f;

 for( int i=0;i<4;i++ )
 {
	 float4 t = tex2D(NormalMap,texCoord+ delta[i]*pixel_size);
	 t -= tex;
	 factor += dot(t,t);
 }

 factor = min(1.0,DL_GetEdgeWeight(texCoord))*weight; 

 float4 color = float4(0.0,0.0,0.0,0.0);

 for( int j=0;j<8;j++ )
 {
	color += tex2D(TextureSampler,texCoord + delta[j]*pixel_size*factor);
 }
 color += 2.0*tex2D(TextureSampler,texCoord);
 return color*(1.0/10.0); 
 
 } 
 
 technique AntiAliasingTabula
{
    pass Pass1
    {
		VertexShader = compile vs_3_0 PassThroughVS();
        PixelShader = compile ps_3_0 PShader();
    }
}

 technique AntiAliasingStalker
{
    pass Pass1
    {
		VertexShader = compile vs_3_0 PassThroughVS();
        PixelShader = compile ps_3_0 PShader2();
    }
}