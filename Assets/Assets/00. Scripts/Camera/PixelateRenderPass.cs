using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class PixelateRenderPass : ScriptableRenderPass
{
    [Range(1, 100)]
    public int pixelate = 10;

    private RenderTargetIdentifier source;
    private RenderTargetHandle temporaryTexture;

    public void Setup(RenderTargetIdentifier source)
    {
        this.source = source;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get("Pixelate Effect");

        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.width /= pixelate;
        descriptor.height /= pixelate;
        descriptor.depthBufferBits = 0;

        cmd.GetTemporaryRT(temporaryTexture.id, descriptor, FilterMode.Point);
        cmd.Blit(source, temporaryTexture.Identifier());
        cmd.Blit(temporaryTexture.Identifier(), source);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        if (cmd != null)
        {
            cmd.ReleaseTemporaryRT(temporaryTexture.id);
        }
    }
}