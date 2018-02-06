using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayShoot : MonoBehaviour
{
    [SerializeField] private float raycastLength;
    public RaycastHit hit;

    void Start ()
    {
		
	}
    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out hit, raycastLength);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
    }
}
