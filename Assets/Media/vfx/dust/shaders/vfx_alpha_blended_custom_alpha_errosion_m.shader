Shader "VFX/alpha_blended_custom_alpha_errosion" {
    Properties {
        [Header(Custom Vertex Z Alpha Errosion)]
        [NoScaleOffset] _MainTex ("Particle Texture", 2D) = "white" {}
        _EmissivePower ("Emissive Power", Range(1, 10)) = 1
        _AlphaPreMult ("Alpha Pre Multiply", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Pass {
        
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            uniform float _EmissivePower;
            uniform float _AlphaPreMult;
            
            struct VertexInput {
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            
            float4 frag(VertexOutput i) : COLOR {
                fixed4 tex = tex2D(_MainTex, i.uv0);
                float3 emissive = i.vertexColor.rgb * tex.rgb * (1 - ((1 - tex.a) * _AlphaPreMult)) * _EmissivePower;
                float opacity = saturate(tex.a - i.uv0.b) * i.vertexColor.a;
                fixed4 col = fixed4(emissive, opacity);
                return col;
            }
            ENDCG
        }
    }
}
