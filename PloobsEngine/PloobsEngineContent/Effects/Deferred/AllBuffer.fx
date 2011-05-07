float4x4 World;
float4x4 WorldInverseTranspose;
float4x4 View;
float4x4 Projection;
float specularIntensity;
float specularPower ; 
float specularIntensityScale;
float specularPowerScale ; 
float id;

float2 scaleBias;
float3   CameraPos;

const uniform bool useParalax;
const uniform bool useGlow;
const uniform bool useBump;
const uniform bool useSpecular;

texture HeightMap;
sampler2D heightSampler = sampler_state
{
	Texture = <HeightMap>;
    ADDRESSU = WRAP;
	ADDRESSV = WRAP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
};


texture glow;
sampler glowSampler = sampler_state
{
    Texture = (glow);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};


texture Texture;
sampler diffuseSampler = sampler_state
{
    Texture = (Texture);
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
    MIPFILTER = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};


texture SpecularMap;
sampler specularSampler = sampler_state
{
    Texture = (SpecularMap);
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};


texture NormalMap;
sampler normalSampler = sampler_state
{
    Texture = (NormalMap);
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};


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
    float3x3 tangentToWorld : TEXCOORD4;
    
    
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
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

		
		if(useParalax)
		{	
			float3 vViewWS = CameraPos - worldPosition.xyz;
			output.vViewWS = vViewWS; 			
	    }
    }
    else
    {  
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
