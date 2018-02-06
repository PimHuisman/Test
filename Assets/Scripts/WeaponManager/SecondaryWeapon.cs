using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeapon : MonoBehaviour
{
    public Text ammoText;
    //Ammo for ShotGun
    public int currentAmmoS;
    [SerializeField] private int maxAmmoS;
    [SerializeField] private int fireAmmoS;
    //Clip for ShotGun
    [SerializeField] private int maxClipS;
    [SerializeField] private int currentClipAmountS;
    [SerializeField] GameObject shotGunHole;
    private bool bulletHoleS;
    public RaycastHit hit;

    void Start()
    {
        currentClipAmountS = maxClipS;
        currentAmmoS = maxAmmoS;
        bulletHoleS = true;
    }

    void Update()
    {
        Reload();
        ShotGun();
        ShotGunAmmo();
    }
    void Reload()
    {
        if (Input.GetButtonDown("R"))
        {
            // ShotGun Reload
            if (fireAmmoS <= 0)
            {
                currentAmmoS = 0;
                bulletHoleS = false;
            }
            else
            {
                int needAmmo = maxClipS - currentClipAmountS;
                currentClipAmountS += needAmmo;
                currentAmmoS -= needAmmo;
                bulletHoleS = true;
            }
            if (currentAmmoS < maxClipS)
            {
                currentClipAmountS += currentAmmoS;
            }
            if (currentClipAmountS >= maxClipS)
            {
                currentClipAmountS = maxClipS;
            }
        }
    }
    void ShotGun()
    {
        //SecondaryWeapon
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1500f))
            {
                if (bulletHoleS)
                {
                    GameObject g = Instantiate(shotGunHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                    currentClipAmountS -= fireAmmoS;
                }
            }
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }
    }
    void ShotGunAmmo()
    {
        //ShotGunAmmo
        ammoText.text = ("Shotgun:" + currentClipAmountS + "/" + currentAmmoS);
        if (currentClipAmountS <= 0)
        {
            currentClipAmountS = 0;
            bulletHoleS = false;
        }
        if (currentClipAmountS >= maxClipS)
        {
            currentClipAmountS = maxClipS;
        }
        if (currentAmmoS <= 0)
        {
            currentAmmoS = 0;
        }
        if (currentAmmoS >= maxAmmoS)
        {
            currentAmmoS = maxAmmoS;
        }
    }
}
