Shader "ShadowUnlit" {
  Properties {
    _MainTex ("Texture", 2D) = "white" {}
  }
  SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    CGINCLUDE
      //#include "AutoLight.cginc"

      #define SHADOW_COORDS(idx1) float4 _CustomShadowCoord : TEXCOORD##idx1;

      float4x4 _CustomShadowViewProj;
      float4 _WorldSpaceShadowCameraPos;
      float4 world2shadow(float4 vertex) {
        float4 wpos = mul( unity_ObjectToWorld, vertex);

        // shadow camera space
        float4 coord = mul( _CustomShadowViewProj, wpos);
        coord.z = length(_WorldSpaceShadowCameraPos.xyz - wpos.xyz);
        return coord;
      }
      #define TRANSFER_SHADOW(o) o._CustomShadowCoord = world2shadow(v.vertex);

      sampler2D_float _CustomShadowMap;
      float4 _CustomShadowZBufferParams;

      float sampleShadowDepth(float4 coord) {
        float2 shadowUv = coord.xy / coord.w * 0.5 + 0.5;
        float shadowDepth = tex2D(_CustomShadowMap, shadowUv).r;
        return 1.0 / (_CustomShadowZBufferParams.z * shadowDepth + _CustomShadowZBufferParams.w);
      }

      float sampleDepth(float4 coord) {
        return coord.z;
      }

      float sampleShadow(float4 coord) {
        float shadowDepth = sampleShadowDepth(coord);
        float depth = sampleDepth(coord);
        return depth < shadowDepth? 1 : 0;
      }
      #define SHADOW_ATTENUATION(i) sampleShadow(i._CustomShadowCoord)

      #define DEBUG_SHADOW_DEPTH(i) float4(1,1,1,1) * sampleShadowDepth(i._CustomShadowCoord)
      #define DEBUG_DEPTH(i) float4(1,1,1,1) * sampleDepth(i._CustomShadowCoord)
    ENDCG

    Pass {
      Tags {"LightMode" = "ForwardBase"}

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_fwdbase
      #include "UnityCG.cginc"

      struct v2f {
        float4 pos : SV_POSITION;
        float2 uv  : TEXCOORD0;
        SHADOW_COORDS(1)
      };

      sampler2D _MainTex;
      float4 _MainTex_ST;
      
      v2f vert (appdata_full v) {
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord;
        TRANSFER_SHADOW(o)
        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target {
        float shadow = SHADOW_ATTENUATION(i);
        float4 atten = float4(shadow * 0.5 + 0.5, 1, 1, 1);
        return tex2D(_MainTex, i.uv) * atten;
      }
      ENDCG

    }
  }
  Fallback "Diffuse"
}
