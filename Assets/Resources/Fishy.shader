Shader "Fishy" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_DecalTex ("Decal (RGBA)", 2D) = "black" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 300
	
CGPROGRAM
#pragma surface surf BlinnPhong

sampler2D _MainTex;
sampler2D _DecalTex;
fixed4 _Color;
half _Shininess;

struct Input {
	float2 uv_MainTex;
	float2 uv_DecalTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	//fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	//o.Albedo = tex.rgb * _Color.rgb;
	//o.Gloss = tex.a;
	//o.Alpha = tex.a * _Color.a;
	//o.Specular = _Shininess;
	
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 decal = tex2D(_DecalTex, IN.uv_DecalTex);
	fixed4 black = float4(0.0, 0.0, 0.0, 1.0);
	fixed4 al = float4(0.0, 0.0, 0.0, 1.0);
	
	tex.rgb = lerp (tex.rgb, decal.rgb, decal.a);
	al.a = abs(decal.a-1);
	tex.rgb = lerp (tex.rgb, black.rgb, al.a);
	
	_Color.rgb = lerp(_Color.rgb, black.rgb, decal.a);
	
	o.Albedo = tex.rgb + _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = 1;
	//o.Alpha = tex.a * _Color.a;
	o.Specular = _Shininess;
}
ENDCG
}

Fallback "VertexLit"
}
