using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LaserBodyPart : MonoBehaviour
{
    [SerializeField] List<InteractableItem> acceptableItems;
    [SerializeField] public bool isDrawer = false;
    [SerializeField] LaserHighlight highlight;

    public LaserInteractable heldItem = null;

    public System.Action OnStatusChanged;

    public void Adopt(LaserInteractable item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.bodyParentParent = this;
        if (!isDrawer) item.SetMeshToOther();

        heldItem = item;
        OnStatusChanged?.Invoke();
    }

    public void OnDeadopted()
    {
        heldItem = null;
        OnStatusChanged?.Invoke();
    }

    public void PreviewItem(LaserInteractable item)
    {
        item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;
        highlight?.EnableHighlight();

        if (!isDrawer) item.SetMeshToOther();
    }

    public void CancelPreview(LaserInteractable item)
    {
        highlight?.DisableHighlight();
        if (!isDrawer) item.SetMeshToDefault();
    }

    public bool CanAccept(LaserInteractable item)
    {
        if (item == heldItem || acceptableItems.Contains(InteractableItem.All)) return true;
        return (acceptableItems.Contains(item.itemType) && (heldItem == null || isDrawer));
    }
}
