// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Custom/Texturas" {
	Properties{
		_Textura1("Textura1", 2D) = "minecraft" {}
	    _Textura2("Textura2", 2D) = "portal" {}
		_Color("White", Color) = (1,1,1,1)
		_Mezclador("Mezclador", Range(0,1)) = 0
	}
		SubShader{
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0
	sampler2D _Textura1;
	sampler2D _Textura2;

	
	struct Input {
		float2 uv_Textura1;
		float2 uv_Textura2;
	};


	float4 _Color;
	float _Mezclador;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		float4 c = tex2D(_Textura1, IN.uv_Textura1) *(1 - _Mezclador) + tex2D(_Textura2, IN.uv_Textura2)*(_Mezclador);
		o.Albedo = c.rgb;

	}
	ENDCG
	}
		FallBack "Diffuse"
}

