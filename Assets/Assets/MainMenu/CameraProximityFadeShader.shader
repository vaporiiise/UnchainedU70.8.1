Shader "Custom/CameraProximityFade"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _CameraPosition ("Camera Position", Vector) = (0,0,0,0)
        _FadeRadius ("Fade Radius", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _MainTex;
            float4 _CameraPosition;
            float _FadeRadius;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Calculate the distance between the camera and the world position of the pixel
                float distToCamera = distance(i.worldPos, _CameraPosition.xyz);

                // Apply fade based on distance within the fade radius
                col.a *= smoothstep(_FadeRadius, 0.0, distToCamera);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
}