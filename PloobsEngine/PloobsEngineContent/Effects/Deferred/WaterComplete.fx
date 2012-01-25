float4x4 World;
float4x4 View;
float4x4 Projection;
float4x4 ReflectionView;
float4x4 RefractionView;
float specularIntensity = 0.8f;
float specularPower = 0.5f; 
float xWaveLength;
float xWaveHeight;
float3 camPos;
float Time;
float WindForce;
float3 WindDirection; 
float id;
static const float	  R0 = 0.02037f;
float4 waterColor;

Texture normalMap0;
sampler NormalSampler0 = sampler_state 
{ 
	texture = <normalMap0> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = wrap; 
	AddressV = wrap;
};

Texture normalMap1;
sampler NormalSampler1 = sampler_state 
{ 
	texture = <normalMap1> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = wrap; 
	AddressV = wrap;
};

Texture RefractionMap;
sampler RefractionSampler = sampler_state 
{ 
	texture = <RefractionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
};

Texture ReflectionMap;
sampler ReflectionSampler = sampler_state 
{ 
	texture = <ReflectionMap> ; 
	magfilter = LINEAR; 
	minfilter = LINEAR; 
	mipfilter = LINEAR; 
	AddressU = Clamp; 
	AddressV = Clamp;
};


struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TexCoord : TEXCOORD0;    
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float2 Depth : TEXCOORD2;    
    float4 ReflectionMapSamplingPos : TEXCOORD3;
    float4 RefractionMapSamplingPos : TEXCOORD4;
    float3 Pos3D : TEXCOORD5;
    float2 BumpMapSamplingPos  : TEXCOORD6;
    float3 toEyeW : TEXCOORD7;
    
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;                             
    output.Normal = mul(input.Normal,World);                       
    output.Depth.x = output.Position.z;
    output.Depth.y = output.Position.w;   
    worldPosition.w = 1;
    output.Pos3D = worldPosition;
    
    output.toEyeW = worldPosition - camPos;
    
  	float4x4 preReflectionViewProjection = mul (ReflectionView, Projection);
    float4x4 preWorldReflectionViewProjection = mul (World, preReflectionViewProjection);    
	output.ReflectionMapSamplingPos = mul(input.Position, preWorldReflectionViewProjection);
	
	float4x4 preRefractionViewProjection = mul (RefractionView, Projection);
    float4x4 preWorldRefractionViewProjection = mul (World, preRefractionViewProjection);    
	output.RefractionMapSamplingPos = mul(input.Position, preWorldRefractionViewProjection);	
	
	float3 windDir = normalize(WindDirection);    
	float3 perpDir = cross(WindDirection, float3(0,1,0));
	float ydot = dot(input.TexCoord , windDir.xz);
	float xdot = dot(input.TexCoord , perpDir.xz);
	float2 moveVector = float2(xdot, ydot);
	moveVector.y += Time*WindForce;    
	moveVector.x += Time*WindForce/3;    
	output.BumpMapSamplingPos = moveVector/xWaveLength;
	
    return output;
}

struct PixelShaderOutput
{
    half4 Color : COLOR0;
    half4 Normal : COLOR1;
    half4 Depth : COLOR2;
    half4 LightOcclusion : COLOR3;
};


PixelShaderOutput PixelShaderFunctionReflexiveSurface(VertexShaderOutput input)
{
	float2 ProjectedTexCoordsReflection;
    ProjectedTexCoordsReflection.x = input.ReflectionMapSamplingPos.x/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoordsReflection.y = -input.ReflectionMapSamplingPos.y/input.ReflectionMapSamplingPos.w/2.0f + 0.5f;

	float2 ProjectedTexCoordsRefraction;
    ProjectedTexCoordsRefraction.x = input.RefractionMapSamplingPos.x/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
    ProjectedTexCoordsRefraction.y = -input.RefractionMapSamplingPos.y/input.RefractionMapSamplingPos.w/2.0f + 0.5f;
    
    float4 n1 = tex2D(NormalSampler1, input.BumpMapSamplingPos);
    float4 bumpColor = tex2D(NormalSampler0, input.BumpMapSamplingPos);
    float2 perturbation = xWaveHeight*(bumpColor.rg - 0.5f)*2.0f;           
    float4 n2 = bumpColor;
    
    //unroll the normals retrieved from the normalmaps
    n1.yz = n1.zy;	
	n2.yz = n2.zy;
	
	n1= 2.0f*n1- 1.0f;
    n2= 2.0f*n2- 1.0f;
    float3 n = normalize(0.5f*(n1 + n2));    
     
    PixelShaderOutput output;   
    input.Normal = normalize(input.Normal);
    output.Normal.rgb = 0.5f * (input.Normal + 1.0f);               
    output.Normal.a = specularPower;                                
    output.Depth = input.Depth.x / input.Depth.y;                   
	output.LightOcclusion =  0;           
	output.LightOcclusion.a =  id / 255.0f;           	 
    
    float4 reflectiveColor = tex2D(ReflectionSampler, ProjectedTexCoordsReflection + perturbation );
    float4 refractiveColor = tex2D(RefractionSampler, ProjectedTexCoordsRefraction + perturbation );    
    
    input.toEyeW = normalize(input.toEyeW);
    float3 R = normalize(reflect(input.toEyeW,n));
    
	float ang = saturate(dot(-input.toEyeW,n));
	float f = R0 + (1.0f-R0) * pow(1.0f-ang,5.0);		
	f = min(1.0f, f + 0.002f * camPos.y);	
	
    if(camPos.y < input.Pos3D.y)
		f = 0.0f;
    
    float3 lv = -float3(2.6f, -1.0f, -1.5f);
    lv = normalize(lv);
	float4 sunlight = 2.5f * pow(saturate(dot(R,lv)), 150) ;
	    
    float4 combinedColor = lerp(refractiveColor, reflectiveColor, f);	    
    output.Color  = waterColor * combinedColor + sunlight;    
    output.Color.a = specularIntensity;  
    return output;        
}


technique ReflexiveSurface
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunctionReflexiveSurface();
    }
}
