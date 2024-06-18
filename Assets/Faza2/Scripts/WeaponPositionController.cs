using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPositionController : MonoBehaviour
{
    [SerializeField] Vector3 inUsePos;
    [SerializeField] Vector3 inUseRot;
    [SerializeField] Vector3 hiddenPos;
    Vector3 initialPos;
    Quaternion initialRot;

    [SerializeField] float transitionSpeed = 2f;
    [HideInInspector] public bool isInUse = false;
    [HideInInspector] public bool isHidden = false;

    public void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;
    }

    public void Update()
    {
        Vector3 targetPos;
        Quaternion targetRot;

        if (isInUse)
        {
            targetPos = inUsePos;
            targetRot = Quaternion.Euler(inUseRot);
        }
        else if (isHidden)
        {
            targetPos = initialPos;
            targetPos.y = hiddenPos.y;
            targetRot = initialRot;
        }
        else
        {
            targetPos = initialPos;
            targetRot = initialRot;
        }

        float smoothStep = transitionSpeed * Time.deltaTime;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, smoothStep);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, smoothStep);
    }
}
