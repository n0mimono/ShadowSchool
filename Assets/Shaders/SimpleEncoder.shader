Shader "SimpleEncoder" {
  SubShader {
    Tags {"RenderType"="Opaque"}

    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      struct v2f {
        float4 pos : SV_POSITION;
      };

      v2f vert (appdata_full v) {
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target {
        return float4(0,0,0,1);
      }
      ENDCG

    }
  }
}
