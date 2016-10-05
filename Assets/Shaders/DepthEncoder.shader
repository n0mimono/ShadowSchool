Shader "DepthEncoder" {
  SubShader {
    Tags {"RenderType"="Opaque"}

    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      sampler2D _CameraDepthTexture;

      struct v2f {
        float4 pos : SV_POSITION;
        float2 uv  : TEXCOORD0;
      };

      v2f vert (appdata_full v) {
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv  = v.texcoord;
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target {
        half depth = tex2D(_CameraDepthTexture, i.uv).r;
        return half4(half3(1,1,1) * depth, 1);
      }
      ENDCG

    }
  }
}
