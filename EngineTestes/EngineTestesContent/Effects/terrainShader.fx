//////////////////////////////////////////////////////
//Various Camera Matrices. As usual, these should be set once every frame in XNA.
float4x4 World;
float4x4 View;
float4x4 Projection;

//////////////////////////////////////////////////////
//The direction vector the terrain will be lit from
float3 sunlightVector = float3(.5,.5,.8);
//The Colour (and brightness) of the Sunlight
float3 lightColour = float3(2,2,2);
//The colour (and birghtness) of the ambient light
float3 ambientColour = float3(.1,.1,.1);

bool HighQualityDetailMapping = true;

//////////////////////////////////////////////////////
//The sizes of the various terrain UV Coordinates
float detailScale = 1;
float diffuseScale = 5;
float globalScale;

//////////////////////////////////////////////////////
//The textures
texture BlendTexture; // Blending. Global.
texture NormalTexture; // Normals. Global.

texture BaseTexture; // Diffuse. If the Blend texture is black
texture RTexture; // Diffuse. If the Blend texture is red
texture GTexture; // Diffuse. If the Blend texture is green
texture BTexture; // Diffuse. If the Blend texture is blue
//texture ATexture; // Diffuse. If the Blend texture is alpha. Currently obsolete.

texture detailTexture; // Normals. Detail map.
float detailMapStrength = 1;

//////////////////////////////////////////////////////
//The texture samplers
//////////////////////////////////////////////////////
sampler BlendSampler = 
sampler_state
{
  Texture = <BlendTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
sampler NormalSampler = 
sampler_state
{
  Texture = <NormalTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
//////////////////////////////////////////////////////

sampler RSampler = 
sampler_state
{
  Texture = <RTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
sampler GSampler = 
sampler_state
{
  Texture = <GTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
sampler BSampler = 
sampler_state
{
  Texture = <BTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
sampler BaseSampler = 
sampler_state
{
  Texture = <BaseTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};
//////////////////////////////////////////////////////

sampler DetailSampler = 
sampler_state
{
  Texture = <detailTexture>;
  MipFilter = LINEAR;
  MinFilter = LINEAR;
  MagFilter = LINEAR;
  MaxAnisotropy = 2;
  AddressU = wrap; 
  AddressV = wrap;
};

//////////////////////////////////////////////////////

struct VertexShaderInput
{
    float4 Position : POSITION0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TextureUV  : TEXCOORD0;
};
//////////////////////////////////////////////////////
//VERTEX SHADER
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
	
	//Create vertex Position and UV Coordinates
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	output.TextureUV[0] = input.Position.x/globalScale;
	output.TextureUV[1] = input.Position.z/globalScale;
	
    return output;
}
//////////////////////////////////////////////////////
//PIXEL SHADER
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 Output;
	//Get Global Normal from the full terrain normal map
	float3 Normal = tex2D(NormalSampler, input.TextureUV);
	Normal[0] -= .5;
	Normal[1] -= .5;
	Normal[2] -= .5;
	Normal = normalize(Normal);
	
	
	if(detailMapStrength>0)
	{
	
		//Get Detail Normal from the detail map
		float3 detailNormalMap = (tex2D(DetailSampler, input.TextureUV*100/detailScale));
		detailNormalMap[0] -= .5;
		detailNormalMap[1] -= .5;
		detailNormalMap[2] -= .5;
		//Multiply Detail Normal by detailMapStrength
		detailNormalMap[0] = mul(detailNormalMap[0], detailMapStrength);
		detailNormalMap[1] = mul(detailNormalMap[1], detailMapStrength);
	
		//Normalize detail Normal
		detailNormalMap = normalize(detailNormalMap);
		
		if(HighQualityDetailMapping == true)
		{
			//Generate the Tangent Basis for the Detail Normal Map.
			float3x3 tangentBasis;
			
			tangentBasis[0] = cross(Normal, float3(1,0,0));
			tangentBasis[1] = cross(Normal, tangentBasis[0]);
			tangentBasis[2] = Normal;
			
			detailNormalMap = detailNormalMap, detailMapStrength;
		    
			Normal = mul(detailNormalMap, tangentBasis);
			Normal = normalize(Normal);
		} 
		else
		{
			Normal = normalize(Normal*2+detailNormalMap*detailMapStrength);
		}
	}
	 
    sunlightVector = normalize(sunlightVector);    
	float3 lightLevel;
	lightLevel[0] = max(dot(Normal, sunlightVector), 0)*lightColour[0]*2+ambientColour[0];
	lightLevel[1] = max(dot(Normal, sunlightVector), 0)*lightColour[1]*2+ambientColour[1];
	lightLevel[2] = max(dot(Normal, sunlightVector), 0)*lightColour[2]*2+ambientColour[2];
	
    float3 blender = tex2D(BlendSampler, input.TextureUV);
    
    //MultiTexturing
    float RWeight = blender[0];
    float GWeight = blender[1];
    float BWeight = blender[2];    
    float BaseWeight = 1-((RWeight+BWeight+GWeight));
    
    float3 RColour = tex2D(RSampler, input.TextureUV*100/diffuseScale);
    float3 GColour = tex2D(GSampler, input.TextureUV*100/diffuseScale);
    float3 BColour = tex2D(BSampler, input.TextureUV*100/diffuseScale);
    float3 BaseColour = tex2D(BaseSampler, input.TextureUV*100/diffuseScale);
    
    float3 diffuse = (((RColour*RWeight)+(BColour*BWeight)+(GColour*GWeight)+(BaseColour*BaseWeight))/4);
	
	Output =  float4(diffuse[0]*lightLevel[0], diffuse[1]*lightLevel[1], diffuse[2]*lightLevel[2], 1);
	
    return Output;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.
        AlphaBlendEnable = FALSE;
		ZEnable = TRUE;
		CullMode = CW; 
		
		
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
