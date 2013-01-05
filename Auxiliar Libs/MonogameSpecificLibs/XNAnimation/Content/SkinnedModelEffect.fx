#define SHADER20_MAX_BONES 80
#define MAX_LIGHTS 8

// Configurations
// -------------------------------------------------
bool diffuseMapEnabled;
bool specularMapEnabled;
bool normalMapEnabled;
bool glowMapEnabled;

// Matrix
// -------------------------------------------------
float4x4 matW  : World;
float4x4 matVI  : ViewInverse;
float4x4 matVP  : ViewProjection;
float4x3 matBones[SHADER20_MAX_BONES];


// Textures and Samplers
// -------------------------------------------------
texture diffuseMap0;
sampler2D diffuseMapSampler = sampler_state {
    texture = <diffuseMap0>;
    MagFilter = Linear;
    MinFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

texture glowMap0;
sampler2D glowMapSampler = sampler_state {
    texture = <glowMap0>;
    MagFilter = Linear;
    MinFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

texture normalMap0;
sampler2D normalMapSampler = sampler_state {
    texture = <normalMap0>;
    MagFilter = Linear;
    MinFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};

texture specularMap0;
sampler2D specularMapSampler = sampler_state {
    texture = <specularMap0>;
    MagFilter = Linear;
    MinFilter = Linear;
    MipFilter = Linear;
    AddressU = Wrap;
    AddressV = Wrap;
};


// Vertex Shaders
// -------------------------------------------------

void AnimatedModelVS_LightWithNormal(
	in float4 inPosition		: POSITION,
    in float3 inNormal			: NORMAL,
    in float2 inUV0				: TEXCOORD0,
    in float3 inTangent			: TANGENT0,
    in float3 inBinormal		: BINORMAL0,
    in float4 inBoneIndex		: BLENDINDICES0,
    in float4 inBoneWeight		: BLENDWEIGHT0,

	out float4 outSVPosition	: POSITION,
    out float3 outPosition		: TEXCOORD0,
    out float2 outUV0			: TEXCOORD1,    
    out float3 outTangent		: TEXCOORD2,
    out float3 outBinormal		: TEXCOORD3,
    out float3 outNormal		: TEXCOORD4,
    out float2 outDepth			: TEXCOORD5
	)
{
    // Calculate the final bone transformation matrix
    float4x3 matSmoothSkin = 0;
    matSmoothSkin += matBones[inBoneIndex.x] * inBoneWeight.x;
    matSmoothSkin += matBones[inBoneIndex.y] * inBoneWeight.y;
    matSmoothSkin += matBones[inBoneIndex.z] * inBoneWeight.z;
    matSmoothSkin += matBones[inBoneIndex.w] * inBoneWeight.w;
    
    // Combine skin and world transformations
    float4x4 matSmoothSkinWorld = 0;
    matSmoothSkinWorld[0] = float4(matSmoothSkin[0], 0);
    matSmoothSkinWorld[1] = float4(matSmoothSkin[1], 0);
    matSmoothSkinWorld[2] = float4(matSmoothSkin[2], 0);
    matSmoothSkinWorld[3] = float4(matSmoothSkin[3], 1);
    matSmoothSkinWorld = mul(matSmoothSkinWorld, matW);
    
    // Transform vertex position and normal
    outPosition = mul(inPosition, matSmoothSkinWorld);
    outSVPosition = mul(float4(outPosition, 1.0f), matVP);

	// Matrix to put world space vectors in tangent space	
	float3x3 tangentSpace = float3x3(inTangent, inBinormal, inNormal);
	float3x3 tangentToWorld = mul(tangentSpace, (float3x3)matSmoothSkinWorld);    
	
	// Tangent base
	outTangent = tangentToWorld[0];
	outBinormal = tangentToWorld[1];
	outNormal = tangentToWorld[2];
	
	// Texture coordinate
	outUV0 = inUV0;
	
	
    outDepth.x = outSVPosition.z;
    outDepth.y = outSVPosition.w;	
}


struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 Extra1 : COLOR3;
};


float4 animatedModelPSFORWARD(	
    in float3 inPosition	: TEXCOORD0,
    in float2 inUV0			: TEXCOORD1,    
    in float3 inTangent		: TEXCOORD2,
    in float3 inBinormal	: TEXCOORD3,
    in float3 inNormal		: TEXCOORD4,
    in float2 inDepth	    : TEXCOORD5	
	) : Color0
	
	{
	
	// Normalize all input vectors
	float3 position = normalize(inPosition);    
    
	//float3x3 toTangentSpace = float3x3(
		//normalize(inTangent), normalize(inBinormal), normalize(inNormal));
	float3x3 toTangentSpace = float3x3(inTangent, inBinormal, inNormal);
    
    // Read texture diffuse color
    float3 diffuseColor =0;    
    diffuseColor = tex2D(diffuseMapSampler, inUV0);
    
    // Reads texture specular color
    float3 specularColor =0;
    if (specularMapEnabled)
        specularColor = tex2D(specularMapSampler, inUV0);
    
    // Read the surface's normal (only use the R and G channels)
    float3 normal = tex2D(normalMapSampler, inUV0);
	normal.xy = normal.xy * 2.0 - 1.0;
	normal.y = -normal.y;
	normal.z = sqrt(1.0 - dot(normal.xy, normal.xy));
    
	// Put normal in world space
	normal = mul(normal, toTangentSpace);    
	
    return float4(tex2D(diffuseMapSampler, inUV0).rgb,1);
    //return float4(normal.xyz,1);
	}


PixelShaderOutput animatedModelPS_Custom(	
    in float3 inPosition	: TEXCOORD0,
    in float2 inUV0			: TEXCOORD1,    
    in float3 inTangent		: TEXCOORD2,
    in float3 inBinormal	: TEXCOORD3,
    in float3 inNormal		: TEXCOORD4,
    in float2 inDepth	    : TEXCOORD5	
	)
{

	// Normalize all input vectors
	float3 position = normalize(inPosition);        
	
	float3x3 toTangentSpace = float3x3(inTangent, inBinormal, inNormal);
    
    // Read texture diffuse color
    float3 diffuseColor =0;    
    diffuseColor = tex2D(diffuseMapSampler, inUV0);
    
    float3 normal = 0;
    if (normalMapEnabled == true)
	{   
		//float3 normalFromMap = tex2D(normalMapSampler, inUV0);
		//tranform to [-1,1]
		//normalFromMap = 2.0f * normalFromMap - 1.0f;
		//transform into world space		
		//normalFromMap = mul(normalFromMap, toTangentSpace);
		//normalize the result
		//normalFromMap = normalize(normalFromMap);
		
		//output the normal, in [0,1] space
		//normal = 0.5f * (normalFromMap + 1.0f);
		
		
		float3 normalFromMap = tex2D(normalMapSampler, inUV0);
		//normalFromMap.xy = normalFromMap.xy * 2.0 - 1.0;
		normalFromMap = 2.0f * normalFromMap - 1.0f;
	    
		// Put normal in world space
		normal = mul(normalFromMap, toTangentSpace);    
		normal = 0.5f * (normalize(normal) + 1.0f);
			
	}
	else	
	{
		normal = 0.5f * (normalize(inNormal) + 1.0f);               
	}
    	
	PixelShaderOutput output = (PixelShaderOutput)0;        	
	
    // Calculate final color
    output.Color.a = 0;
    output.Color.rgb = diffuseColor;
		
	output.Normal.rgb = normal;    //transform normal domain
    output.Normal.a = 0;                       //output SpecularPower
    output.Depth = inDepth.x / inDepth.y;                           //output Depth
    output.Extra1 = 0.0f;    
    
    if (specularMapEnabled)
	{
		float4 specularAttributes = tex2D(specularMapSampler, inUV0);  		
		output.Color.a = specularAttributes.r;		
		output.Normal.a = specularAttributes.a;          
    }
    
    if(glowMapEnabled)
    {
    	output.Extra1.xyz = tex2D(glowMapSampler, inUV0);  		
    }
    
    return output;


}


PixelShaderOutput animatedModelPS(	
    in float3 inPosition	: TEXCOORD0,
    in float2 inUV0			: TEXCOORD1,    
    in float3 inTangent		: TEXCOORD2,
    in float3 inBinormal	: TEXCOORD3,
    in float3 inNormal		: TEXCOORD4,
    in float2 inDepth	    : TEXCOORD5	
	)
{
    // Normalize all input vectors
	float3 position = normalize(inPosition);        
	
    // Read texture diffuse color
    float3 diffuseColor =0;    
    diffuseColor = tex2D(diffuseMapSampler, inUV0);    	
	PixelShaderOutput output = (PixelShaderOutput)0;        	
        	
    // Calculate final color
    output.Color.a = 0;
    output.Color.rgb = diffuseColor;
    output.Normal.rgb = 0.5f * (normalize(inNormal) + 1.0f);               
    output.Normal.a = 0;                           
    output.Depth = inDepth.x / inDepth.y;      
    output.Extra1 = 0.0f;    
    return output;		
}


// Techniques
// -------------------------------------------------

technique DEFERRED
{
    pass p0
    {
		AlphaBlendEnable = FALSE;
		
        VertexShader = compile vs_2_0 AnimatedModelVS_LightWithNormal();
        PixelShader = compile ps_2_b animatedModelPS();
    }
}

technique DEFERREDCUSTOM
{
    pass p0
    {
		AlphaBlendEnable = FALSE;
		
        VertexShader = compile vs_2_0 AnimatedModelVS_LightWithNormal();
        PixelShader = compile ps_2_b animatedModelPS_Custom();
    }
}


technique FORWARD
{
    pass p0
    {
		AlphaBlendEnable = FALSE;
		
        VertexShader = compile vs_2_0 AnimatedModelVS_LightWithNormal();
        PixelShader = compile ps_2_b animatedModelPSFORWARD();
    }
}
