Shader "A11YTK/Unlit" {
	Properties
	{
		_Color ("Color Tint", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white"
	}
	Category
	{
    	Tags { "Queue"="Overlay" }
		Lighting On
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest Always
		SubShader {
			Material {
				Emission [_Color]
				Diffuse [_Color]
			}
			Pass
			{
				SetTexture [_MainTex] {
					Combine Texture * Primary
				}
			}
		}
	}
}
