// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Outline"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		// Add values to determine if outlining is enabled and outline color.
		[PerRendererData] _Outline("Outline", Float) = 0
		[PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
		[PerRendererData] _OutlineSize("Outline Size", float) = 0.1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
 
			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			 
			struct v2f
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 screenpos : TEXCOORD1;
				float4 worldPos : TEXCOORD2;
			};

			sampler2D _MainTex;
            sampler2D _AlphaTex;
            float4 _MainTex_TexelSize;

			fixed4 SampleSpriteTexture (float2 uv)
            {
                fixed4 color = tex2D (_MainTex, uv);

                #if ETC1_EXTERNAL_ALPHA
                // get the color from an external texture (usecase: Alpha support for ETC1 on android)
                color.a = tex2D (_AlphaTex, uv).r;
                #endif //ETC1_EXTERNAL_ALPHA

                return color;
            }

			float _Outline;
			fixed4 _OutlineColor;
			int _OutlineSize;

			v2f vert(appdata v) {
                  v2f o;
  
                  o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                  o.vertex = UnityObjectToClipPos(v.vertex);
  
                  return o;
            }

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;

				// If outline is enabled and there is a pixel, try to draw an outline.
				if (_Outline > 0) {

				}

				c.rgb *= c.a;

				return c;
			}
		ENDCG
		}
	}
}
