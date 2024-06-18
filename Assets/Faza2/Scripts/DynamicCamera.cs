using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] float inUseFov;
    float defaultFov;

    Camera targetCamera;

    [SerializeField] float transitionSpeed = 12f;
    [HideInInspector] public bool isInUse = false;

    void Start()
    {
        targetCamera = GetComponent<Camera>();
        defaultFov = targetCamera.fieldOfView;
    }

    void Update()
    {
        float targetFov;

        if (isInUse)
        {
            targetFov = inUseFov;
        }
        else
        {
            targetFov = defaultFov;
        }

        float smoothStep = transitionSpeed * Time.deltaTime;
        targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, targetFov, smoothStep);
    }
}
