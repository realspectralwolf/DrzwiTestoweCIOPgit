using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHighlight : MonoBehaviour
{
    [SerializeField] MeshRenderer m_Renderer;
    LaserInteractable interactable;

    Color32 highlightColor = new Color32(52, 204, 235, 255);
    Color32 originalColor;

    private void OnEnable()
    {
        interactable = GetComponent<LaserInteractable>();
        interactable.OnLaserEnter += EnableHighlight;
        interactable.OnLaserExit += DisableHighlight;

        if (m_Renderer == null)
        {
            m_Renderer = GetComponent<MeshRenderer>();
        }
        
        originalColor = m_Renderer.material.color;
        // Tinted highlight
        highlightColor = Color.Lerp(highlightColor, originalColor, 0.1f);
    }

    private void OnDisable()
    {
        interactable.OnLaserEnter -= EnableHighlight;
        interactable.OnLaserExit -= DisableHighlight;
    }

    public void EnableHighlight()
    {
        m_Renderer.material.color = highlightColor;
    }

    public void DisableHighlight()
    {
        m_Renderer.material.color = originalColor;
    }
}
