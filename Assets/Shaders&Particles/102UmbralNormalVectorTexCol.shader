// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Custom/2UmbralNormalVectorTexCol" {
	Properties{
		_Textura("Textura", 2D) = "lava" {}
		_Umbral1("Umbral1", Range(0,1)) = 0
		_Umbral2("Umbral2", Range(0,1)) = 0
	}
		SubShader{
		LOD 200
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

	sampler2D _Textura;
		
	struct Input {
		float2 uv_Textura;
		float3 worldNormal;
		float3 viewDir;
	};
	float _Umbral1;
	float _Umbral2;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		float bordes = abs(dot(IN.worldNormal, IN.viewDir));
		float4 c = tex2D(_Textura, IN.uv_Textura);


		if (bordes < _Umbral1)
		{
			bordes = 0;
		}
		else {
			bordes = 1;
		}

		if (bordes < _Umbral2)
		{
			bordes = _Umbral2/5;
		}
		else {
			bordes = _Umbral2/5;
		}

			
		o.Albedo = (bordes*c).rgb;

	}
	ENDCG
	}
		FallBack "Diffuse"
}

