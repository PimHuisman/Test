using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWeapons : MonoBehaviour
{
    [Header("Text/Gameobject")]
    [SerializeField] private Text ammoText;
    [SerializeField] private string weaponType;
    [SerializeField] private GameObject outofAmmo;
    [SerializeField] private GameObject needtoReload;
    [SerializeField] private Text reloadText;
    [SerializeField] private GameObject reloadSlider;
    [SerializeField] private Slider reload;
    [SerializeField] private string timerReload;

    [Header("Sound")]
    private AudioSource audioSourse;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip hitMarkerSound;
    [SerializeField] private AudioSource reloading;
    [SerializeField] private float volumeScale;

    [Header("Aiming")]
    private Camera cameraFOV;
    private bool aiming;
    private float defaultFOV;
    private float aimFOV;
    [SerializeField] private Vector3 normalPos;
    [SerializeField] private Vector3 aimingPos;
    [SerializeField] private float changeFOV;
    [SerializeField] private float fovSpeed;

    [Header("Sway")]
    private Vector3 newSwayPosition;
    [SerializeField] private float xSwayAmount;
    [SerializeField] private float ySwayAmount;
    [SerializeField] private float xSwayMax;
    [SerializeField] private float ySwayMax;
    [SerializeField] private float swaySmoothAmount;

    [Header("Recoil")]
    private Transform recoilT;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private Vector3 recoil;
    [SerializeField] private float recoilAmount;
    [SerializeField] private float aimSpeed;

    [Header("CrossHair")]
    [SerializeField] private GameObject crossHair1;
    [SerializeField] private GameObject crossHair2;

    [Header("Ammo")]
    public int currentAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int fireAmmo;

    [Header("Clip (Magazine)")]
    [SerializeField] private int maxClip;
    [SerializeField] private int currentClipAmount;

    [Header("RayCastBullets")]
    private RaycastHit hit;
    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject bloodHole;
    [SerializeField] private GameObject houseHole;
    [SerializeField] private GameObject normalHole;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform barrelEnd;

    [Header("muzzelFlash")]
    private float muzzelFlashTimer;
    private bool muzzelFlachEnabled;
    [SerializeField] private GameObject muzzelFlash;
    [SerializeField] private float muzzelFlashTimerStart;

    [Header("Hitmarker")]
    private float hitMarkerTimer;
    private bool hitMarkerEnabled;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float hitMarkerTimerStart;

    [Header("ReloadTimer")]
    public bool mayFire;
    private bool timeSwitch;
    private float currentTime;
    [SerializeField] private float reloadTime;

    [Header("AddForce")]
    [SerializeField] private float inpactForce;

    [Header("FireRate")]
    [SerializeField] private float fireAgain;
    private bool fire;
    private float fireTime;

    [Header("Damage")]
    [SerializeField] private float damage;


    void Start()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraFOV =  camera.GetComponent<Camera>();
        recoilT = camera.GetComponent<Transform>();
        audioSourse = GetComponent<AudioSource>();
        mayFire = true;
        timeSwitch = false;
        fire = false;
        hitMarker.SetActive(false);
        aimFOV = cameraFOV.fieldOfView - changeFOV; // Get Aim FOV
        defaultFOV = cameraFOV.fieldOfView; // Get Default FOV
        muzzelFlashTimer = muzzelFlashTimerStart;
        hitMarkerTimer = hitMarkerTimerStart;
        reload.value = CalculateTime();
        currentClipAmount = maxClip;
        currentTime = reloadTime;
        currentAmmo = maxAmmo;
        fireTime = fireAgain;
    }
    void Update()
    {
        crossHair2.SetActive(false);
        AmmoCheck();
        Weapon();
        Reload();
        ReloadTimer();
        FireRate();
        MuzzelFlash();
        HitMarker();
    }
    void FixedUpdate()
    {
        WeaponSway();
        isAiming();
    }

    void Reload()
    {
        // Press R for reload or when it hits zero
        if (currentClipAmount < maxClip)
        {
            if (mayFire)
            {
                int amount = maxClip / 5;
                if (Input.GetButtonDown("R") || currentClipAmount <= 0)
                {
                    needtoReload.SetActive(false);
                    reloadSlider.SetActive(true);
                    timeSwitch = true;
                }
                else if (currentClipAmount <= amount && mayFire)
                {
                    if (!timeSwitch)
                    {
                        needtoReload.SetActive(true);
                    }
                }
                else
                {
                    needtoReload.SetActive(false);
                }
            }
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
    float CalculateTime()
    {
        return currentTime / reloadTime;
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;
    }

    void AmmoCheck()
    {
        // Ammo Text
        ammoText.text = (weaponType + currentClipAmount + "/" + currentAmmo);

        // If you have NO Ammo at all
        if (currentAmmo <= 0 && currentClipAmount <= 0)
        {
            mayFire = false;
            outofAmmo.SetActive(true);
            needtoReload.SetActive(false);
            reloadSlider.SetActive(false);
        }
        else
        {
            outofAmmo.SetActive(false);
        }
        // Check if timeSwitch == true
        if (currentClipAmount < maxClip && currentAmmo <= 0)
        {
            timeSwitch = false;
        }
        if (currentTime <= 0)
        {
            reloadSlider.SetActive(false);
            mayFire = true;
            timeSwitch = false;
            currentTime = reloadTime;
            if (currentClipAmount >= 0 && currentAmmo < maxClip)
            {
                int ammoOver = currentAmmo;
                currentClipAmount += currentAmmo;
                currentAmmo -= ammoOver;
            }
            else
            {
                int needAmmo = maxClip - currentClipAmount;
                currentClipAmount += needAmmo;
                currentAmmo -= needAmmo;
            }
            if (currentClipAmount > maxClip)
            {
                currentClipAmount = maxClip;
            }
            if (currentClipAmount == 0)
            {
                currentClipAmount = 0;
                mayFire = false;
            }
            if (currentAmmo == 0)
            {
                currentAmmo = 0;
            }
            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }
        }
    }

    void MuzzelFlash()
    {
        if (muzzelFlachEnabled)
        {
            muzzelFlashTimer -= Time.deltaTime;
        }
        if (muzzelFlashTimer <= 0)
        {
            muzzelFlashTimer = muzzelFlashTimerStart;
            muzzelFlachEnabled = false;
            muzzelFlash.SetActive(false);
        }
    }

    void HitMarker()
    {
        if (hitMarkerEnabled)
        {
            hitMarkerTimer -= Time.deltaTime;
        }
        if (hitMarkerTimer <= 0)
        {
            hitMarkerTimer = hitMarkerTimerStart;
            hitMarkerEnabled = false;
            hitMarker.SetActive(false);
        }
    }

    void FireRate()
    {
        if (fire)
        {
            fireTime -= Time.deltaTime;
        }
        if (fireTime <= 0)
        {
            fire = false;
            fireTime = fireAgain;
        }
    }

    void Weapon()
    {
        // Weapon Functions
        if (Input.GetButton("Fire1"))
        {
            if (mayFire)
            {
                if (!fire)
                {
                    if (currentClipAmount > 0)
                    {
                        if (timeSwitch)
                        {
                            timeSwitch = false;
                            reloadSlider.SetActive(false);
                            currentTime = reloadTime;
                        }
                        currentClipAmount -= fireAmmo;
                        muzzelFlash.SetActive(true);
                        muzzelFlachEnabled = true;

                        // Recoil Movement
                        Vector3 weaponLocalPosition = weaponObject.transform.localPosition;
                        weaponLocalPosition.z = weaponLocalPosition.z -= recoilAmount;
                        weaponObject.transform.localPosition = weaponLocalPosition;
                        audioSourse.PlayOneShot(shoot,volumeScale);


                        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, raycastLength))
                        {
                            if (hit.transform.tag != null)
                            {
                                if (hit.transform.tag == "Enemy")
                                {
                                    volumeScale = 0.5f;
                                    hitMarker.SetActive(true);
                                    hitMarkerEnabled = true;
                                    audioSourse.PlayOneShot(hitMarkerSound);
                                    GameObject g = Instantiate(bloodHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                                    g.transform.parent = hit.transform;
                                    hit.collider.gameObject.GetComponent<EnemyHealth>().EnemyHealthCheck(damage);
                                }
                                if (hit.transform.tag == "House")
                                {
                                    GameObject h = Instantiate(houseHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                                    h.transform.parent = hit.transform;
                                }
                            }
                            if (hit.transform.tag == "Untagged")
                            {
                                GameObject n = Instantiate(normalHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                                n.transform.parent = hit.transform;
                            }
                           
                            if (hit.rigidbody != null)
                            {
                                hit.rigidbody.AddForce(-hit.normal * inpactForce);
                            }
                        }
                        else
                        {
                            volumeScale = 1f;
                        }
                        recoilT.Rotate(recoil);
                    }
                }
            }
            fire = true;
            Debug.DrawRay(cameraPosition.position, cameraPosition.forward * 10, Color.red);
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
            crossHair1.SetActive(false);
            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, aimingPos, Time.deltaTime * aimSpeed);
            cameraFOV.fieldOfView = Mathf.Lerp(cameraFOV.fieldOfView, aimFOV, Time.deltaTime * fovSpeed);
        }
        else if (!aiming)
        {
            crossHair1.SetActive(true);
            weaponObject.transform.localPosition = Vector3.Lerp(weaponObject.transform.localPosition, normalPos, Time.deltaTime * aimSpeed);
            cameraFOV.fieldOfView = Mathf.Lerp(cameraFOV.fieldOfView, defaultFOV, Time.deltaTime * fovSpeed);
        }
    }

}

