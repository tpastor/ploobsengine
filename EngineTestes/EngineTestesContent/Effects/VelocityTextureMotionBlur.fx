float4x4 wvp;
float4x4 oldwvp;

struct VertexShaderInput
{
    float3 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
	float4 p1 : TEXCOORD1;
	float4 p2 : TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
    
    output.Position = mul(float4(input.Position,1),wvp);
	output.p1 = output.Position ;
	output.p2 = mul(float4(input.Position,1),oldwvp);
    output.TexCoord = input.TexCoord;    
    return output;
}

float4 PixelShaderFunctionNormal(VertexShaderOutput input) : COLOR0
{
	input.p1 = input.p1 / input.p1.w;
	input.p2 = input.p2 / input.p2.w;
	float4 r = (input.p1 - input.p2) / 2;
	r = (r + 1)/2;
	return r;
}

technique NormalTechnich
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunctionNormal();
    }
}

