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
        tool.isInUse = (Input.GetMouseButton(0) && !gun.isInUse);

        if (!tool.isInUse)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gun.isInUse = !gun.isInUse;
            }
        }

        gun.isHidden = tool.isInUse;
        tool.isHidden = gun.isInUse;

        if (gun.isInUse && Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }

        dynamicCamera.isInUse = (gun.isInUse || tool.isInUse);
    }
}
