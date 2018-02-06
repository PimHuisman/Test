using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int currentPoints;
    [SerializeField] private GameObject scoreTab;
    [SerializeField] private Text[] manager;
    static public int currentKills;
    static private int endPoints;
    static private int waves;

    void Start()
    {
        Time.timeScale = 1;
        manager[0].text = ("Kills" + "/" + currentKills);
        manager[1].text = ("Points" + "/" + currentPoints);
        EndPoints();
    }


    void Update ()
    {
        endPoints = currentPoints;
        if (Input.GetButtonDown("Tab"))
        {
            scoreTab.SetActive(true);
        }
        if (Input.GetButtonUp("Tab"))
        {
            scoreTab.SetActive(false);
        }
        //ShortCut
        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.O) && (Input.GetKey(KeyCode.K)))
        {
            Points(100, 0);
        }
    }

    public void Killes(int kill)
    {
        currentKills += kill;
        manager[0].text = ("Killes" + "/" + currentKills);
        Transform wavesSystem = GameObject.FindGameObjectWithTag("WaveSystem").transform;
        waves = wavesSystem.GetComponent<WaveSystem>().waveAmount;
    }
    public void Points(int point, int dPoint)
    {
        if (currentPoints <= 0)
        {
            currentPoints = 0;
        }
        if (currentPoints >= dPoint)
        {
            currentPoints -= dPoint;
        }
        currentPoints += point;
        manager[1].text = ("Points" + "/" + currentPoints);
    }
    public void EndPoints()
    {
        manager[1].text = ("Points" + "/" + endPoints);
        manager[2].text = ("Waves" + "/" + waves);
    }
}
