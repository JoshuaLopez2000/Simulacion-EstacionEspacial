Shader "Custom/StencilMask"
{
    SubShader
    {
        // Render first
        Tags { "Queue" = "Geometry-10" }

        Pass
        {
            // Don't render color or depth
            ColorMask 0
            ZWrite Off

            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }
        }
    }
}
