using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    public void Destroy(GameObject gObject)
    {
        Destroy(gObject,4);
    }
}
