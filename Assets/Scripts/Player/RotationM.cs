using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationM : MonoBehaviour
{
    private Vector3 v;
    private Vector3 v02;
    public float speed;
    public float speedC;

	void Start ()
    {
		
	}
	

	void Update ()
    {
        v.z = Input.GetAxis("Vertical");
        v.x = Input.GetAxis("Horizontal");
        transform.Translate(v*speed);

        v02.y = Input.GetAxis("Mouse X");
        transform.Rotate(v02*speedC);
    }
}
