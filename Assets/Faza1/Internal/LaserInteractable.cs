using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LaserInteractable : MonoBehaviour
{
    [SerializeField] public InteractableItem itemType;
    [SerializeField] public LayerMask bodyPartLayer;

    [SerializeField] GameObject defaultMesh;
    [SerializeField] GameObject otherMesh;

    [HideInInspector] public Transform initialParent;
    [HideInInspector] public LaserBodyPart bodyParentParent = null;

    public System.Action OnLaserEnter;
    public System.Action OnLaserExit;

    private void Awake()
    {
        initialParent = transform.parent;
    }

    public void LaserEnter()
    {
        OnLaserEnter?.Invoke();
    }

    public void LaserExit()
    {
        OnLaserExit?.Invoke();
    }

    public void SetMeshToDefault()
    {
        if (otherMesh == null || defaultMesh == null) return;
        defaultMesh.gameObject.SetActive(true);
        otherMesh.gameObject.SetActive(false);
        Debug.Log("DEFAULT MESH");
    }

    public void SetMeshToOther()
    {
        if (otherMesh == null || defaultMesh == null) return;
        defaultMesh.gameObject.SetActive(false);
        otherMesh.gameObject.SetActive(true);
    }

    public void EnableRaycastCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void DisableRaycastCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
}

public enum InteractableItem
{
    None,
    All,
    Helmet,
    Vest,
    Glove,
    Boot,
    Belt,
    GasMask,
    Tool,
    Gun
}