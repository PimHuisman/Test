using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelect : MonoBehaviour
{
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float raycastLength;
    [SerializeField] private Text itemInfo;
    public List<string> type = new List<string>();
    [SerializeField] GameObject[] gun;
    [SerializeField] private GameObject eShop;
    private Transform score;

    void Start ()
    {
        score = GameObject.FindGameObjectWithTag("ScoreManager").transform;      
    }

	void FixedUpdate ()
    {
        Shop();
    }

    void Shop ()
    {
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, raycastLength))
        {
            if (hit.transform.tag == "Shop")
            {
                string typeOf = hit.transform.GetComponent<Shop>().type;
                string typeInfo = hit.transform.GetComponent<Shop>().info;
                int points = hit.transform.GetComponent<Shop>().points;
                itemInfo.text = (typeInfo + "(" + points + ")" );
                eShop.SetActive(true);
                if (Input.GetButtonDown("E"))
                {
                    int currentpoints = score.GetComponent<ScoreManager>().currentPoints;
                    if (typeOf == type[0])
                    {
                        if (typeInfo == "Musket")
                        {
                            if (currentpoints >= points)
                            {
                                int upAmmo = hit.transform.GetComponent<Shop>().amount;
                                gun[0].GetComponent<MainWeapons>().AddAmmo(upAmmo);
                            }
                            int downPoints = hit.transform.GetComponent<Shop>().points;
                            score.GetComponent<ScoreManager>().Points(0, downPoints);
                            gun[0].GetComponent<MainWeapons>().mayFire = true;
                        }
                        if (typeInfo == "Shotgun")
                        {
                            if (currentpoints >= points)
                            {
                                int upAmmo = hit.transform.GetComponent<Shop>().amount;
                                gun[1].GetComponent<MainWeapons>().AddAmmo(upAmmo);
                            }
                            int downPoints = hit.transform.GetComponent<Shop>().points;
                            score.GetComponent<ScoreManager>().Points(0, downPoints);
                            gun[1].GetComponent<MainWeapons>().mayFire = true;
                        }
                        if (typeInfo == "Experimental")
                        {
                            if (currentpoints >= points)
                            {
                                int upAmmo = hit.transform.GetComponent<Shop>().amount;
                                gun[2].GetComponent<WeaponAbility>().AddAmmo(upAmmo);
                            }
                            int downPoints = hit.transform.GetComponent<Shop>().points;
                            score.GetComponent<ScoreManager>().Points(0, downPoints);
                            gun[2].GetComponent<WeaponAbility>().mayFire = true;
                        }
                    }
                    if (typeOf == type[1])
                    {
                        if (typeInfo == "Health")
                        {
                            if (currentpoints >= points)
                            {
                                float upAmount = hit.transform.GetComponent<Shop>().amount;
                                gameObject.GetComponent<HealthManager>().UpHealth(upAmount);
                            }
                            int downPoints = hit.transform.GetComponent<Shop>().points;
                            score.GetComponent<ScoreManager>().Points(0, downPoints);
                        }
                    }
                }
            }
        }
        else
        {
            eShop.SetActive(false);
        }
        Debug.DrawRay(cameraPosition.position, cameraPosition.forward * 2, Color.blue);
    }
}
