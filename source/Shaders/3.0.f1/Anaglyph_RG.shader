// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'
// Upgrade NOTE: replaced 'glstate.matrix.texture[0]' with 'UNITY_MATRIX_TEXTURE0'
// Upgrade NOTE: replaced 'samplerRECT' with 'sampler2D'
// Upgrade NOTE: replaced 'texRECT' with 'tex2D'

Shader "Colour Balanced Anaglyph RG" {
Properties {
   _LeftTex ("Left (RGB)", RECT) = "white" {}
   _RightTex ("Right (RGB)", RECT) = "white" {}
}

SubShader {
   Pass {
      ZTest Always Cull Off ZWrite Off
      Fog { Mode off }

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest
      #include "UnityCG.cginc"
      
      #define RED_RATIO_ADJ  .6f
      #define GREEN_RATIO_ADJ  .8f
      #define BLUE_RATIO_ADJ  .4f
      
      uniform sampler2D _LeftTex;
      uniform sampler2D _RightTex;
      
      struct v2f {
         float4 pos : POSITION;
         float2 uv : TEXCOORD0;
      };
      
      v2f vert( appdata_img v )
      {
         v2f o;
         o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
         float2 uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
         o.uv = uv;
         return o;
      }
      
      half4 frag (v2f i) : COLOR
      {
         float r, g, b;
         float4 texR = tex2D(_LeftTex, i.uv);
         float4 texGB = tex2D(_RightTex, i.uv);
         float4 texRGB;
         
         r=texR.r;
         g=texGB.g;
         b=texGB.b;
         
         float lumR = texR.r * 0.299f;
         float lumGB = texR.g * 0.299f;
         
         // Balance red
         float ratio = lumGB / lumR;
         float d = texR.r * ratio * RED_RATIO_ADJ;
         if (d > texR.r) {
            r = d;
            if (r > 0xff) r = 0xff;
         }
         
         lumR = texGB.r * 0.299f;
         lumGB = texGB.g * 0.299f;
         
         //Balance green
         ratio = lumR / lumGB;
         d = texGB.g * ratio * GREEN_RATIO_ADJ;
         if (d > texGB.g) {
            g = d;
            if (g > 0xff) g = 0xff;
         }
         
         //Balance blue
         d = texGB.b * ratio * BLUE_RATIO_ADJ;
         if (d > texGB.b) {
            b = d;
            if (b > 0xff) b = 0xff;
         }
         
         texRGB = float4(r,g,b,1);
         return texRGB;
      }
      ENDCG
   }
}   
   Fallback off
} 
