using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCamera : MonoBehaviour
{
    [Range(1, 100)]
    public int pixelate;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture texture = RenderTexture.GetTemporary(source.width / pixelate, source.height / pixelate, 0, source.format);
        texture.filterMode = FilterMode.Point;
        Graphics.Blit(source, texture);
        Graphics.Blit(texture, destination);
        RenderTexture.ReleaseTemporary(texture);
    }
}
