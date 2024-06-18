using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPositionController : MonoBehaviour
{
    [SerializeField] Vector3 inUsePos;
    [SerializeField] Vector3 inUseRot;
    Vector3 initialPos;
    Quaternion initialRot;

    [SerializeField] float transitionSpeed = 2f;
    [HideInInspector] public bool isInUse = false;

    void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;
    }

    void Update()
    {
        Vector3 targetPos;
        Quaternion targetRot;

        if (isInUse)
        {
            targetPos = inUsePos;
            targetRot = Quaternion.Euler(inUseRot);
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
