Shader "Unlit/StarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // If not needed, this will be bypassed
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Size ("Size", float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Background" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"
            #include "UnityInstancing.cginc" // For instancing support

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO // For stereo rendering
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                
                // Initialize stereo output to ensure both eyes are rendered correctly
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); 

                // Correct star size transformation
                float4 scaledVertex = v.vertex;
                scaledVertex.xyz *= _Size; // Scale the star

                // Standard VR-aware transformation using UnityObjectToClipPos
                o.vertex = UnityObjectToClipPos(scaledVertex);

                // Apply UV transformation
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Transfer fog data
                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate distance from centre of the quad
                float distance_from_centre = length((2 * i.uv) - 1);

                // Desmos-designed function to give punchy star drop off.
                float inverse_dist = saturate((0.2 / distance_from_centre) - 0.2);

                // Star colour with a smooth falloff based on distance from the centre
                float4 col = float4(_Color.r, _Color.g, _Color.b, inverse_dist);

                // Apply fog if needed
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
