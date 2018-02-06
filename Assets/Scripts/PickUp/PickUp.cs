using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject ePickUp;
    [SerializeField] private GameObject[] gun;
    [SerializeField] private Text itemInfo;
    void Update()
    {
        Pickup();
    }

    void Pickup()
    {
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, raycastLength))
        {
            if (hit.transform.tag == "AmmoPickUp")
            {
                string typeOfA = hit.transform.GetComponent<AmmoPickUp>().type;
                itemInfo.text = (typeOfA);
                ePickUp.SetActive(true);
                if (Input.GetButtonDown("E"))
                {
                    if (typeOfA == "Musket")
                    {
                        Destroy(hit.transform.gameObject);
                        int upAmmo = hit.transform.GetComponent<AmmoPickUp>().ammo;
                        gun[0].GetComponent<MainWeapons>().mayFire = true;
                        gun[0].GetComponent<MainWeapons>().AddAmmo(upAmmo);
                    }
                    if (typeOfA == "Shotgun")
                    {
                        Destroy(hit.transform.gameObject);
                        int upAmmo = hit.transform.GetComponent<AmmoPickUp>().ammo;
                        gun[1].GetComponent<MainWeapons>().mayFire = true;
                        gun[1].GetComponent<MainWeapons>().AddAmmo(upAmmo);
                    }
                }
            }
            if (hit.transform.tag == "HealthPickUp")
            {
                string typeOfH = hit.transform.GetComponent<HealthPickUp>().type;
                itemInfo.text = (typeOfH);
                ePickUp.SetActive(true);
                if (Input.GetButtonDown("E"))
                {
                    Destroy(hit.transform.gameObject);
                    float upHealth = hit.transform.GetComponent<HealthPickUp>().health;
                    gameObject.GetComponent<HealthManager>().UpHealth(upHealth);
                }
            }
        }
        else
        {
            ePickUp.SetActive(false);
        }
        Debug.DrawRay(cameraPosition.position, cameraPosition.forward * 2, Color.blue);
    }
}

