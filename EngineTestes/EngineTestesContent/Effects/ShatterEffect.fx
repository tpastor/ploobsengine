float4x4 World;
float4x4 WorldInverseTranspose;
float4x4 View;
float4x4 Projection;
float specularIntensity;
float specularPower ; 
float specularIntensityScale;
float specularPowerScale ; 
float id;

float RotationAmount;
float TranslationAmount;
float time;


float2 scaleBias;
float3   CameraPos;

const uniform bool useParalax;
const uniform bool useGlow;
const uniform bool useBump;
const uniform bool useSpecular;

texture Texture;
sampler diffuseSampler = sampler_state
{
    Texture = (Texture);
	ADDRESSU = WRAP;
	ADDRESSV = WRAP;	  
};

texture HeightMap;
sampler2D heightSampler = sampler_state
{
	Texture = <HeightMap>;
    ADDRESSU = WRAP;
	ADDRESSV = WRAP;	
};


texture glow;
sampler glowSampler = sampler_state
{
    Texture = (glow);   
	ADDRESSU = WRAP;
	ADDRESSV = WRAP;	     
};

texture SpecularMap;
sampler specularSampler = sampler_state
{
    Texture = (SpecularMap);    
	ADDRESSU = WRAP;
	ADDRESSV = WRAP;	
};


texture NormalMap;
sampler normalSampler = sampler_state
{
    Texture = (NormalMap);    
	ADDRESSU = WRAP;
	ADDRESSV = WRAP;	
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;    
    float3 TriangleCenter : TEXCOORD1;
    float3 RotationalVelocity : TEXCOORD2;        
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
    float3x3 tangentToWorld : TEXCOORD4;
};

float4x4 CreateYawPitchRollMatrix(float x, float y, float z)
{
    float4x4 result;
        
    result[0][0] = cos(z)*cos(y) + sin(z)*sin(x)*sin(y);
    result[0][1] = -sin(z)*cos(y) + cos(z)*sin(x)*sin(y);
    result[0][2] = cos(x)*sin(y);
    result[0][3] = 0;
    
    result[1][0] = sin(z)*cos(x);
    result[1][1] = cos(z)*cos(x);
    result[1][2] = -sin(x);
    result[1][3] = 0;
    
    result[2][0] = cos(z)*-sin(y) + sin(z)*sin(x)*cos(y);
    result[2][1] = sin(z)*sin(y) + cos(z)*sin(x)*cos(y);
    result[2][2] = cos(x)*cos(y);
    result[2][3] = 0;
    
    result[3][0] = 0;
    result[3][1] = 0;
    result[3][2] = 0;
    result[3][3] = 1;    

    return result;
}


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	input.RotationalVelocity *= RotationAmount;
    float4x4 rotMatrix = CreateYawPitchRollMatrix(input.RotationalVelocity.x, 
                                                  input.RotationalVelocity.y, 
                                                  input.RotationalVelocity.z);
    
    // Rotate the vertex around its triangle's center
    float3 position = input.TriangleCenter + mul(input.Position - input.TriangleCenter, 
                                            rotMatrix);
    
    // Displace the vertex along its normal
    position += input.Normal * TranslationAmount;    
    
    // Move the vertex downward as a function of time^2 to give a nice curvy falling
    // effect.
    position.y -= time*time * 200;                 

    float4 worldPosition = mul(float4(position,1), World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                           
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;
    
    if(useBump)
    {    
	    input.Normal = mul(input.Normal, rotMatrix); 
		input.Tangent = mul(input.Tangent, rotMatrix); 
		input.Binormal = mul(input.Binormal, rotMatrix); 

		// calculate tangent space to world space matrix using the world space tangent,
		// binormal, and normal as basis vectors
		output.tangentToWorld[0] = mul(input.Tangent, WorldInverseTranspose);
		output.tangentToWorld[1] = mul(input.Binormal, WorldInverseTranspose);
		output.tangentToWorld[2] = mul(input.Normal, WorldInverseTranspose );    
		output.Normal = 0; 

		
		if(useParalax)
		{	
			float3 vViewWS = CameraPos - worldPosition.xyz;
			output.vViewWS = vViewWS; 			
	    }
    }
    else
    {  
	    input.Normal = mul(input.Normal, rotMatrix); 
		output.Normal =mul(input.Normal,World);                       //get normal into world space   
		
    }
    
    return output;
}
struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 EXTRA1 : COLOR3;
};

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input)
{	
	if(useParalax)
	{
		input.vViewWS = mul(input.vViewWS, input.tangentToWorld);
		float3 h   = normalize( input.vViewWS  );
		float height = tex2D(heightSampler, input.TexCoord ).r;        
        height = height * scaleBias.x + scaleBias.y;
        input.TexCoord = input.TexCoord + (height * h.xy);				
	}

    PixelShaderOutput output;
    output.Color = tex2D(diffuseSampler, input.TexCoord);
    
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
    }
    else
    {
	output.Normal.rgb = 0.5f * (normalize(input.Normal) + 1.0f);               //transform normal domain
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
    
	output.Depth = input.Depth.x / input.Depth.y;                           //output Depth
	output.EXTRA1.a =  id;		
    
    
    return output;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
