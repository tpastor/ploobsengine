float4x4 InvProj;
float4x4 ProjectorViewProjection;
float2 halfPixel;

texture2D ProjectedTexture;
texture2D DepthBuffer;

sampler2D ProjectorSampler = sampler_state
{
	texture = <ProjectedTexture>;
};


sampler2D DepthSampler = 
sampler_state
{
	Texture = <DepthBuffer>;
	MinFilter = POINT;
	MipFilter = POINT;
	MagFilter = POINT;
	
	AddressU = CLAMP;
	AddressV = CLAMP;
};

float2 ProjectionToScreen(float4 position)
{
	float2 screenPos = position.xy / position.w;
	return 0.5f * (float2(screenPos.x, -screenPos.y) + 1);
}


float4 Project(float2 UV)
{
	if (UV.x < 0 || UV.x > 1 || UV.y < 0 || UV.y > 1)
		return float4(0,0,0,1);

	return tex2D(ProjectorSampler, UV);
}


struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 UV 	        : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TextCoord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)  
{  
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
    output.TextCoord = input.UV;
    return output;
}  				

float4	PosFromDepth (float2 UV)
{
	float	Depth=tex2D(DepthSampler,UV).r;	
	float4	Pos=float4((UV.x-0.5)*2,(0.5-UV.y)*2,Depth,1);
	float4	Ray=mul(Pos,InvProj);	
	Ray /= Ray.w;
	return	Ray ;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 pos  = PosFromDepth(input.TextCoord);
	float4 posproj = mul(pos, ProjectorViewProjection);
    return Project(ProjectionToScreen(posproj));
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
