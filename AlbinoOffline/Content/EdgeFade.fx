﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_3
	#define PS_SHADERMODEL ps_4_0_level_9_3
#endif

float multiplier = 0;
float fPixelWidth = 1;
float fPixelHeight = 1;

int4x4 ptn0 = {    0, 48, 12, 60,
		  32, 16, 44, 28,
		   8, 56,  4, 52,
		  40, 24, 36, 20 };

int4x4 ptn1 = {    3, 51, 15, 63,
		  35, 19, 47, 31,
		  11, 59,  7, 55,
		  43, 27, 39, 23 };

int4x4 ptn2 = {    2, 50, 14, 62,
		  34, 18, 46, 30,
		  10, 58,  6, 54,
		  42, 26, 38, 22 };

int4x4 ptn3 = {    1, 49, 13, 61,
		  33, 17, 45, 29,
		   9, 57,  5, 53,
		  41, 25, 37, 21 };

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 coords =  input.TextureCoordinates;
    float4 pixel = tex2D(SpriteTextureSampler, coords);
    int xIn = (coords.x * (1.0 / fPixelWidth)) % 8;
    int yIn = (coords.y * (1.0 / fPixelHeight)) % 8;
    int alpha = round(pixel.a * multiplier * 64.0);
    int pattern = floor( xIn / 4.0 ) + (floor( yIn / 4.0 ) * 2);
    pixel.a = 0;
    int diff = 0; 
    if (pattern == 0) { diff = ptn0[xIn][yIn] - alpha ; }
    if (pattern == 1) { diff = ptn1[xIn-4][yIn] - alpha ; }
    if (pattern == 2) { diff = ptn2[xIn][yIn-4] - alpha ; }
    if (pattern == 3) { diff = ptn3[xIn-4][yIn-4] - alpha ; }
    if (diff < 0) { pixel.a = 1; }

    return pixel;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};