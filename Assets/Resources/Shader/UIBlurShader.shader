Shader "UI/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 10)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _MainTex_ST;
            float _BlurAmount;
            float4 _MainTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = 0;
                float2 uv = i.uv;
                
                // Простое размытие (можно заменить на более сложный алгоритм)
                col += tex2D(_MainTex, uv + float2(-_BlurAmount, -_BlurAmount) * _MainTex_TexelSize.xy) * 0.077847;
                col += tex2D(_MainTex, uv + float2(0, -_BlurAmount) * _MainTex_TexelSize.xy) * 0.123317;
                col += tex2D(_MainTex, uv + float2(_BlurAmount, -_BlurAmount) * _MainTex_TexelSize.xy) * 0.077847;
                
                col += tex2D(_MainTex, uv + float2(-_BlurAmount, 0) * _MainTex_TexelSize.xy) * 0.123317;
                col += tex2D(_MainTex, uv) * 0.195346;
                col += tex2D(_MainTex, uv + float2(_BlurAmount, 0) * _MainTex_TexelSize.xy) * 0.123317;
                
                col += tex2D(_MainTex, uv + float2(-_BlurAmount, _BlurAmount) * _MainTex_TexelSize.xy) * 0.077847;
                col += tex2D(_MainTex, uv + float2(0, _BlurAmount) * _MainTex_TexelSize.xy) * 0.123317;
                col += tex2D(_MainTex, uv + float2(_BlurAmount, _BlurAmount) * _MainTex_TexelSize.xy) * 0.077847;
                
                return col;
            }
            ENDCG
        }
    }
}