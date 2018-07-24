float4x4 World;
float4x4 WorldInverseTranspose;
float4x4 View;
float4x4 Projection;
float specularIntensity;
float specularPower ; 
float specularIntensityScale;
float specularPowerScale ; 
float id;
float reflectionIndex;
float3   CameraPos;

const uniform bool useGlow;
const uniform bool useBump;
const uniform bool useSpecular;

sampler diffuseSampler : register(s0);
sampler normalSampler : register(s1);
sampler specularSampler : register(s2);
sampler glowSampler : register(s3);
samplerCUBE map_diffuse : register(s4);

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;
    float3 Binormal : BINORMAL0;
    float3 Tangent : TANGENT0;

};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;    
    float2 Depth : TEXCOORD1;
    float3 Normal : TEXCOORD2;
    float3 vViewWS           : TEXCOORD3;     
	float4 Pos : TEXCOORD4;
    float3x3 tangentToWorld : TEXCOORD5;   
    
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput )0;

    float4 worldPosition = mul(input.Position, World);
	output.Pos = worldPosition;
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                            //pass the texture coordinates further    
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;
    
    
    if(useBump)
    {    
		// calculate tangent space to world space matrix using the world space tangent,
		// binormal, and normal as basis vectors
		output.tangentToWorld[0] = mul(input.Tangent, WorldInverseTranspose);
		output.tangentToWorld[1] = mul(input.Binormal, WorldInverseTranspose);
		output.tangentToWorld[2] = mul(input.Normal, WorldInverseTranspose );    
		output.Normal = 0; 

    }
    else
    {  
		output.Normal =mul(input.Normal,World);                       //get normal into world space   
		
    }
	
    return output;
}
struct PixelShaderOutput
{
    float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Depth : COLOR2;
    float4 EXTRA1 : COLOR3;
};

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{	
	
    PixelShaderOutput output;
	
    if(useBump)
    {       
	// read the normal from the normal map
    float3 normalFromMap = tex2D(normalSampler, input.TexCoord);
    //tranform to [-1,1]
    normalFromMap = 2.0f * normalFromMap - 1.0f;
    //transform into world space
    normalFromMap = mul(normalFromMap, input.tangentToWorld);
    //normalize the result
    normalFromMap = normalize(normalFromMap);
    //output the normal, in [0,1] space
    output.Normal.rgb = 0.5f * (normalFromMap + 1.0f);

	float3 refl = reflect(normalize(input.Pos - CameraPos),normalFromMap);       
    output.Color = reflectionIndex * texCUBE(map_diffuse,refl) + (1 - reflectionIndex)* tex2D(diffuseSampler, input.TexCoord);   

    }
    else
    {
	float3 nnormal = normalize(input.Normal);
	float refl = reflect(normalize(input.Pos - CameraPos),nnormal);           
	output.Color = reflectionIndex * texCUBE(map_diffuse,refl) + (1 - reflectionIndex)* tex2D(diffuseSampler, input.TexCoord);   
	output.Normal.rgb = 0.5f * (nnormal + 1.0f);             
    }
    

	if(useSpecular)
	{
	    float4 specularAttributes = tex2D(specularSampler, input.TexCoord);  
		//specular Intensity
		output.Color.a = specularAttributes.r * specularIntensityScale;
		//specular Power
		output.Normal.a = specularAttributes.a * specularPowerScale;  
    }
    else
    {
		//specular Intensity
		output.Color.a = specularIntensity;
		//specular Power
		output.Normal.a = specularPower;
    }
    
     if(useGlow)
     {
     output.EXTRA1 = output.Color * tex2D(glowSampler, input.TexCoord);  
     }
     else
     {   
	 output.EXTRA1 =  0;  
     }    
    
	output.Depth = 1-input.Depth.x / input.Depth.y;                           //output Depth
	output.EXTRA1.a =  id / 255.0f;		
    
    
    return output;
}




technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexShaderFunction();
        PixelShader = compile ps_4_0 PixelShaderFunction();
    }
}
