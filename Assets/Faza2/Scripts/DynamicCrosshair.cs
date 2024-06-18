using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    [SerializeField] WeaponPositionController gun;
    [SerializeField] WeaponPositionController tool;

    [Header("Options")]
    [SerializeField] Vector3 inUseScale;
    Vector3 initialScale;

    [SerializeField] Color inUseColor;
    [SerializeField] Color defaultColor;

    [SerializeField] float transitionSpeed = 12f;
    [SerializeField] Image crosshairImage;

    [HideInInspector] public bool isInUse = false;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        Vector3 targetScale;
        Color targetColor;
        float smoothStep = transitionSpeed * Time.deltaTime;

        if (tool.isInUse)
        {
            crosshairImage.color = Color.Lerp(crosshairImage.color, new Color(0,0,0,0), smoothStep);
            return;
        }

        if (gun.isInUse)
        {
            targetScale = inUseScale;
            targetColor = inUseColor;
        }
        else
        {
            targetScale = initialScale;
            targetColor = defaultColor;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, smoothStep);
        crosshairImage.color = Color.Lerp(crosshairImage.color, targetColor, smoothStep);
    }
}
