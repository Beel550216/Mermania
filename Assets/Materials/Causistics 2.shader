Shader "Custom/Causistics2"
{
    Properties
    {
        [Header(Caustics)]
        _CausticsTex("Caustics (RGB)", 3D) = "white" {}
        
        // Tiling X, Tiling Y, Offset X, Offset Y
        _Caustics_ST("Caustics ST", Vector) = (1,1,0,0)
        _Caustics2_ST("Caustics 2 ST", Vector) = (1,1,0,0)

        _Caustics1_Speed("Caustics 1 Speed", Vector) = (1, 1, 0 ,0)
        _Caustics2_Speed("Caustics 2 Speed", Vector) = (1, 1, 0 ,0)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;
                return color;
            }

            void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            fixed2 uv = IN.uv_MainTex * _Caustics_ST.xy + _Caustics_ST.zw;
            uv += _CausticsSpeed * _Time.y;

            fixed3 caustics = tex2D(_CausticsTex, uv).rgb;
            // Add
            o.Albedo.rgb += caustics;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
            ENDHLSL
        }
    }
}
