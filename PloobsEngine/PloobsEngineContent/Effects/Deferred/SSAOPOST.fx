uniform float4		Params; // .w = about 10 or so (pixel radius) whilst .x and .y are RT dimensions and .z=viewport far
uniform float4x4	InvProj;		// Full-fat 4x4 projection matrix that's been inverted, then transposed, then set row by row into these 4x constants
uniform float4x4	View;
uniform float jitter =  0.001F;
uniform float diffScale =  1;
uniform float whiteCorrection = 0;
texture DepthBuffer;
texture NormalBuffer;
texture RandomTexture;
float2 halfPixel;

sampler2D DepthSampler = 
sampler_state
{
	Texture = <DepthBuffer>;
	MinFilter = POINT;
	MipFilter = POINT;
	MagFilter = POINT;
	
	AddressU = CLAMP;
	AddressV = CLAMP;
};

sampler2D NormalSampler = 
sampler_state
{
	Texture = <NormalBuffer>;
	MinFilter = POINT;
	MipFilter = POINT;
	MagFilter = POINT;
	
	AddressU = CLAMP;
	AddressV = CLAMP;
};

sampler2D RandomSampler = 
sampler_state
{
	Texture = <RandomTexture>;
	MinFilter = LINEAR;
	MipFilter = NONE;
	MagFilter = LINEAR;
	
	AddressU = WRAP;
	AddressV = WRAP;
};

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 UV 	        : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TextCoord : TEXCOORD0;
};

float3	PosFromDepth (float2 UV)
{
	float	Depth=tex2D(DepthSampler,UV).r;	
	float4	Pos=float4((UV.x-0.5)*2,(0.5-UV.y)*2,Depth,1);
	float4	Ray=mul(Pos,InvProj);	
	//Ray /= Ray.w;
	return	Ray.xyz ;
}

float3	ReadNormal (float2 UV)
{
	 float4 normalData = tex2D(NormalSampler,UV);       
     float3 normal = 2.0f * normalData.xyz - 1.0f;     
     normal = mul(normal,View);  	 	 
     return normal;	
}

float3 getRandom(float2 uv)
{
	return normalize(tex2D(RandomSampler, float2(Params.x,Params.y) * uv / 128.0f * 3.0f).rgb * 2.0f - 1.0f);
}


float aoFF (float3 ddiff,float3 cnorm,float2 UV)
{
	float3	vv=normalize(ddiff);
	float	rd=length(ddiff)*diffScale ;
	return (1-clamp(dot(ReadNormal(UV),-vv),0,1))*clamp(dot(cnorm,vv),0,1)* (1-1/sqrt(1/(rd*rd)+1));
}

float4 main (VertexShaderOutput input):COLOR
{																	
   float3	n=ReadNormal(input.TextCoord);										
   float3	p=PosFromDepth(input.TextCoord);										
   float3	Random=jitter*Params.w*getRandom(input.TextCoord);			

   float	ao=0;													
   float	incx=1.0F/Params.x*Params.w;							
   float	incy=1.0F/Params.y*Params.w;							
   float	pw=incx;												
   float	ph=incy;												

    float	Sample=tex2D(DepthSampler,input.TextCoord).r;								
	float	CDepth=1.0F/Sample;			

	for (float i=0;i<3;++i)											 
	{																
		float npw = (pw+Random.x)*CDepth;							
		float nph = (ph+Random.y)*CDepth;							

		float2	UV1=input.TextCoord+float2(npw,nph);								
		float3	Dif1=PosFromDepth(UV1)-p;							
		ao+=	aoFF(Dif1,n,UV1);									

		float2	UV2=input.TextCoord+float2(npw,-nph);							
		float3	Dif2=PosFromDepth(UV2)-p;							
		ao+=	aoFF(Dif2,n,UV2);									

		float2	UV3=input.TextCoord+float2(-npw,nph);							
		float3	Dif3=PosFromDepth(UV3)-p;							
		ao+=	aoFF(Dif3,n,UV3);									

		float2	UV4=input.TextCoord+float2(-npw,-nph);							
		float3	Dif4=PosFromDepth(UV4)-p;							
		ao+=	aoFF(Dif4,n,UV4);									

		float2	UV5=input.TextCoord+float2(0,nph);								
		float3	Dif5=PosFromDepth(UV5)-p;							
		ao+=	aoFF(Dif5,n,UV5);									

		float2	UV6=input.TextCoord+float2(0,-nph);								
		float3	Dif6=PosFromDepth(UV6)-p;							
		ao+=	aoFF(Dif6,n,UV6);									

		float2	UV7=input.TextCoord+float2(npw,0);								
		float3	Dif7=PosFromDepth(UV7)-p;							
		ao+=	aoFF(Dif7,n,UV7);									

		float2	UV8=input.TextCoord+float2(-npw,0);								
		float3	Dif8=PosFromDepth(UV8)-p;							
		ao+=	aoFF(Dif8,n,UV8);									

		pw += incx;													
		ph += incy;													
	}																

	float	Val=(ao/24);	
	Val = Val + whiteCorrection;										
	return	float4(Val,Val,Val,1);								
		
	
}	
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)  
{  
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
    output.TextCoord = input.UV;
    return output;
}  				

technique SSAOTech
{
    pass P0
    {	
        VertexShader = compile vs_3_0 VertexShaderFunction();
        pixelShader  = compile ps_3_0 main();   		
    }
}

