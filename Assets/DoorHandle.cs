using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] Door door;
    public void RequestOpen()
    {
        door.RequestOpen();
    }
}
