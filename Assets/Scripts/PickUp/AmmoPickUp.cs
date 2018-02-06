using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [Header("Type")]
    public int ammo;
    public string type;

    void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
