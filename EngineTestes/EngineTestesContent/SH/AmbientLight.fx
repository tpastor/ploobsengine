/***********************************************************************************************************************************************

From Xen: Graphics API for XNA and Ravi Ramamoorthi's paper.
License: Microsoft_Permissive_License
Modified by: Schneider, José Ignacio (jis@cs.uns.edu.ar)

************************************************************************************************************************************************/

// Storage for environment spherical harmonic L2
float3	sphericalHarmonicBase[9] : GLOBAL;

// Sample the spherical harmonic L2, outputting linear light
float3 SampleSH(float3 normal)
{
	float3 light = 
		sphericalHarmonicBase[0].xyz + 
		sphericalHarmonicBase[1].xyz * normal.x +  
		sphericalHarmonicBase[2].xyz * normal.y + 
		sphericalHarmonicBase[3].xyz * normal.z + 
		sphericalHarmonicBase[4].xyz * (normal.x * normal.y) +
		sphericalHarmonicBase[5].xyz * (normal.y * normal.z) + 
		sphericalHarmonicBase[6].xyz * (normal.x * normal.z) + 
		sphericalHarmonicBase[7].xyz * ((normal.z * normal.z) - (1.0f / 3.0f)) + 
		sphericalHarmonicBase[8].xyz * ((normal.x * normal.x) - (normal.y * normal.y));
		
	// Clamp to zero	
	return max(0, light);
	
	/*
	// Ravi Ramamoorthi method
	float3 light = 0.429043 * sphericalHarmonicBase[8].xyz * ((normal.x * normal.x) - (normal.y * normal.y)) +
	               0.743125 * sphericalHarmonicBase[6].xyz * (normal.z * normal.z) +
				   0.886227 * sphericalHarmonicBase[0].xyz -
				   0.247708 * sphericalHarmonicBase[6].xyz +
				   2 * 0.429043 * (sphericalHarmonicBase[4] * normal.x * normal.y + sphericalHarmonicBase[7] * normal.x * normal.z +  sphericalHarmonicBase[5] * normal.y * normal.z) +
				   2 * 0.511664 * (sphericalHarmonicBase[3] * normal.x + sphericalHarmonicBase[1] * normal.y + sphericalHarmonicBase[2] * normal.z);
	// Clamp to zero	
	return max(0, light);
	*/
} // SampleSH

//////////////////////////////////////////////
/////////////// Parameters ///////////////////
//////////////////////////////////////////////

float2 halfPixel;

float3x3 viewI;

float intensity = 0.1f;

float ambientOcclusionStrength;

//////////////////////////////////////////////
///////////////// Textures ///////////////////
//////////////////////////////////////////////

texture ambientOcclusionTexture : register(t3);
sampler2D ambientOcclusionSample : register(s3) = sampler_state
{
    Texture = <ambientOcclusionTexture>;
    /*MinFilter = Point;
    MagFilter = Point;
	MipFilter = none;
	AddressU = CLAMP;
	AddressV = CLAMP;*/
};

texture normalTexture : register(t2);
sampler2D normalSample : register(s4) = sampler_state
{
    Texture = <ambientOcclusionTexture>;
    /*MinFilter = Point;
    MagFilter = Point;
	MipFilter = none;
	AddressU = CLAMP;
	AddressV = CLAMP;*/
};

//////////////////////////////////////////////
////////////// Data Structs //////////////////
//////////////////////////////////////////////

struct VS_OUT
{
	float4 position		: POSITION;
	float2 uv			: TEXCOORD0;
};

//////////////////////////////////////////////
////////////// Vertex Shader /////////////////
//////////////////////////////////////////////

VS_OUT vs_main(in float4 position : POSITION, in float2 uv : TEXCOORD)
{
	VS_OUT output = (VS_OUT)0;
	
	output.position = position;
	output.position.xy += halfPixel; // http://drilian.com/2008/11/25/understanding-half-pixel-and-half-texel-offsets/
	output.uv = uv;
	
	return output;
}

//////////////////////////////////////////////
/////////////// Pixel Shader /////////////////
//////////////////////////////////////////////

// This shader works in view space.
float4 ps_mainSH(in float2 uv : TEXCOORD0) : COLOR0
{	
	float3 N = tex2D(normalSample , uv);

	// Normal (view space) to world space
	N = normalize(mul(N, viewI));
			
	return float4(SampleSH(N) * intensity, 0);
} // ps_mainSH

// This shader works in view space.
float4 ps_mainSHSSAO(in float2 uv : TEXCOORD0) : COLOR0
{	
	float3 N = tex2D(normalSample , uv);

	// Normal (view space) to world space
	N = normalize(mul(N, viewI));	

	float ssao = tex2D(ambientOcclusionSample, uv).r;
		
	return float4(SampleSH(N) * intensity * pow(ssao, ambientOcclusionStrength), 0);
} // ps_mainSHSSAO

//////////////////////////////////////////////
//////////////// Techniques //////////////////
//////////////////////////////////////////////

technique AmbientLightSH
{
	pass p0
	{
		VertexShader = compile vs_3_0 vs_main();
		PixelShader  = compile ps_3_0 ps_mainSH();
	}
} // AmbientLightSH

technique AmbientLightSHSSAO
{
	pass p0
	{
		VertexShader = compile vs_3_0 vs_main();
		PixelShader  = compile ps_3_0 ps_mainSHSSAO();
	}
} // AmbientLightSHSSAO