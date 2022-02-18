using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderPipelineMaterial : MonoBehaviour
{

    public Material renderPipelineMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
       /// renderPipelineMaterial.SetTexture("_MainTex", destination);
        Graphics.Blit(source, destination, renderPipelineMaterial);
    }
}
