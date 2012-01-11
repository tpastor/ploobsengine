//-----------------------------------------
// Parameters
//-----------------------------------------
float4 LightColor;
float3 LightDir;
float3 FrustumCorners[4];
float2 GBufferPixelSize;


//we use this to avoid clamping our results into [0..1]. 
//this way, we can fake a [0..10] range, since we are using a
//floating point buffer
const static float LightBufferScale = 0.1f;

//-----------------------------------------
// Textures
//-----------------------------------------
texture DepthBuffer;
sampler2D depthSampler = sampler_state
{
	Texture = <DepthBuffer>;
	MipFilter = NONE;
	MagFilter = POINT;
	MinFilter = POINT;
	AddressU = Clamp;
	AddressV = Clamp;
};

texture NormalBuffer;
sampler2D normalSampler = sampler_state
{
	Texture = <NormalBuffer>;
	MipFilter = NONE;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};

//-------------------------------
// Helper functions
//-------------------------------
half3 DecodeNormal (half4 enc)
{
	float kScale = 1.7777;
	float3 nn = enc.xyz*float3(2*kScale,2*kScale,0) + float3(-kScale,-kScale,1);
	float g = 2.0 / dot(nn.xyz,nn.xyz);
	float3 n;
	n.xy = g*nn.xy;
	n.z = g-1;
	return n;
}

float3 GetFrustumRay(in float2 texCoord)
{
	float index = texCoord.x + (texCoord.y * 2);
	return FrustumCorners[index];
}


//-------------------------------
// Shaders
//-------------------------------

struct PixelShaderOutput
{
    float4 Diffuse : COLOR0;
    float4 Specular : COLOR1;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 FrustumRay : TEXCOORD1;
};

VertexShaderOutput DirectionalLightVS(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	output.Position = input.Position;
	output.TexCoord = input.TexCoord +GBufferPixelSize;
	output.FrustumRay = GetFrustumRay(input.TexCoord);
	return output;
}


PixelShaderOutput DirectionalLightPS(VertexShaderOutput input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	// If we want the WorldPosition, we have to multiply by the world camera matrix
	float depthValue = tex2D(depthSampler, input.TexCoord).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);

    float3 pos = input.FrustumRay * depthValue;
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler,  input.TexCoord);
	float3 normal = DecodeNormal(normalMap);

	float nl = saturate(dot(normal, LightDir));
	
	clip(nl - 0.00001f);
	
	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
		
	//scale by our constant
	nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(LightDir, normal)); 
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}

//tech 2
technique DirectionalTechnique
{
    pass DirectionalLight
    {
        VertexShader = compile vs_2_0 DirectionalLightVS();
        PixelShader = compile ps_2_0 DirectionalLightPS();
    }
}

