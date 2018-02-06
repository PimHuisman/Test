using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbility : MonoBehaviour
{
    [Header("Text/Gameobject")]
    [SerializeField]
    private string weaponType;
    [SerializeField] private GameObject outofAmmo;
    [SerializeField] private GameObject needtoReload;
    [SerializeField] private Text reloadText;
    [SerializeField] private GameObject reloadSlider;
    [SerializeField] private Slider reload;
    [SerializeField] private string timerReload;

    [Header("Sound")]
    private AudioSource audioSourse;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioSource reloading;

    [Header("Aiming")]
    private Camera cameraFOV;
    private bool aiming;
    private float defaultFOV;
    private float aimFOV;
    [SerializeField] private Vector3 normalPos;
    [SerializeField] private Vector3 aimingPos;
    [SerializeField] private float changeFOV;
    [SerializeField] private float fovSpeed;

    [Header("ReloadTimer")]
    public bool mayFire;
    private bool timeSwitch;
    private float currentTime;
    [SerializeField] private float reloadTime;

    [Header("Recoil")]
    private Transform recoilT;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private Vector3 recoil;
    [SerializeField] private float recoilAmount;
    [SerializeField] private float aimSpeed;

    [Header("CrossHair")]
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject crossHair2;

    [Header("Clip (Magazine)")]
    [SerializeField]
    private int maxClip;
    [SerializeField] private int currentClipAmount;

    [Header("Sway")]
    private Vector3 newSwayPosition;
    [SerializeField] private float xSwayAmount;
    [SerializeField] private float ySwayAmount;
    [SerializeField] private float xSwayMax;
    [SerializeField] private float ySwayMax;
    [SerializeField] private float swaySmoothAmount;

    [Header("Ammo")]
    public int currentAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int fireAmmo;

    [SerializeField] private Text ammoText;
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private Transform barrelEnd;
    //RayCastBullets
    private RaycastHit hit;
    private bool bulletHole;

    void Start()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraFOV = camera.GetComponent<Camera>();
        defaultFOV = cameraFOV.fieldOfView; // Get Default FOV
        aimFOV = cameraFOV.fieldOfView - changeFOV; // Get Aim FOV
        reload.value = CalculateTime();
        recoilT = camera.GetComponent<Transform>();
        audioSourse = GetComponent<AudioSource>();
        currentClipAmount = maxClip;
        currentAmmo = maxAmmo;
        currentTime = reloadTime;
        mayFire = true;
        timeSwitch = false;
        bulletHole = true;

    }
    void FixedUpdate()
    {
        WeaponSway();
        isAiming();
    }
    void Update()
    {
        Reload();
        HarpoonAmmo();
        Weapon();
        ReloadTimer();
    }
    void Weapon()
    {
        // Weapon Functions
        if (Input.GetButtonDown("Fire1"))
        {
            if (mayFire)
            {
                if (bulletHole)
                {
                    if (currentClipAmount > 0)
                    {
                        if (timeSwitch)
                        {
                            timeSwitch = false;
                            reloadSlider.SetActive(false);
                            currentTime = reloadTime;
                        }
                        Rigidbody bulletInstance;
                        bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
                        bulletInstance.velocity = barrelEnd.forward * 50;
                        currentClipAmount -= fireAmmo;

                        // Recoil Movement
                        Vector3 weaponLocalPosition = weaponObject.transform.localPosition;
                        weaponLocalPosition.z = weaponLocalPosition.z -= recoilAmount;
                        weaponObject.transform.localPosition = weaponLocalPosition;
                        audioSourse.PlayOneShot(shoot);
                        recoilT.Rotate(recoil);
                        print(bulletInstance);
                    }
                }
            }
        }
        if (Input.GetButton("Fire2"))
        {
            aiming = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            aiming = false;
        }
    }
    void ReloadTimer()
    {
        if (timeSwitch)
        {
            currentTime -= Time.deltaTime;
            reloadText.text = (timerReload);
            reload.value = CalculateTime();
        }
    }
    void Reload()
    {
        if (Input.GetButtonDown("R"))
        {
            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
                bulletHole = false;
            }
            else
            {
                int needAmmo = maxClip - currentClipAmount;
                currentClipAmount += needAmmo;
                currentAmmo -= needAmmo;
                bulletHole = true;
            }
            if (currentAmmo < maxClip)
            {
                currentClipAmount += currentAmmo;
            }
            if (currentClipAmount >= maxClip)
            {
                currentClipAmount = maxClip;
            }
        }
    }
    void HarpoonAmmo()
    {
        {
            ammoText.text = ("Exper:" + currentClipAmount + "/" + currentAmmo);
            if (currentClipAmount <= 0)
            {
                currentClipAmount = 0;
                bulletHole = false;
            }
            if (currentClipAmount >= maxClip)
            {
                currentClipAmount = maxClip;
            }
            if (currentAmmo <= 0)
            {
                currentAmmo = 0;
            }
        }
    }
    float CalculateTime()
    {
        return currentTime / reloadTime;
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;
    }

    void WeaponSway()
    {
        float x = Input.GetAxis("Mouse X") * xSwayAmount;
        float y = Input.GetAxis("Mouse Y") * ySwayAmount;

        if (x > xSwayMax)
        {
            x = xSwayMax;
        }
        if (x < -xSwayMax)
        {
            x = -xSwayMax;
        }
        if (y > ySwayMax)
        {
            y = xSwayMax;
        }
        if (y < -ySwayMax)
        {
            y = -ySwayMax;
        }

        Vector3 detection = new Vector3(newSwayPosition.x + x, newSwayPosition.y + y, newSwayPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, detection, Time.deltaTime * swaySmoothAmount);
    }

    void isAiming()
    {
        if (aiming)
        {
            crossHair.SetActive(false);
            crossHair2.SetActive(true);
            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, aimingPos, Time.deltaTime * aimSpeed);
            cameraFOV.fieldOfView = Mathf.Lerp(cameraFOV.fieldOfView, aimFOV, Time.deltaTime * fovSpeed);
        }
        else
        {
            crossHair.SetActive(true);
            crossHair2.SetActive(false);
            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, normalPos, Time.deltaTime * aimSpeed);
            cameraFOV.fieldOfView = Mathf.Lerp(cameraFOV.fieldOfView, defaultFOV, Time.deltaTime * fovSpeed);
        }
    }
}
