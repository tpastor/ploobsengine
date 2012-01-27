float intensity  = 2;
float2 halfPixel;

float4x4 VP : ViewProjection;

float3 cameraPosition; 
float SunSize = 2500;
float Density = 3;
float Exposure = -.1;
float Weight = .25;
float Weight2 = .125;
float Decay = .5;

float3 lightPosition;

float lightIntensity = 10.0f;
float3 Color = float3(1,1,1);

texture BackBufferTex;
sampler BackBuffer = sampler_state
{
    Texture = (BackBufferTex);
    AddressU = CLAMP;
    AddressV = CLAMP;
};

texture flare;
sampler Flare = sampler_state
{
    Texture = (flare);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;

};


//
texture Extra1;
sampler ExtraSampler = sampler_state
{
    Texture = (Extra1);   
};


//depth
texture depthMap;
sampler depthSampler = sampler_state
{
    Texture = (depthMap);   
	MagFilter = POINT;
    MinFilter = POINT;
    Mipfilter = POINT;
};



texture colorMap;
sampler colorSampler = sampler_state
{
    Texture = (colorMap);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};

texture glowMapBlurried;
sampler glowSampler = sampler_state
{
    Texture = (glowMapBlurried);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 texCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 texCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = float4(input.Position,1);
    output.texCoord = input.texCoord - halfPixel;    
    return output;
}


float4 DoLenseFlare(float4 ScreenLightPosition,float2 texCoord,bool fwd)
{
	// Calculate vector from pixel to light source in screen space.  
	float2 deltaTexCoord = (texCoord - ScreenLightPosition.xy);  
	
	// Divide by number of samples and scale by control factor.  
	deltaTexCoord *= 1.0f / 3 * Density;  
	
	// Store initial sample.  
	float3 color = 0;
	
	// Set up illumination decay factor.  
	float illuminationDecay = 1.0f;  
		
	for (int i = 0; i < 3 ; i++)  
	{  	
		// Step sample location along ray.  
		if(fwd)
			texCoord -= deltaTexCoord;  
		else
			texCoord += deltaTexCoord;  
		// Retrieve sample at new location.  
		float3 sample = tex2D(Flare, texCoord);
		
		// Apply sample attenuation scale/decay factors.  
		if(fwd)
			sample *= illuminationDecay * Weight;  
		else
			sample *= illuminationDecay * Weight2;			
		
		// Accumulate combined color.  
		color += sample;  
		// Update exponential decay factor.  
		illuminationDecay *= Decay;
	}  

	return float4(color,1);
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	
  // Get the scene
	float4 col = tex2D(BackBuffer,input.texCoord);
	
	// Find the suns position in the world and map it to the screen space.
	float4 ScreenPosition = mul(lightPosition - cameraPosition,VP);
	float scale = ScreenPosition.z;
	ScreenPosition.xyz /= ScreenPosition.w;
	///passa do espaco [-1,1] para [0,1]
	ScreenPosition.x = ScreenPosition.x/2.0f+0.5f;
	ScreenPosition.y = (-ScreenPosition.y/2.0f+0.5f);
	
	///nao sobrescreve modelos que nao estao no pano de fundo	
	int flag = round(tex2D(ExtraSampler,input.texCoord).a * 255 );	
	bool isBackGround = fmod(flag, 4) >= 2; 

	if(isBackGround)
	{
	   return tex2D(BackBuffer, input.texCoord);
	}
	else
	{
	
	// Are we lokoing in the direction of the sun?
	if(ScreenPosition.w > 0)
	{		
		float2 coord;
		
		float size = SunSize / scale;
					
		float2 center = ScreenPosition.xy;

		coord = .5 - (input.texCoord - center) / size * .5;
		
		//if(depthVal > ScreenPosition.z-.0003)
			col += (pow(tex2D(Flare,coord) * float4(Color,1),2) * lightIntensity) * 2;				
		
		// Lens flare
		col += ((DoLenseFlare(ScreenPosition,input.texCoord,true) + DoLenseFlare(ScreenPosition,input.texCoord,false)) * float4(Color,1) * lightIntensity) * 5;
	}
	
	return col;	
	}
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}


