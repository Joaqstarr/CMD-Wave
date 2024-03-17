Shader "Unlit/RadarStencilShader"
{
    Properties
    {

        [IntRange] _StencilID("Stencil ID", Range(0, 255)) = 0
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
            "RenderPipeline" = "HighDefinitionRenderPipeline"
            "Queue" = "Geometry-4"
        }
        LOD 100
         Stencil {
            Ref[_StencilID]
            Comp always
            Pass replace
        }
        Pass
        {
            Blend Zero One
            ZWrite Off

            /*
            Stencil
            {
                Ref [_StencilID]
                Comp Always
                Pass Replace
                Fail Keep
            }
            */
        }
    }
}
