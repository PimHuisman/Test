using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [Header("Type")]
    public float health;
    public string type;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
