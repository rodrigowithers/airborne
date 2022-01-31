Shader "Hidden/PaletteSwap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 pos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            half4x4 _ColorMatrix;

            half _Radius;
            half3 _Position;

            fixed4 frag(v2f i) : SV_Target
            {
                half3 col = tex2D(_MainTex, i.uv).rgb;
                fixed x = col.r;
                
                half dist = length(_Position.xy - i.pos);

                if (dist > _Radius)
                    return _ColorMatrix[floor(x * 3)];

                return half4(col, 1);
            }
            ENDCG
        }
    }
}