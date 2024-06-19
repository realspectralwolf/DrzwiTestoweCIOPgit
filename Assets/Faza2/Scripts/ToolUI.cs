using DG.Tweening;
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
    [SerializeField] float toolRange = 5;
    [SerializeField] Text textTooFar;

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

    float showTimer = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.PlaySound("scan");

            showTimer = 1.5f;
            container.gameObject.SetActive(true);
            RaycastHit hit;
            Vector3 targetPos;
            if (Physics.Raycast(raycastSource.position, raycastSource.forward, out hit, Mathf.Infinity, doorLayer))
            {
                Transform objectHit = hit.transform;
                targetPos = hit.point;

                if (Vector3.Distance(hit.point, transform.position) <= toolRange)
                {
                    var hitObject = objectHit.GetComponentInParent<Door>();
                    UpdateIndicatorUI(hitObject.requiredGunSetting);
                    hitObject.ProcessOnInspectedByUser();

                    textTooFar.enabled = false;
                    textTooFar.DOKill();
                }
                else
                {
                    UpdateIndicatorUI(-1);

                    textTooFar.DOKill();
                    textTooFar.enabled = true;
                    textTooFar.color = Color.white;
                    textTooFar.DOColor(new Color32(255, 255, 255, 0), 0)
                        .SetDelay(1.5f)
                        .OnComplete(() => { textTooFar.enabled = false; });
                }
            }
            else
            {
                UpdateIndicatorUI(-1);
            }
        }
        if (showTimer > 0)
        {
            showTimer -= Time.deltaTime;
        }
        else
        {
            container.gameObject.SetActive(false);
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
