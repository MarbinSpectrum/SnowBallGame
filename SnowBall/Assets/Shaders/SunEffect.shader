Shader "Custom/SunEffect"
{
 Properties
    {
        _EffTex("Texture", 2D) = "white" {}
        _EffValue ("Eff",Range (0, 1)) = 0

        _BlendColor ("BlendColor", Color) = (1,1,1,0)
        _LerpValue ("lerp",Range (0, 1)) = 0

        _Tint ("TintColor", Color) = (1,1,1,0)
    }
    SubShader
    {

        Tags{ "Queue" = "Transparent" }

        GrabPass
        {   
        }

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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 vertColor : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                o.vertColor = v.color;
                return o;
            }

            sampler2D _EffTex;
            float _EffValue;

            sampler2D _GrabTexture;
            float4 _BlendColor;

            float4 _Tint;

            UNITY_INSTANCING_BUFFER_START(Props)

            UNITY_DEFINE_INSTANCED_PROP(float, _LerpValue)

            UNITY_INSTANCING_BUFFER_END(Props)

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2Dproj(_GrabTexture, i.grabPos);

                fixed4 c = tex2D(_EffTex, i.grabPos + _Time.x);
                fixed4 newPos = i.grabPos;
                newPos.x += c.x*_EffValue;
                half4 col2 = tex2Dproj(_GrabTexture, newPos);
                
                col2 = col2/(1-_BlendColor)+_Tint;
                col2 = lerp(col,col2,UNITY_ACCESS_INSTANCED_PROP(Props, _LerpValue));
                return col2;
            }
            ENDCG
        }
    }
}