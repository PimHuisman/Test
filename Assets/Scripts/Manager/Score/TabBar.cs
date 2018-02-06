using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabBar : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject wManager;
    private PlayerMovement move;
    private CameraMovement cam;
    private bool paused;
    private bool isPressed;
    CursorLockMode wantedMode;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = player.GetComponentInChildren<CameraMovement>();
        move = player.GetComponent<PlayerMovement>();
        print(player);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        isPressed = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("P"))
        {
            paused = !paused;
            isPressed = !isPressed;

            if (isPressed)
            {
                settingsPanel.gameObject.SetActive(true);
            }
            else if (!isPressed)
            {
                Cursor.visible = false;
                if (Cursor.lockState != CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                settingsPanel.gameObject.SetActive(false);
            }

            if (paused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                cam.enabled = false;
                move.enabled = false;
                wManager.SetActive(false);
                if (Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else if (!paused)
            {
                Time.timeScale = 1;
                cam.enabled = true;
                move.enabled = true;
                wManager.SetActive(true);
            }
        }
    }
}
