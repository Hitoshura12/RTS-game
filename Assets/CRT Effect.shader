Shader "Effects/CRT effect"
{
    Properties
    {
[NoScaleOffset]
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _MaskBlend ("TexBlend",2D)= "white"{}
        _MaskSize ("Mask Size", Float)= 0.2
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
          

            #include "UnityCG.cginc"


            sampler2D _MainTex;
            float4 _Color;
            sampler2D _MaskBlend;
            float _MaskSize;
 

            float4 frag (v2f_img i) : SV_Target
            {

                // sample the texture
                float4 col = tex2D(_MainTex , i.uv * _MaskSize);
                float mask = tex2D(_MaskBlend,i.uv);
               
                float4 eff= (col * _Color)* mask *sin(float4((i.pos.yw)*_Time,1,1));
            return lerp( col,mask, eff);
            }
            ENDCG
        }
    }
}
