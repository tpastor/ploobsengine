texture depth;
texture color;
texture project;

sampler depthSampler = sampler_state
{
    Texture = (depth);
	AddressU = CLAMP;
    AddressV = CLAMP;	
};

sampler colorSampler = sampler_state
{
    Texture = (color);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};

sampler projectSampler = sampler_state
{
    Texture = (project);
    AddressU = CLAMP;
    AddressV = CLAMP;
    MagFilter = LINEAR;
    MinFilter = LINEAR;
    Mipfilter = LINEAR;
};

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0_centroid;
};

float2 halfPixel;
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
	input.Position.x =  input.Position.x - 2*halfPixel.x;
	input.Position.y =  input.Position.y + 2*halfPixel.y;
    output.Position = float4(input.Position,1);
    output.TexCoord = input.TexCoord ;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{	

	//to be finished !!!
	///ideia
	///recuperar posicao 3d pelo depth map
	///reprojetar usando view peojectrion do projetor
	///fazer "depth test" na mao
	///os que passar, ler da textura de projecao e "misturar" com o conteudo do color map (acessado pelo uv normal)
	
	//projTex.xyz /= projTex.w;   
    //projTex.x = 0.5 * projTex.x + 0.5f;
    //projTex.y = -0.5 * projTex.y + 0.5f;    

    //return tex2D(projectTexture,projTex.xy);

	//float procces = tex2D(extraSampler,input.TexCoord).a;
    return float4(0,0,0,0);
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}


