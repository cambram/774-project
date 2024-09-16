Shader "Unlit/StarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Size ("Size", float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Background" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // For proper alpha blending
            ZWrite Off // Disable depth writing
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"
            #include "UnityInstancing.cginc"

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
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;

            v2f vert(appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                // Scale the vertex position based on the size
                float4 scaledVertex = v.vertex;
                scaledVertex.xyz *= _Size;

                // Apply the position transformation for stereo
                o.vertex = UnityObjectToClipPos(scaledVertex);
                o.uv = v.uv;

                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                // Center the UV coordinates from -0.5 to +0.5
                float2 centeredUV = i.uv - 0.5;

                // Calculate the distance from the center of the star (normalised)
                float distance_from_centre = length(centeredUV) * 2.0;

                // Smoothstep for a circular dropoff
                float alpha = smoothstep(0.7, 0.3, distance_from_centre);

                // Set the color with proper alpha
                fixed4 col = fixed4(_Color.rgb, alpha);

                // Apply fog if needed
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}
