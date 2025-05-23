Shader "Custom/GlassScannerStencil_BuiltIn"
{
    Properties
    {
        _Color("Glass Color", Color) = (0, 1, 0, 0.3)
        _ScanColor("Scan Line Color", Color) = (1, 1, 1, 0.5)
        _ScanWidth("Scan Line Width", Float) = 0.1
        _ScanSpeed("Scan Speed", Float) = 1.0
        [IntRange] _StencilID("Stencil ID", Range(0, 255)) = 1
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Back

        Stencil
        {
            Ref [_StencilID]
            Comp Always
            Pass Replace
        }

        Pass
        {
            Cull Off
            Name "GlassPass"
            Tags { "LightMode" = "Always" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            float4 _Color;
            float4 _ScanColor;
            float _ScanWidth;
            float _ScanSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float scanY = frac(_Time.y * _ScanSpeed) * 10.0;
                float dist = abs(i.worldPos.y - scanY);
                float scanIntensity = saturate(1.0 - dist / _ScanWidth);

                float3 baseColor = _Color.rgb;
                float3 scanColor = _ScanColor.rgb * scanIntensity;
                float alpha = _Color.a + (_ScanColor.a * scanIntensity);

                return float4(baseColor + scanColor, alpha);
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
