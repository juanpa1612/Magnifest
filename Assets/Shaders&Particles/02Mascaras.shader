// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Custom/Mascaras" {
	Properties{
		_Textura("Textura", 2D) = "minecraft" {}
	    _Mascara("Mascara", 2D) = "mascara" {}
		_Color("Red", Color) = (1,0,0,1)
		_Mezclador("Mezclador", Range(0,1)) = 0
	}
		SubShader{
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0
	sampler2D _Textura;
	sampler2D _Mascara;

	
	struct Input {
		float2 uv_Textura;
		float2 uv_Mascara;
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
		float4 c1 = tex2D(_Textura, IN.uv_Textura);
		float4 c2 = tex2D(_Mascara, IN.uv_Textura)*(_Color)*(_Mezclador);
		//float4 c3 = ;
		float4 c4 = c1*c2;
		o.Albedo = c4.rgb;


	}
	ENDCG
	}
		FallBack "Diffuse"
}

