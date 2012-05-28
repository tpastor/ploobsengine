float3 SampleNormal(float2 uv, sampler2D textureSampler)
{
	float2 normalInformation = tex2Dlod(textureSampler, float4(uv, 0, 0)).xy;
	float3 N;
	
	// Spheremap Transform (not working)
	/*N.z = dot(normalInformation, normalInformation) * 2 - 1; // lenght2 = dot(v, v)
	N.xy = normalize(normalInformation) * sqrt(1 - N.z * N.z);	*/	

	// Spherical Coordinates
	N.xy = -normalInformation * normalInformation + normalInformation;
	N.z = -1;
	float f = dot(N, float3(1, 1, 0.25));
	float m = sqrt(f);
	N.xy = (normalInformation * 8 - 4) * m;
	N.z = -(1 - 8 * f);
	
	// Basic form
	//float3 N = tex2D(normalSampler2, uv).xyz * 2 - 1;
	
	return N; // Already normalized
} // SampleNormal

// Compress the normal using spherical coordinates. This gives us more precision with an acceptable space.
float4 CompressNormal(float3 inputNormal)
{
	float f = inputNormal.z * 2 + 1;
	float g = dot(inputNormal, inputNormal);
	float p = sqrt(g + f);
	return float4(inputNormal.xy / p * 0.5 + 0.5, 1, 1);
	// return float4(normalize(inputNormal.xy) * sqrt(inputNormal.z * 0.5 + 0.5), 1, 1); // Spheremap Transform: Crytek method. 
	// return 0.5f * (float4(inputNormal.xyz, 1) + 1.0f); // Change to the [0, 1] range to avoid negative values.
} // CompressNormal

/// Compress to the (0,1) range with high precision for low values. Guerilla method.
float CompressSpecularPower(float specularPower)
{
	return log2(specularPower) / 10.5;
} // CompressSpecularPower

float DecompressSpecularPower(float compressedSpecularPower)
{
	return pow(2, compressedSpecularPower * 10.5);
} // DecompressSpecularPower
