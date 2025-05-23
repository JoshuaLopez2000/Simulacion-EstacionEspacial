Shader "Custom/RedPulseThroughWalls"
{
    Properties
    {
        _Color("Color", Color) = (1, 0, 0, 1)
        _PulseSpeed("Pulse Speed", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest Always
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _Color;
            float _PulseSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float pulse = abs(sin(_Time.y * _PulseSpeed));
                return fixed4(_Color.rgb * pulse, pulse);
            }
            ENDCG
        }
    }
}
