// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/TransparentOnBackground"
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
                Blend OneMinusDstColor OneMinusSrcColor //invert blending, so long as FG color is 1,1,1,1
                BlendOp Add
                SetTexture [_MainTex]
                {
                    constantColor [_Color]
                    combine texture * constant
                }
            }
    }
}
