float4x4 WorldViewProjection;  // Matrix World View Projection

//--Estrutura de Entrada para o vertex Shader
struct VS_INPUT
{
   float4 position:	POSITION;
};
//--Estrutura de Saida do vertex Shader
struct VS_OUTPUT
{
   float4 position:		POSITION;
   float3 texcoord0:	TEXCOORD0;
};

//--Vertex Shader
VS_OUTPUT VShader( VS_INPUT vin )
{
	VS_OUTPUT vout;
   
	float4 Posicion_Vertice = vin.position;   
    // Calcular a posicao final do Ponto
	vout.position = mul(Posicion_Vertice, WorldViewProjection);
	vout.position.z = vout.position.w;
	
    //Repassar as Posicoes como Textura para o Pixel Shader
	vout.texcoord0 = vin.position;

	return vout;
}

// Propriedades da Textura
samplerCUBE map_diffuse : register(s0);

// Estrutura de Entrada para o Pixel Shader
struct PS_INPUT
{
	float3 texcoord0: TEXCOORD0;
};

struct PixelShaderOutput
{
    float4 Color : COLOR0;
    float4 Normal : COLOR1;
    float4 Depth : COLOR2;
    float4 EXTRA1 : COLOR3;
};

// Pixel Shader
PixelShaderOutput PShader( PS_INPUT pin ) : COLOR0
{
	PixelShaderOutput output;
	float4 oColor;
   
	//Pega os Valores da Textura
	oColor = texCUBE(map_diffuse, pin.texcoord0);	

	output.Color = oColor;
	output.Normal.rgb = 0.5f;    
    output.Normal.a = 0.0f;    
    output.Depth = 1.0f;	

    output.EXTRA1.rgb = 0;	
    output.EXTRA1.a = 3.0f / 255.0f;

	return output;
}

technique RenderScene
{
	pass p0
	{	
		VertexShader = compile vs_2_0 VShader();
		PixelShader = compile ps_2_0 PShader();	
	}
}

