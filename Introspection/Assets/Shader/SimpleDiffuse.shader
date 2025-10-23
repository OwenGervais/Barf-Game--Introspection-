Shader "Unlit/SimpleDiffuse"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1, 1, 1, 1)   
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" "RenderType" = "Opaque" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;  
                float3 normalOS : NORMAL;      
                float2 uv : TEXCOORD0;         
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION; 
                float3 normalWS : TEXCOORD1;      
                float3 viewDirWS : TEXCOORD2;     
                float2 uv : TEXCOORD0;            
            };

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;  
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPosWS);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                Light mainLight = GetMainLight();
                half3 lightDir = normalize(mainLight.direction);

                half3 normalWS = normalize(IN.normalWS);

                half NdotL = saturate(dot(normalWS, lightDir));

                half3 diffuse = _BaseColor.rgb * NdotL;

                half3 finalColor = diffuse + _BaseColor.rgb;

                return half4(finalColor, 1.0);
            }

            ENDHLSL
        }
    }
}