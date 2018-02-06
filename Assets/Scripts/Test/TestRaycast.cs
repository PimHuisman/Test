using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform barrelEnd;

	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward * -10, Color.red);
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletInstance;
            bulletInstance = Instantiate(bullet,barrelEnd.position, barrelEnd.rotation) as Rigidbody;
            bulletInstance.AddForce(barrelEnd.forward * 700);
        }
    }
}
