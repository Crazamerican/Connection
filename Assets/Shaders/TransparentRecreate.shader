// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/TransparentRecreate"
{
    Properties
    {
        _Color ("Tint Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white"{}
    }
   
    SubShader
    {
        Tags { "Queue"="Transparent" }
 
        Pass
        {
           ZWrite Off
           ColorMask 0
        }
        Blend OneMinusDstColor OneMinusSrcAlpha //invert blending, so long as FG color is 1,1,1,1
        BlendOp Add
       
        Pass
        {
       
CGPROGRAM
#include "UnityCG.cginc"

#pragma vertex vert
#pragma fragment frag
uniform float4 _Color;
sampler2D _MainTex;
 
struct vertexInput
{
    float4 vertex: POSITION;
	float2 uv : TEXCOORD0;
    float4 color : COLOR;  
};
 
struct fragmentInput
{
    float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
    float4 color : COLOR0;
};

float4 _MainTex_ST;
 
fragmentInput vert( vertexInput i )
{
    fragmentInput o;
    o.pos = UnityObjectToClipPos(i.vertex);
	o.uv = TRANSFORM_TEX(i.uv, _MainTex);
    o.color = _Color;
    return o;
}
 
half4 frag( fragmentInput i ) : COLOR
{
	fixed4 texcol = tex2D(_MainTex, i.uv);
    return texcol - _Color;
}
 
ENDCG
}
}
}