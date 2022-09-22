Shader "Custom/Mask"
{
	Properties
	{
		//[IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
	}

	SubShader
	{

			Tags
			{
				"RenderType" = "Opaque"
				"RenderPipeline" = "UniversalPipeline"
				"Queue" = "Geometry"
			}

			Pass
			{
				ZWrite Off
				Blend Zero One

				Stencil
				{
					Ref 1
					Comp Always
					Pass Replace
					Fail Keep
				}
			}
	}
}