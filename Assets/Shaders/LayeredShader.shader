Shader "Unlit/LayeredShader"
{
	Properties
	{
		_Color("Skin Color", Color) = (1,1,1,1)

		// layer 1
		_Tint1r("Tint 1 (Red Channel)", Color) = (1,1,1,1)
		_Tint1g("Tint 1 (Green Channel)", Color) = (0,0,0,1)
		_Tint1b("Tint 1 (Blue Channel)", Color) = (0,0,0,1)

		_Tex1("Layer 1 (RGB) Transp (A)", 2D) = "black" {}

		// layer 2
		_Tint2r("Tint 2 (Red Channel)", Color) = (1,1,1,1)
		_Tint2g("Tint 2 (Green Channel)", Color) = (0,0,0,1)
		_Tint2b("Tint 2 (Blue Channel)", Color) = (0,0,0,1)

		_Tex2("Layer 2 (RGB) Transp (A)", 2D) = "black" {}

		// layer 3
		_Tint3r("Tint 3 (Red Channel)", Color) = (1,1,1,1)
		_Tint3g("Tint 3 (Green Channel)", Color) = (0,0,0,1)
		_Tint3b("Tint 3 (Blue Channel)", Color) = (0,0,0,1)

		_Tex3("Layer 3 (RGB) Transp (A)", 2D) = "black" {}

		// layer 4
		_Tint4r("Tint 4 (Red Channel)", Color) = (1,1,1,1)
		_Tint4g("Tint 4 (Green Channel)", Color) = (0,0,0,1)
		_Tint4b("Tint 4 (Blue Channel)", Color) = (0,0,0,1)

		_Tex4("Layer 4 (RGB) Transp (A)", 2D) = "black" {}

		// layer 5
		_Tint5r("Tint 5 (Red Channel)", Color) = (1,1,1,1)
		_Tint5g("Tint 5 (Green Channel)", Color) = (0,0,0,1)
		_Tint5b("Tint 5 (Blue Channel)", Color) = (0,0,0,1)

		_Tex5("Layer 5 (RGB) Transp (A)", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"
			"RenderPipeline"="UniversalPipeline"
			"Queue" = "Geometry"
		}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			fixed4 _Color; // skin color


			// layer 1
			sampler2D _Tex1;
			float4 _Tex1_ST;
			fixed4 _Tint1r;
			fixed4 _Tint1g;
			fixed4 _Tint1b;

			// layer 2
			sampler2D _Tex2;
			float4 _Tex2_ST;
			fixed4 _Tint2r;
			fixed4 _Tint2g;
			fixed4 _Tint2b;

			// layer 3
			sampler2D _Tex3;
			float4 _Tex3_ST;

			fixed4 _Tint3r;
			fixed4 _Tint3g;
			fixed4 _Tint3b;

			// layer 4
			sampler2D _Tex4;
			float4 _Tex4_ST;
			fixed4 _Tint4r;
			fixed4 _Tint4g;
			fixed4 _Tint4b;

			// layer 5
			sampler2D _Tex5;
			float4 _Tex5_ST;

			fixed4 _Tint5r;
			fixed4 _Tint5g;
			fixed4 _Tint5b;

			struct Input {
				float2 uv_Tex1;
			};

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};
			
			inline fixed4 TriTint(fixed4 texColor, fixed4 tintr, fixed4 tintg, fixed4 tintb)
			{
				fixed4 col =
					(tintr * texColor.r)
					+ (tintg * texColor.g)
					+ (tintb * texColor.b)
					;
				col.a = texColor.a;
				return col;
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _Tex1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// ALBEDO
				//
				float2 uv = i.uv;

				fixed4 c1 = tex2D(_Tex1, uv);
				fixed4 c2 = tex2D(_Tex2, uv);
				fixed4 c3 = tex2D(_Tex3, uv);
				fixed4 c4 = tex2D(_Tex4, uv);
				fixed4 c5 = tex2D(_Tex5, uv);

				fixed4 c = _Color;

				fixed4 color1 = TriTint(c1, _Tint1r, _Tint1g, _Tint1b);
				fixed4 color2 = TriTint(c2, _Tint2r, _Tint2g, _Tint2b);
				fixed4 color3 = TriTint(c3, _Tint3r, _Tint3g, _Tint3b);
				fixed4 color4 = TriTint(c4, _Tint4r, _Tint4g, _Tint4b);
				fixed4 color5 = TriTint(c5, _Tint5r, _Tint5g, _Tint5b);

				c = lerp(c, color1, color1.a);
				c = lerp(c, color2, color2.a);
				c = lerp(c, color3, color3.a);
				c = lerp(c, color4, color4.a);
				c = lerp(c, color5, color5.a);

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}
			ENDCG
		} // end pass
	}
}
