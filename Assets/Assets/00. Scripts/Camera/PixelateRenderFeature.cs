using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelateRenderFeature : ScriptableRendererFeature
{
    class PixelateRenderPassWrapper : ScriptableRenderPass
    {
        public PixelateRenderPass pass;

        public PixelateRenderPassWrapper()
        {
            pass = new PixelateRenderPass();
            pass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            pass.Execute(context, ref renderingData);
        }
    }

    PixelateRenderPassWrapper pixelateRenderPassWrapper;

    public override void Create()
    {
        pixelateRenderPassWrapper = new PixelateRenderPassWrapper();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var cameraColorTarget = renderer.cameraColorTarget;
        pixelateRenderPassWrapper.pass.Setup(cameraColorTarget);
        renderer.EnqueuePass(pixelateRenderPassWrapper);
    }
}