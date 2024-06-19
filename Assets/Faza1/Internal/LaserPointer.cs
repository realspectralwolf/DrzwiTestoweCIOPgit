using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask layerItem;
    [SerializeField] LayerMask layerDrawer;
    [SerializeField] LayerMask layerBodyPart;
    [SerializeField] float laserRenderMaxDistance = 100;
    [SerializeField] Color laserColorDefault;
    [SerializeField] Color laserColorClicked;

    LaserInteractable selectedObject = null;
    LaserInteractable selectedDrawer = null;
    LaserBodyPart selectedBodyPart = null;

    bool isDragging = false;
    float dragStartDistance = -1;
    bool isItemInPreview = false;

    Vector2 mousePos;
    Vector3 mouseWorldPos;
    Camera targetCamera;

    void Start()
    {
        targetCamera = Camera.main;
        lineRenderer.positionCount = 2;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();
        DrawLaser();
        HandleItemRaycast();
        HandleDrawerRaycast();
        HandleBodypartRaycast();
        HandleDragControls();
    }

    void HandleInput()
    {
        mousePos = Input.mousePosition;
        mouseWorldPos = targetCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, laserRenderMaxDistance));
    }

    void HandleDragControls()
    {
        if (selectedObject == null) return;
        if (selectedObject.itemType == InteractableItem.None) return;
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging)
            {
                isDragging = true;
                dragStartDistance = Vector3.Distance(transform.position, selectedObject.transform.position);
                selectedObject.LaserExit();
                selectedObject.transform.SetParent(null);
                selectedObject.transform.DOKill();
                selectedObject.bodyParentParent?.OnDeadopted();
                selectedDrawer?.OnLaserExit();

                selectedObject.transform.GetChild(0).DOPunchScale(Vector3.one * 0.2f, 0.2f, 0);
            }
            else
            {
                selectedObject.transform.GetChild(0).DOPunchScale(Vector3.one * 0.2f, 0.2f, 0);
                isDragging = false;
                bool doReset = true;
                if (selectedBodyPart != null)
                {
                    if (selectedBodyPart.CanAccept(selectedObject))
                    {
                        selectedBodyPart.Adopt(selectedObject);
                        selectedObject = null;
                        if (selectedBodyPart.isDrawer)
                        {
                            var drawer = selectedBodyPart.GetComponentInParent<LaserDrawer>();
                            drawer.OpenDrawer();
                        }
                        doReset = false;
                    }
                }

                if (doReset)
                {
                    selectedObject.transform.DOKill();
                    selectedObject.transform.SetParent(selectedObject.initialParent);
                    selectedObject.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
                }
            }
        }

        if (isDragging && !isItemInPreview)
        {
            Vector3 dir = (mouseWorldPos - transform.position).normalized;
            selectedObject.transform.position = transform.position + dir * dragStartDistance;
        }
    }

    void DrawLaser()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mouseWorldPos);
        transform.LookAt(mouseWorldPos);
        
        lineRenderer.startColor = Input.GetMouseButton(0) ? laserColorClicked : laserColorDefault;
        lineRenderer.endColor = Input.GetMouseButton(0) ? laserColorClicked : laserColorDefault;
    }

    void HandleItemRaycast()
    {
        if (isDragging) return;

        RaycastHit hit;
        Vector3 targetPos;
        if (Physics.Raycast(transform.position, mouseWorldPos, out hit, Mathf.Infinity, layerItem))
        {
            Transform objectHit = hit.transform;
            targetPos = hit.point;
            var hitObject = objectHit.GetComponent<LaserInteractable>();

            if (hitObject != selectedObject)
            {
                hitObject?.LaserEnter();
                selectedObject?.LaserExit();
                selectedObject = hitObject;
            }
        }
        else
        {
            selectedObject?.LaserExit();
            selectedObject = null;
        }
    }

    void HandleDrawerRaycast()
    {
        RaycastHit hit;
        Vector3 targetPos;

        if (Physics.Raycast(transform.position, mouseWorldPos, out hit, Mathf.Infinity, layerDrawer))
        {
            Transform objectHit = hit.transform;
            targetPos = hit.point;
            var hitDrawer = objectHit.GetComponent<LaserInteractable>();

            if (hitDrawer != null && hitDrawer != selectedDrawer)
            {   
                selectedDrawer?.OnLaserExit();
                if (!isDragging)
                {
                    hitDrawer.OnLaserEnter();
                }
                selectedDrawer = hitDrawer;
            }
        }
        else
        {
            selectedDrawer?.OnLaserExit();
            selectedDrawer = null;
        }
    }

    void HandleBodypartRaycast()
    {
        if (!isDragging) return;

        RaycastHit hit;
        Vector3 targetPos;

        if (Physics.Raycast(transform.position, mouseWorldPos, out hit, Mathf.Infinity, selectedObject.bodyPartLayer))
        {
            Transform objectHit = hit.transform;
            targetPos = hit.point;
            var hitBodyPart = objectHit.GetComponent<LaserBodyPart>();
            if (hitBodyPart.CanAccept(selectedObject))
            {
                hitBodyPart.PreviewItem(selectedObject);
                isItemInPreview = true;

                if (selectedBodyPart != hitBodyPart)
                {
                    selectedBodyPart?.CancelPreview(selectedObject);
                }
                selectedBodyPart = hitBodyPart;
            }
            else
            {
                selectedBodyPart?.CancelPreview(selectedObject);
                selectedBodyPart = null;
            }
        }
        else
        {
            selectedBodyPart?.CancelPreview(selectedObject);
            selectedBodyPart = null;
            isItemInPreview = false;
        }
    }
}
