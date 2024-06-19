using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LaserInteractable))]
public class LaserDrawer : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] float itemSpacing = 1.5f;
    [SerializeField] float animTime = 1f;
    [SerializeField] BoxCollider colliderWhenClosed;
    [SerializeField] BoxCollider colliderWhenOpened;
    LaserInteractable interactable;

    private void Start()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).gameObject.SetActive(false);
        }
        DrawerManager.Instance.laserDrawers.Add(this);
    }

    private void OnEnable()
    {
        interactable = GetComponent<LaserInteractable>();
        interactable.OnLaserEnter += OpenDrawer;
        interactable.OnLaserExit += CloseDrawer;
    }


    private void OnDisable()
    {
        interactable.OnLaserEnter -= OpenDrawer;
        interactable.OnLaserExit -= CloseDrawer;
        DrawerManager.Instance.laserDrawers.Remove(this);
    }

    public void OpenDrawer()
    {
        OpenDrawerSequence();
        DrawerManager.Instance.CloseAllOtherDrawers(this);
    }

    void FixPos(Transform target)
    {
        Vector3 fixedPos = target.localPosition;
        fixedPos.y = 0;
        fixedPos.x = 0;
        target.localPosition = fixedPos;
    }

    void OpenDrawerSequence()
    {
        ToggleColliderState(isOpened: true);

        int childCount = container.childCount;
        UpdateOpenedColliderSize(childCount);

        for (int i = 0; i < childCount; i++)
        {
            container.GetChild(i).DOKill();
            FixPos(container.GetChild(i));
        }

        for (int i = 0; i < childCount; i++)
        {
            var childObject = container.GetChild(i).gameObject;
            container.GetChild(i).DOLocalMoveZ((i + 1) * itemSpacing, animTime * childCount).SetEase(Ease.InSine)
            .SetDelay(0)
            .OnPlay(() =>
            {
                childObject.SetActive(true);
            }).OnComplete(() =>
            {
                childObject.GetComponent<LaserInteractable>().EnableRaycastCollider();
            });
        }
    }

    void UpdateOpenedColliderSize(int childCount)
    {
        Vector3 newSize = colliderWhenOpened.size;
        newSize.z = (childCount + 1) * itemSpacing;
        colliderWhenOpened.size = newSize;
        Vector3 newCenter = colliderWhenOpened.center;
        newCenter.z = newSize.z / 2f;
        colliderWhenOpened.center = newCenter;
    }

    public void CloseDrawer()
    {
        ToggleColliderState(isOpened: false);

        int childCount = container.childCount;
        for (int i = 0; i < childCount; i++)
        {
            container.GetChild(i).DOKill();
            FixPos(container.GetChild(i));
        }

        for (int i = 0; i < childCount; i++)
        {
            var childObject = container.GetChild(i).gameObject;
            childObject.GetComponent<LaserInteractable>().DisableRaycastCollider();
            container.GetChild(i).DOLocalMoveZ(0, animTime * childCount).SetEase(Ease.InOutExpo)
            .OnComplete(() =>
            {
                childObject.SetActive(false);
            });
        }
    }

    void ToggleColliderState(bool isOpened)
    {
        colliderWhenOpened.enabled = isOpened;
        colliderWhenClosed.enabled = !isOpened;
    }
}
