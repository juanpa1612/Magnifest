// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Custom/TexMask" {
	Properties{
		_Textura1("Textura1", 2D) = "lava" {}
	    _Textura2("Textura2", 2D) = "magic" {}
	    _Textura3("Textura3", 2D) = "wood" {}
	    _Mascara("Mascara", 2D) = "mascara" {}
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
    sampler2D _Textura3;
	sampler2D _Mascara;

	
	struct Input {
		float2 uv_Textura1;
		float2 uv_Textura2;
		float2 uv_Textura3;
		float2 uv_Mascara;
	};


	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		float4 tex1 = tex2D(_Textura1, IN.uv_Textura1);
		float4 tex2 = tex2D(_Textura2, IN.uv_Textura2);
		float4 tex3 = tex2D(_Textura3, IN.uv_Textura3);

		float4 mask1 = tex2D(_Mascara, IN.uv_Textura1).r;
		float4 mask2 = tex2D(_Mascara, IN.uv_Textura2).g;
		float4 mask3 = tex2D(_Mascara, IN.uv_Textura3).b;

		float4 c = (tex1*mask1) + (tex2*mask2) + (tex3*mask3);
		o.Albedo = c.rgb;


	}
	ENDCG
	}
		FallBack "Diffuse"
}

