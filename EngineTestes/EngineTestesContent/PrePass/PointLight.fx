float4 LightColor;
float3 LightDir;
float3 LightPosition;
float InvLightRadiusSqr;
float3 FrustumCorners[4];
float2 GBufferPixelSize;
float FarClip;
float4x4 WorldViewProjection;
float2 TanAspect;
float4x4 CameraTransform;

float SpotAngle;
float InvSpotAngle;
float SpotExponent;

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

struct VertexShaderOutputMeshBased
{
    float4 Position : POSITION0;
	float4 TexCoordScreenSpace : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 FrustumRay : TEXCOORD1;
};


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

float2 PostProjectionSpaceToScreenSpace(float4 pos)
{
	float2 screenPos = pos.xy / pos.w;
	return (0.5f * (float2(screenPos.x, -screenPos.y) + 1));
}

float ComputeAttenuation(float3 lDir)
{
	return 1 - saturate(dot(lDir,lDir)*InvLightRadiusSqr);
}



VertexShaderOutputMeshBased PointLightMeshVS(VertexShaderInput input)
{
    VertexShaderOutputMeshBased output = (VertexShaderOutputMeshBased)0;	
    output.Position = mul(input.Position, WorldViewProjection);

	//we will compute our texture coords based on pixel position further
	output.TexCoordScreenSpace = output.Position;
	return output;
}


PixelShaderOutput PointLightMeshPS(VertexShaderOutputMeshBased input)
{
	PixelShaderOutput output = (PixelShaderOutput)0;

	//as we are using a sphere mesh, we need to recompute each pixel position into texture space coords
	float2 screenPos = PostProjectionSpaceToScreenSpace(input.TexCoordScreenSpace) + GBufferPixelSize;
	//read the depth value
	float depthValue = tex2D(depthSampler, screenPos).r;
	
	//if depth value == 1, we can assume its a background value, so skip it
	clip(-depthValue + 0.9999f);
	
    // Reconstruct position from the depth value, the FOV, aspect and pixel position
	depthValue*=FarClip;
	//convert screenPos to [-1..1] range
	float3 pos = float3(TanAspect*(screenPos*2 - 1)*depthValue, -depthValue);
	
	//light direction from current pixel to current light
	float3 lDir = LightPosition - pos;

	//compute attenuation, 1 - saturate(d2/r2)
	float atten = ComputeAttenuation(lDir);
	
	// Convert normal back with the decoding function
	float4 normalMap = tex2D(normalSampler, screenPos);
	float3 normal = DecodeNormal(normalMap);
			
	lDir = normalize(lDir);

	// N dot L lighting term, attenuated
	float nl = saturate(dot(normal, lDir))*atten;

	//reject pixels outside our radius or that are not facing the light
	clip(nl -0.00001f);

	//As our position is relative to camera position, we dont need to use (ViewPosition - pos) here
	float3 camDir = normalize(pos);
	
	//scale by our constant
	//nl*= LightBufferScale;

	// Calculate specular term
	float3 h = normalize(reflect(lDir, normal));
	float spec = nl*pow(saturate(dot(camDir, h)), normalMap.b*100);	
	
	output.Diffuse.rgb = LightColor * nl;
	output.Specular.rgb = (LightColor.a*spec)* LightColor.rgb;

	//output light
	return output;
}

technique PointMeshTechnique
{
    pass PointLight
    {
        VertexShader = compile vs_2_0 PointLightMeshVS();
        PixelShader = compile ps_2_0 PointLightMeshPS();
    }
}
