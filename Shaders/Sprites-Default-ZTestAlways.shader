// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Base Shader: builtin_shaders-2020.1.5f1\DefaultResourcesExtra\Sprites-Default.shader
// Added "ZTest Always". This forces the sprite to always be drawn in front of 3D objects.
// Use case: Added a gradient sprite at the edge of Flow's forest so it fades off screen.
// https://forum.unity.com/threads/render-sprite-in-front-of-everything.580945/#post-3879802
// https://support.unity.com/hc/en-us/articles/210290023-Is-there-a-simple-way-to-make-a-sprite-render-in-front-of-everything-in-the-scene-


Shader "Sprites/Default-ZTestAlways"
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
        ZTest Always // Added this to force the sprite to be drawn in front of 3D objects.
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
        ENDCG
        }
    }
}
