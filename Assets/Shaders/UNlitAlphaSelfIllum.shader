Shader "Unlit/AlphaSelfIllum"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _MainTex ("SelfIllum Color (RGB) Alpha (A)", 2D) = "white"
    }
    Category
    {
       Lighting Off
       ZWrite Off
       Cull Back
       Blend SrcAlpha OneMinusSrcAlpha
       Tags {"Queue"="AlphaTest" "RenderType"="Transparent"}
       Color[_Color]
       SubShader
       {
                      
            Pass
            {            
               SetTexture [_MainTex] 
               {
                      Combine Texture * Primary
               }
            
            }
            
            
        } 
    }
}