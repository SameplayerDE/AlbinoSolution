#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_3
	#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

Texture2D Dither;

sampler2D DitherSampler = sampler_state
{
	Texture = <Dither>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
    float dx = input.TextureCoordinates.x - 0.5;
    float dy = input.TextureCoordinates.y - 0.5;
    
    float tpos = dx * dx + dy * dy;
    if(tpos <= 0.25 && tpos > 0.25 - 100)
        return color;
    else
        return float4(0.0, 0.0, 0.0, 0.0);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};