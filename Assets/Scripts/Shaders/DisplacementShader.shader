Shader "Custom/DisplacementShader"
{
    Properties
    {
        _MainTex("Source Texture", 2D) = "white" {}
        _DispTex("Displacement Map", 2D) = "black" {}
        _Intensity("Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _DispTex;
            float4 _MainTex_ST;
            float4 _DispTex_ST;
            float _Intensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            float4 frag(v2f i) : SV_Target
            {
                float disp = tex2D(_DispTex, i.uv).r;
                float2 offset = float2(0, (disp - 0.5) * 2.0 * _Intensity);
                float4 color = tex2D(_MainTex, i.uv + offset);
                return color;
            }
            ENDCG
        }
    }
}
