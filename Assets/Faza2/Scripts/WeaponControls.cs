using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControls : MonoBehaviour
{
    [SerializeField] GunController gun;
    [SerializeField] WeaponPositionController tool;
    [SerializeField] DynamicCamera dynamicCamera;
    [SerializeField] GameObject tooltipDefault;
    [SerializeField] GameObject tooltipFlipped;

    public static bool doFlipHands = false;
    bool doFlipControls = false;

    private void Awake()
    {
        if (doFlipHands)
        {
            doFlipHands = false;
            FlipWeaponHands();
        }
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        tool.isInUse = (Input.GetMouseButton(doFlipControls ? 1: 0) && !gun.isInUse);

        if (!tool.isInUse)
        {
            if (Input.GetMouseButtonDown(doFlipControls ? 0 : 1))
            {
                gun.isInUse = !gun.isInUse;
            }
        }

        gun.isHidden = tool.isInUse;
        tool.isHidden = gun.isInUse;

        if (gun.isInUse && Input.GetMouseButtonDown(doFlipControls ? 1 : 0))
        {
            gun.Shoot();
        }

        dynamicCamera.isInUse = (gun.isInUse || tool.isInUse);
    }

    public void FlipWeaponHands() // Call before Start
    {
        gun.inUsePos = new Vector3(-gun.inUsePos.x, gun.inUsePos.y, gun.inUsePos.z);
        tool.inUsePos = new Vector3(-tool.inUsePos.x, tool.inUsePos.y, tool.inUsePos.z);

        gun.inUseRot = new Vector3(gun.inUseRot.x, -gun.inUseRot.y, gun.inUseRot.z);
        tool.inUseRot = new Vector3(tool.inUseRot.x, -tool.inUseRot.y, tool.inUseRot.z);

        gun.hiddenPos = new Vector3(-gun.hiddenPos.x, gun.hiddenPos.y, gun.hiddenPos.z);
        tool.hiddenPos = new Vector3(-tool.hiddenPos.x, tool.hiddenPos.y, tool.hiddenPos.z);

        Vector3 gunPos = gun.transform.localPosition;
        gun.transform.localPosition = new Vector3(-gunPos.x, gunPos.y, gunPos.z);
        Vector3 toolPos = tool.transform.localPosition;
        tool.transform.localPosition = new Vector3(-toolPos.x, toolPos.y, toolPos.z);

        Vector3 gunRot = gun.transform.localRotation.eulerAngles;
        gun.transform.localRotation = Quaternion.Euler(new Vector3(gunRot.x, -gunRot.y, gunRot.z));
        Vector3 toolRot = tool.transform.localRotation.eulerAngles;
        tool.transform.localRotation = Quaternion.Euler(new Vector3(toolRot.x, -toolRot.y, toolRot.z));

        doFlipControls = true;

        tooltipDefault.SetActive(false);
        tooltipFlipped.SetActive(true);
    }
}
