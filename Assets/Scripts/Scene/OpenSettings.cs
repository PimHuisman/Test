using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject healthPanel;
    private bool paused;
    private bool isPressed;

    void Start()
    {
        paused = false;
        isPressed = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
            isPressed = !isPressed;

            if (isPressed)
            {
                settingsPanel.gameObject.SetActive(true);
                healthPanel.gameObject.SetActive(false);
            }
            else if (!isPressed)
            {
                settingsPanel.gameObject.SetActive(false);
                healthPanel.gameObject.SetActive(true);
            }

            if (paused)
            {
                Time.timeScale = 0;
            }
            else if (!paused)
            {
                Time.timeScale = 1;
            }
        }
    }
}


