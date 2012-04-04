float3 lightDirection;
float  lightIntensity = 1;
float3 Color; 
float3 FrustumCorners[4];

sampler normalSampler : register(s1);
sampler depthSampler : register(s2);

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
	float3 PosView : TEXCOORD1; 
};

float2 halfPixel;
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{    
	VertexShaderOutput output;
	
	float index = input.TexCoord.x + (input.TexCoord.y * 2);
	output.PosView = FrustumCorners[index];

	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
	
    //align texture coordinates
    output.TexCoord = input.TexCoord ;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{	
	//get normal data from the normalMap
    float4 normalData = tex2D(normalSampler,input.TexCoord);		
    //tranform normal back into [-1,1] range
    float3 normal = 2.0f * normalData.xyz - 1.0f;
    
    //read depth
    float depthVal = tex2D(depthSampler,input.TexCoord).r;
	    
    float3 position = input.PosView * depthVal;   	
	
    //surface-to-light vector
    float3 lightVector = -normalize(lightDirection);

    //compute diffuse light
    float NdL = max(0,dot(normal,lightVector));
    float3 diffuseLight = NdL * Color.rgb;

    //reflexion vector
    float3 reflectionVector = normalize(reflect(-lightVector, normal));
    //camera-to-surface vector
    float3 directionToCamera = normalize(position);
    //compute specular light
    float specularLight = saturate(dot(reflectionVector, directionToCamera));

    //output the two lights
    return float4(diffuseLight.rgb * lightIntensity , specularLight) ;
}

technique Technique0
{
    pass Pass0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}