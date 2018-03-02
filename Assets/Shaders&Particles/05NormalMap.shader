// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Custom/NormalTex" {
	Properties{
		_MovTex("Textura", 2D) = "lava" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
	    _VelAng1("VelAng1", Range(-1,1)) = 0
		_VelAng2("VelAng2", Range(-1,1)) = 0
		_Lambda1("Lambda1", Range(-1,1)) = 0
		_Lambda2("Lambda2", Range(-1,1)) = 0
		_Fuerza("Fuerza", Range(0,2)) = 0
	}
		SubShader{
		LOD 200
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

	sampler2D _MovTex;
	sampler2D _BumpMap;
		
	struct Input {
		float2 uv_MovTex;
		float2 uv_BumpMap;
	};

	float _VelAng1;
	float _VelAng2;
	float _Lambda1;
	float _Lambda2;
	float _Fuerza;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		float2 UVmov = IN.uv_MovTex;
		float2 UVnormal = IN.uv_BumpMap;
		float xdis = sin(_VelAng1*_Time.y)*_Lambda1;
		float ydis = sin(_VelAng2*_Time.y)*_Lambda2;
		UVmov += float2(xdis, ydis);
		UVnormal += float2(xdis, ydis);

		float4 c = tex2D(_MovTex, UVmov);
		o.Albedo = c.rgb;

		float4 n = tex2D(_BumpMap, UVnormal);
		float3 normalR = (UnpackNormal(n)).rgb;
		normalR.rg *= (_Fuerza);
		o.Normal = normalize(normalR);


	}
	ENDCG
	}
		FallBack "Diffuse"
}

