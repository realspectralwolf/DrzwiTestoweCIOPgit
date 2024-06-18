using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDoorOpener : MonoBehaviour
{
    [SerializeField] Camera targetCamera;
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] Image doorInteractionUI;
    [SerializeField] float distanceRequired = 2;

    DoorHandle selectedDoorHandle = null;
    void Update()
    {
        RaycastHit hit;
        Vector3 targetPos;
        if (Physics.Raycast(targetCamera.transform.position, targetCamera.transform.forward, out hit, Mathf.Infinity, raycastLayer))
        {
            Transform objectHit = hit.transform;
            targetPos = hit.point;

            if (Vector3.Distance(transform.position, hit.point) < distanceRequired)
            {
                selectedDoorHandle = objectHit.GetComponent<DoorHandle>();
            }
            else
            {
                selectedDoorHandle = null;
            }
        }
        else
        {
            selectedDoorHandle = null;
        }

        if (selectedDoorHandle != null)
        {
            doorInteractionUI.enabled = true;
            var screenPos = targetCamera.WorldToScreenPoint(selectedDoorHandle.transform.position);
            doorInteractionUI.transform.position = screenPos;

            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedDoorHandle.RequestOpen();
            }
        }
        else
        {
            doorInteractionUI.enabled = false;
        }
    }
}
