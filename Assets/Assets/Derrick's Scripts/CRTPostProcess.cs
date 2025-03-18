using UnityEngine;

[ExecuteInEditMode]
public class CRTPostProcess : MonoBehaviour
{
    public Shader crtShader;
    private Material crtMaterial;

    void Start()
    {
        crtShader = Shader.Find("Hidden/CRT_Shader");

        if (crtShader != null)
        {
            crtMaterial = new Material(crtShader);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (crtMaterial != null)
        {
            Graphics.Blit(source, destination, crtMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}