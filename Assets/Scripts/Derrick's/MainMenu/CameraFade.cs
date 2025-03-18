using UnityEngine;

[ExecuteAlways]
public class CameraFade : MonoBehaviour
{
    public Material fadeMaterial;
    public float fadeRadius = 5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (fadeMaterial != null && mainCamera != null)
        {
            fadeMaterial.SetVector("_CameraPosition", mainCamera.transform.position);
            fadeMaterial.SetFloat("_FadeRadius", fadeRadius);
            Debug.Log("Camera Position: " + mainCamera.transform.position + ", Fade Radius: " + fadeRadius);

        }
    }
}
