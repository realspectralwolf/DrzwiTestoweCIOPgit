using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    public static DrawerManager Instance;

    public List<LaserDrawer> laserDrawers = new();

    private void Awake()
    {
        Instance = this;
    }

    public void CloseAllOtherDrawers(LaserDrawer drawerException)
    {
        for (int i = 0; i < laserDrawers.Count; i++)
        {
            if (laserDrawers[i] == drawerException) continue;
            laserDrawers[i].CloseDrawer();
        }
    }
}
