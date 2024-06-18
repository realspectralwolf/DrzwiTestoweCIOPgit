using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] GunController gun;
    [SerializeField] WeaponPositionController tool;
    [SerializeField] DynamicCamera dynamicCamera;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        gun.isInUse = Input.GetMouseButton(1);
        tool.isInUse = (Input.GetMouseButton(0) && !Input.GetMouseButton(1));

        if (gun.isInUse && Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }

        dynamicCamera.isInUse = (gun.isInUse || tool.isInUse);
    }
}
