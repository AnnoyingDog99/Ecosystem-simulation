Shader "LowPolyGeometry"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightPoint ("Light Point Position", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma require geometry
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            

            #include "UnityCG.cginc"

            struct v2g
            {
                float2 uv : TEXCOORD0;
                float4 vertex : POSITION;
                float3 worldPos : TEXCOORD1;
                fixed4 col : COLOR;
            };

            struct g2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 col : COLOR;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightPoint;

            v2g vert (appdata_full v)
            {
                v2g o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.col = v.color;
                
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triangleStream)
            {
                g2f o;
                float3 normal = normalize(cross(
                    normalize(IN[1].worldPos - IN[0].worldPos),
                    normalize(IN[2].worldPos - IN[0].worldPos)
                    ));

                fixed4 colAvg = (IN[0].col + IN[1].col + IN[2].col) / 3;

 
                for (int i = 0; i < 3; i++)
                {
                    o.pos = IN[i].vertex;
                    o.uv = IN[i].uv;
                    o.col = colAvg;
                    o.worldNormal = normal;
                    o.worldPos = IN[i].worldPos;
                    triangleStream.Append(o);
                }
            }

            fixed4 frag (g2f i) : SV_Target
            {
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed intensity = dot(i.worldNormal, lightDir);
                intensity = saturate(intensity);
                fixed4 color = i.col * intensity;
                return i.col * intensity;
            }
            ENDCG
        }
    }
}
