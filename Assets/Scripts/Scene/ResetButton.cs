using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private bool switchTime;

    void Start()
    {
        switchTime = !switchTime;
    }
    public void Freeze()
    {
        Time.timeScale = 1;
    }
}

