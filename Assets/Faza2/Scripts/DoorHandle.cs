using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] public Door door;
    public void RequestOpen()
    {
        door.RequestOpen();
    }
}
