//--------------------------------------------------------------------------------
// Name		: Water.fx
// Desc		: Rippling reflective water (we don't need refractions).
// Author	: Anirudh S Shastry. Copyright (c) 2004.
// Date		: June 27th, 2004.
//--------------------------------------------------------------------------------
//

float fTime	: Time;
float id;
 

//--------------------------------------------------
// Global variables
//--------------------------------------------------
float4x4 matWorldViewProj 	: WorldViewProjection;
float4x4 matWorld 			: World;
float4x4 matWorldView		: WorldView;
float4x4 matViewI 			: ViewInverse;

float fBumpHeight
<
	string UIName 	= "Bump Height";
	string UIWidget = "Slider";
	float  UIMin 	= 0.0f;
	float  UIMax 	= 2.0f;
	float  UIStep 	= 0.1f;
> = 0.5f;

float2 vTextureScale
<
    string UIName 	= "Texture Scale";
	string UIWidget = "Vector";
> = { 4.0f, 4.0f };

float2 vBumpSpeed
<
    string UIName 	= "Bump Speed";
	string UIWidget = "Vector";
> = { -0.0f, 0.05f };

float fFresnelBias
<
    string UIName 	= "Fresnel Bias";
	string UIWidget = "Slider";
	float  UIMin 	= 0.0f;
	float  UIMax 	= 1.0f; 
	float  UIStep 	= 0.1f;
> = 0.025f;

float fFresnelPower
<
    string UIName 	= "Fresnel Exponent";
	string UIWidget = "Slider";
	float  UIMin 	= 1.0f;
	float  UIMax 	= 10.0f;
	float  UIStep 	= 0.1f;
> = 1.0f;

float fHDRMultiplier // To exaggerate HDR glow effects!
<
    string UIName 	= "HDR Multiplier";
	string UIWidget = "Slider";
	float  UIMin 	= 0.0f;
	float  UIMax 	= 100.0f;
	float  UIStep 	= 1.0f;
> = 1.0f;

float4 vDeepColor : Diffuse
<
    string UIName 	= "Deep Water Color";
	string UIWidget = "Vector";
> = { 0.0f, 0.40f, 0.50f, 1.0f };

float4 vShallowColor : Diffuse
<
    string UIName 	= "Shallow Water Color";
	string UIWidget = "Vector";
> = { 0.55f, 0.75f, 0.75f, 1.0f };

float4 vReflectionColor : Diffuse
<
    string UIName 	= "Reflection Color";
	string UIWidget = "Vector";
> = { 1.0f, 1.0f, 1.0f, 1.0f };

float fReflectionAmount
<
    string UIName 	= "Reflection Amount";
	string UIWidget = "Slider";    
	float  UIMin 	= 0.0f;
	float  UIMax 	= 2.0f;
	float  UIStep 	= 0.1f;    
> = 0.5f;

float fWaterAmount
<
    string UIName 	= "Water Color Amount";
	string UIWidget = "Slider";    
	float  UIMin 	= 0.0f;
	float  UIMax 	= 2.0f;
	float  UIStep 	= 0.1f;    
> = 0.5f;

float fWaveAmp
<
    string UIName 	= "Wave Amplitude";
	string UIWidget = "Slider";
	float  UIMin 	= 0.0f;
	float  UIMax 	= 10.0f;
	float  UIStep 	= 0.1f;
> = 0.5f;

float fWaveFreq
<
    string UIName 	= "Wave Frequency";
	string UIWidget = "Slider";
	float  UIMin 	= 0.0f;
	float  UIMax 	= 1.0f;
	float  UIStep 	= 0.001f;
> = 0.1f;

texture tNormalMap : Normal
<
	string UIName	    = "Normal Map";
	string ResourceName = "waves2.dds";
	string TextureType = "2D";
>;

texture tEnvMap : Environment
<
	string UIName	    = "Environment Map";
	string ResourceName = "CloudyHillsCubemap2.dds";
	string TextureType 	= "Cube";
>;

sampler2D s0 = sampler_state {
	Texture = (tNormalMap);
	
	MipFilter 	= Linear;
	MinFilter 	= Linear;
	MagFilter 	= Linear;
};

samplerCUBE s1 = sampler_state {
	Texture = (tEnvMap);
	
	MinFilter 	= Linear;
	MagFilter 	= Linear;
	MipFilter 	= Linear;
};

//--------------------------------------------------
// Vertex shader
//--------------------------------------------------
struct VSOUTPUT {
	float4 vPos				: POSITION;
	float2 vTex				: TEXCOORD0;
	float3 vTanToCube[3]	: TEXCOORD1;
	float2 vBump0			: TEXCOORD4;
	float2 vBump1			: TEXCOORD5;
	float2 vBump2			: TEXCOORD6;
	float3 vView			: TEXCOORD7;
	float2 Depth			: TEXCOORD8;	
};

// Wave
struct Wave {
	float	fFreq;	// Frequency (2PI / Wavelength)
	float	fAmp;	// Amplitude
	float	fPhase;	// Phase (Speed * 2PI / Wavelength)
	float2	vDir;	// Direction
};

#define NUMWAVES	3

// Wave functions
float EvaluateWave( Wave w, float2 vPos, float fTime ) {
	return w.fAmp * sin( dot( w.vDir, vPos ) * w.fFreq + fTime * w.fPhase );
}

float EvaluateWaveDifferential( Wave w, float2 vPos, float fTime ) {
	return w.fAmp * w.fFreq * cos( dot( w.vDir, vPos ) * w.fFreq + fTime * w.fPhase );
}

float EvaluateWaveSharp( Wave w, float2 vPos, float fTime, float fK )
{
  return w.fAmp * pow( sin( dot( w.vDir, vPos ) * w.fFreq + fTime * w.fPhase )* 0.5 + 0.5 , fK );
}

float EvaluateWaveSharpDifferential( Wave w, float2 vPos, float fTime, float fK )
{
  return fK * w.fFreq * w.fAmp * pow( sin( dot( w.vDir, vPos ) * w.fFreq + fTime * w.fPhase )* 0.5 + 0.5 , fK - 1 ) * cos( dot( w.vDir, vPos ) * w.fFreq + fTime * w.fPhase );
}

VSOUTPUT VS_Water( float4 inPos : POSITION, float3 inNor : NORMAL, float2 inTex : TEXCOORD0,
				   float3 inTan : TANGENT, float3 inBin : BINORMAL ) {
	VSOUTPUT OUT = (VSOUTPUT)0;
	
	static Wave Waves[NUMWAVES] = {
	{ 1.0f, 1.00f, 0.50f, float2( -1.0f, 0.0f ) },
	{ 2.0f, 0.50f, 1.30f, float2( -0.7f, 0.7f ) },
	{ .50f, .50f, 0.250f, float2( 0.2f, 0.1f ) },
	};


	// Generate some waves!
    Waves[0].fFreq 	= fWaveFreq;
    Waves[0].fAmp 	= fWaveAmp;

    Waves[1].fFreq 	= fWaveFreq * 2.0f;
    Waves[1].fAmp 	= fWaveAmp * 0.5f;
    
    Waves[2].fFreq 	= fWaveFreq * 3.0f;
    Waves[2].fAmp 	= fWaveAmp * 1.0f;

	// Sum up the waves
	inPos.y = 0.0f;
	float ddx = 0.0f, ddy = 0.0f;
	
	for( int i = 0; i < NUMWAVES; i++ ) {
    	inPos.y += EvaluateWave( Waves[i], inPos.xz, fTime );
    	float diff = EvaluateWaveDifferential( Waves[i], inPos.xz, fTime);
    	ddx += diff * Waves[i].vDir.x;
    	ddy += diff * Waves[i].vDir.y;
    }

	// Output the position
	OUT.vPos = mul( inPos, matWorldViewProj );
	OUT.Depth.x = OUT.vPos.z;
    OUT.Depth.y = OUT.vPos.w;
	
	// Generate the normal map texture coordinates
	OUT.vTex = inTex * vTextureScale;

	float fTimeM = fmod( fTime, 100.0 );
	OUT.vBump0 = inTex * vTextureScale + fTimeM * vBumpSpeed;
	OUT.vBump1 = inTex * vTextureScale * 2.0f + fTimeM * vBumpSpeed * 4.0;
	OUT.vBump2 = inTex * vTextureScale * 4.0f + fTimeM * vBumpSpeed * 8.0;

	// Compute tangent basis
    float3 vB = float3( 1,  ddx, 0 );
    float3 vT = float3( 0,  ddy, 1 );
    float3 vN = float3( -ddx, 1, -ddy );

	// Compute the tangent space to object space matrix
	float3x3 matTangent = float3x3( fBumpHeight * normalize( vT ),
									fBumpHeight * normalize( vB ),
									normalize( vN ) );
	
	OUT.vTanToCube[0] = mul( matTangent, matWorld[0].xyz );
	OUT.vTanToCube[1] = mul( matTangent, matWorld[1].xyz );
	OUT.vTanToCube[2] = mul( matTangent, matWorld[2].xyz );

	// Compute the world space vector
	float4 vWorldPos = mul( inPos, matWorld );
	OUT.vView = matViewI[3].xyz - vWorldPos;
	
	return OUT;
}

//--------------------------------------------------
// Pixel shader
//--------------------------------------------------
float3 Refract( float3 vI, float3 vN, float fRefIndex, out bool fail )
{
	float fIdotN = dot( vI, vN );
	float k = 1 - fRefIndex * fRefIndex * ( 1 - fIdotN * fIdotN );
	fail = k < 0;
	return fRefIndex * vI - ( fRefIndex * fIdotN + sqrt(k) )* vN;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 LightOcclusion : COLOR3;
};

PixelShaderOutput PS_Water( VSOUTPUT IN ) : COLOR0 {


	PixelShaderOutput output ;
	// Fetch the normal maps (with signed scaling)
    float4 t0 = tex2D( s0, IN.vBump0 ) ;
    float4 t1 = tex2D( s0, IN.vBump1 ) ;//* 2.0f - 1.0f;
    float4 t2 = tex2D( s0, IN.vBump2 ) ;//* 2.0f - 1.0f;

    float3 vN = t0.xyz + t1.xyz + t2.xyz;   	

	// Compute the tangent to world matrix
    float3x3 matTanToWorld;
    
    matTanToWorld[0] = IN.vTanToCube[0];
    matTanToWorld[1] = IN.vTanToCube[1];
    matTanToWorld[2] = IN.vTanToCube[2];
    
    float3 vWorldNormal = mul( matTanToWorld, vN );
    vWorldNormal = normalize( vWorldNormal );

	// Compute the reflection vector
    IN.vView = normalize( IN.vView );
    float3 vR = reflect( -IN.vView, vWorldNormal );
	
	// Sample the cube map
    float4 vReflect = texCUBE( s1, vR.zyx );    
    vReflect = texCUBE( s1, vR );
    
    // Exaggerate the HDR effect
    vReflect.rgb *= ( 1.0 + vReflect.a * fHDRMultiplier );

	// Compute the Fresnel term
    float fFacing  = 1.0 - max( dot( IN.vView, vWorldNormal ), 0 );
    float fFresnel = fFresnelBias + ( 1.0 - fFresnelBias ) * pow( fFacing, fFresnelPower);

	// Compute the final water color
    float4 vWaterColor = lerp( vDeepColor, vShallowColor, fFacing );

	output.Color = vWaterColor * fWaterAmount + vReflect * vReflectionColor * fReflectionAmount * fFresnel;
	output.Color.a = 0.01; 
	output.Depth = IN.Depth.x / IN.Depth.y;                           //output Depth
	output.Normal.rgb = 0.5f * (normalize(vWorldNormal ) + 1.0f);
	output.Normal.a = 0.01;
	output.LightOcclusion.rgb = 0;
	output.LightOcclusion.a = id;
	return output;
}

//--------------------------------------------------
// Techniques
//--------------------------------------------------
technique techDefault {
	pass p0 {
		CullMode		= None;
	
		VertexShader	= compile vs_3_0 VS_Water();
		PixelShader		= compile ps_3_0 PS_Water();
	}
}
