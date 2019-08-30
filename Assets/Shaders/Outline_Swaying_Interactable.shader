Shader "Toon/Outline Swaying Interactable" {
    Properties{
        _Color("Main Color", Color) = (.5,.5,.5,1)
        _MainTex("Base (RGB)", 2D) = "white" { }
        _Ramp("Toon Ramp (RGB)", 2D) = "gray" {}

        // swaying
        _Speed ("MoveSpeed", Range(0,50)) = 25 // speed of the swaying
        _Rigidness("Rigidness", Range(1,50)) = 25 // lower makes it look more "liquid" higher makes it look rigid
        _SwayMax("Sway Max", Range(0, 0.1)) = .005 // how far the swaying goes
        _YOffset("Y offset", float) = 0.5// y offset, below this is no animation

        // outline
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline width", Range(.002, 0.2)) = .005
        _OutlineZ("Outline Z", Range(-.002, 0)) = -.001// outline z offset
        
        _Offset("Outline Noise Offset", Range(0.5, 10)) = .005// noise offset
        _NoiseTex("Noise (RGB)", 2D) = "white" { }// noise texture
    
        [Toggle(NOISE)] _NOISE("Enable Noise?", Float) = 0

        // interactability
        _MaxWidth("Max Displacement Width", Range(0, 2)) = 0.1 // width of the line around the dissolve
        _Radius("Radius", Range(0,5)) = 1 // width of the line around the dissolve

    }
 
CGINCLUDE
#include "UnityCG.cginc"
#pragma shader_feature NOISE
    struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float4 texcoord : TEXCOORD0;// texture coordinates
    };
 
    struct v2f {
        float4 pos : SV_POSITION;
        UNITY_FOG_COORDS(0)
        fixed4 color : COLOR;
    };
 
    uniform float _Outline;
    uniform float _OutlineZ;// outline z offset
    uniform float4 _OutlineColor;
    sampler2D _NoiseTex;// noise texture
    float _Offset; // noise offset
    float _Radius;

    float _Speed;
    float _SwayMax;
    float _YOffset;
    float _Rigidness;
    float _MaxWidth;

    uniform float3 _Positions[100];
    uniform float _PositionArray;
 
    v2f vert(appdata v) {
        // sway first
        float3 wpos = mul(unity_ObjectToWorld, v.vertex).xyz;// world position
        float x = sin(wpos.x / _Rigidness + (_Time.x * _Speed)) *(v.vertex.y - _YOffset) * 5;// x axis movements
        float z = sin(wpos.z / _Rigidness + (_Time.x * _Speed)) *(v.vertex.y - _YOffset) * 5;// z axis movements
        v.vertex.x += step(0,v.vertex.y - _YOffset) * x * _SwayMax;// apply the movement if the vertex's y above the YOffset
        v.vertex.z += step(0,v.vertex.y - _YOffset) * z * _SwayMax;

        // interaction radius movement for every position in array
        for (int i = 0; i < _PositionArray; i++){
            float3 dis =  distance(_Positions[i], wpos); // distance for radius
            float3 radius = 1-  saturate(dis /_Radius); // in world radius based on objects interaction radius
            float3 sphereDisp = wpos - _Positions[i]; // position comparison
            sphereDisp *= radius; // position multiplied by radius for falloff
            v.vertex.xz += clamp(sphereDisp.xz * step(_YOffset, v.vertex.y), -_MaxWidth,_MaxWidth);// vertex movement based on falloff and clamped
        }

        // then get outline info
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);
        float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));

        float2 offset = TransformViewToProjection(norm.xy);
 
        float4 tex = tex2Dlod(_NoiseTex, float4(v.texcoord.xy, 0, 0) * _Offset);// noise texture based on texture coordinates and offset
 
            #ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE //to handle recent standard asset package on older version of unity (before 5.5)
            #if NOISE // switch for noise
                    o.pos.xy += offset * _Outline * (tex.r);// add noise
            #else
                    o.pos.xy += offset * _Outline;// or not
            #endif
                    o.pos.z += _OutlineZ;// push away from camera
            #else
                    o.pos.xy += offset * o.pos.z * _Outline;
            #endif

        o.color = _OutlineColor;
        UNITY_TRANSFER_FOG(o, o.pos);
        return o;
    }
    ENDCG
 
SubShader{
    Tags{ "RenderType" = "Opaque" }
    UsePass "Toon/Lit Swaying Interactive/FORWARD"
    Pass{
        Name "OUTLINE"
        Tags{ "LightMode" = "Always" }
        Cull Off// we dont want to cull
        ZWrite On
        ColorMask RGB
 
 
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        // #pragma multi_compile_fog   // uncomment later for smaller LOD (in distance)
                fixed4 frag(v2f i) : SV_Target
            {
                UNITY_APPLY_FOG(i.fogCoord, i.color);
            return i.color;
            }
        ENDCG
        }
    }
 
        Fallback "Toon/Basic"
}