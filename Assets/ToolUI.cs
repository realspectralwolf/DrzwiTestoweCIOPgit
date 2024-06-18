using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Canvas canvas;
    [SerializeField] WeaponPositionController weaponController;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] Transform raycastSource;

    Text[] texts;
    Image[] images;

    private void Start()
    {
        texts = new Text[container.childCount];
        images = new Image[container.childCount];
        for (int i =  0; i < container.childCount; i++)
        {
            images[i] = container.GetChild(i).GetComponent<Image>();
            texts[i] = container.GetChild(i).GetChild(0).GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (weaponController.isInUse)
        {
            canvas.gameObject.SetActive(true);
            RaycastHit hit;
            Vector3 targetPos;
            if (Physics.Raycast(raycastSource.position, raycastSource.forward, out hit, Mathf.Infinity, doorLayer))
            {
                Transform objectHit = hit.transform;
                targetPos = hit.point;
                var hitObject = objectHit.GetComponentInParent<Door>();

                UpdateIndicatorUI(hitObject.requiredGunSetting);
            }
            else
            {
                UpdateIndicatorUI(-1);
            }
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }

    void UpdateIndicatorUI(int targetValue)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            byte newAlphaValue = 70;
            if (i == targetValue)
            {
                newAlphaValue = 255;
            }

            Color32 currentColor = texts[i].color;
            texts[i].color = new Color32(currentColor.r, currentColor.g, currentColor.b, newAlphaValue);

            currentColor = images[i].color;
            images[i].color = new Color32(currentColor.r, currentColor.g, currentColor.b, newAlphaValue);
        }
    }
}
