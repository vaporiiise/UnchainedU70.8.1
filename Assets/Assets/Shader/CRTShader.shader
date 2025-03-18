Shader "Hidden/CRT_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Curvature ("Curvature", Range(0, 1)) = 0.15
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.3
        _Vignette ("Vignette", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Curvature;
            float _ScanlineIntensity;
            float _Vignette;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                // CRT curvature effect (barrel distortion)
                uv = uv - 0.5;
                uv *= 1.0 + _Curvature * (uv.x * uv.x + uv.y * uv.y);
                uv += 0.5;

                // Scanline effect
                float scanline = sin(uv.y * 800.0) * _ScanlineIntensity;

                // Vignette effect (darkens edges)
                float2 center = float2(0.5, 0.5);
                float vignette = 1.0 - (length(uv - center) * _Vignette);

                // Sample the texture
                fixed4 col = tex2D(_MainTex, uv);
                col.rgb *= (1.0 + scanline) * vignette;

                return col;
            }
            ENDCG
        }
    }
}