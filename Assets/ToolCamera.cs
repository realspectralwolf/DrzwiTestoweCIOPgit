using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCamera : MonoBehaviour
{
    [SerializeField] Camera renderCamera;
    [SerializeField] Material targetMaterial;

    private RenderTexture renderTexture;

    void Start()
    {
        // Create the Render Texture
        renderTexture = new RenderTexture(Mathf.FloorToInt(Screen.width * 0.7f), Screen.height, 0);
        renderTexture.name = "CameraRenderTexture";

        // Optionally, you can set depth buffer and anti-aliasing
        renderTexture.depth = 24;
        renderTexture.antiAliasing = 1;

        // Assign the Render Texture to the camera
        renderCamera.targetTexture = renderTexture;

        // Assign the Render Texture to the material
        targetMaterial.mainTexture = renderTexture;

        // Disable camera rendering to screen
        renderCamera.targetDisplay = -1;
        renderCamera.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        // Clean up the Render Texture when the object is destroyed
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
    }
}