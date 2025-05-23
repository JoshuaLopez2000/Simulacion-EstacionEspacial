Shader "Custom/HologramEffectImproved"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Hologram ("Hologram Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _Frequency ("Scan Frequency", Range(1,30)) = 10
        _Speed ("Scan Speed", Range(0,10)) = 2
        _Intensity ("Hologram Intensity", Range(0,5)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _Hologram;
            float4 _MainTex_ST;
            float4 _Hologram_ST;
            float4 _Color;
            float _Frequency;
            float _Speed;
            float _Intensity;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 huv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Para el escaneo horizontal: modificamos X en lugar de Y
                float scanOffset = _Time.y * _Speed;
                o.huv = TRANSFORM_TEX(v.uv, _Hologram);
                o.huv.x += scanOffset;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 baseCol = tex2D(_MainTex, i.uv) * _Color;
                fixed4 holo = tex2D(_Hologram, i.huv);

                // Crear líneas de escaneo horizontales usando seno
                float scanLine = abs(sin(i.huv.x * _Frequency));
                holo.rgb *= scanLine * _Intensity;

                fixed alpha = holo.r * baseCol.a;

                return fixed4(holo.rgb * baseCol.rgb, alpha);
            }
            ENDCG
        }
    }
}
