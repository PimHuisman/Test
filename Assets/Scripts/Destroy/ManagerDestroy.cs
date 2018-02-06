using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDestroy : MonoBehaviour
{
    public void DestroyObject(GameObject gObject)

    {
        Destroy(gObject, 4);
    }
}
