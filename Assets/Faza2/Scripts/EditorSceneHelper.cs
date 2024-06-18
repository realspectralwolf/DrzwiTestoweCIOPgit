using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSceneHelper : MonoBehaviour
{
    public bool button = false;
    [SerializeField] Material altWallM;

    void Update()
    {
        if (button)
        {
            button = false;
            DoAction();
        }
    }

    void DoAction()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var obj = transform.GetChild(i);
            
            if (obj.childCount != 0)
            {
                var childw = obj.GetChild(0);

                if (childw != null)
                {
                    DestroyImmediate(childw.gameObject);
                }
            }

            var child = Instantiate(obj, obj.transform);
            child.localScale = Vector3.one;
            child.localRotation = Quaternion.Euler(Vector3.zero);
            child.localPosition = Vector3.zero;

            DestroyImmediate(child.GetComponent<MeshCollider>());
            child.gameObject.layer = 10;
            child.GetComponent<MeshRenderer>().material = altWallM;
        }
    }
}
