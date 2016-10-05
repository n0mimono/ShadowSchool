Shader "ShadowCoord" {
  Properties {
    _MainTex ("Texture", 2D) = "white" {} // dummy
  }
  SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Pass {
      Tags {"LightMode" = "ForwardBase"}

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_fwdbase
      #include "UnityCG.cginc"
      #include "AutoLight.cginc"


      struct v2f {
        float4 pos : SV_POSITION;
        float2 uv  : TEXCOORD0;

        //SHADOW_COORDS(1)
        float4 _ShadowCoord : TEXCOORD1;
      };

      sampler2D _MainTex;
      float4 _MainTex_ST;
      
      v2f vert (appdata_full v) {
        v2f o;
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord;

        //TRANSFER_SHADOW(o)
        #if defined(UNITY_NO_SCREENSPACE_SHADOWS)
          o._ShadowCoord = mul( unity_WorldToShadow[0], mul( unity_ObjectToWorld, v.vertex ));
        #else
          o._ShadowCoord = ComputeScreenPos(o.pos);
        #endif

        return o;
      }
      
      fixed4 frag (v2f i) : SV_Target {

        //fixed shadow = SHADOW_ATTENUATION(i);
        #if defined (SHADOWS_SCREEN)
          //float shadow = unitySampleShadow(i._ShadowCoord);
          #if defined(UNITY_NO_SCREENSPACE_SHADOWS)
            #if defined(SHADOWS_NATIVE)
              fixed shadow = UNITY_SAMPLE_SHADOW(_ShadowMapTexture, i._ShadowCoord.xyz);
              shadow = _LightShadowData.r + shadow * (1-_LightShadowData.r);
            #else // for tegra
              float dist = SAMPLE_DEPTH_TEXTURE(_ShadowMapTexture, i._ShadowCoord.xy);
              float lightShadowDataX = _LightShadowData.x;
              float threshold = i._ShadowCoord.z;
              fixed shadow = max(dist > threshold, lightShadowDataX);
            #endif
          #else
            fixed shadow = tex2Dproj( _ShadowMapTexture, UNITY_PROJ_COORD(i._ShadowCoord)).r;
          #endif
        #else
          fixed shadow = 1;
        #endif

        //return i._ShadowCoord;
        return float4(1,1,1,1) * shadow;
      }
      ENDCG

    }
  }
  Fallback "Diffuse"
}
