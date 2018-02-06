using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [Header("Enemy value")]
    public float currentAmountOfEnemies;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float amountOfEnemies;
    [SerializeField] private float upEnemies;
    [SerializeField] private float upHealth;
    [SerializeField] private float downTime;
    
    [Header("Type Enemy")]
    [SerializeField] private float ork;
    [SerializeField] private float dog;
    [SerializeField] private float troll;
    public List<Transform> orkSpawnPoints = new List<Transform>();
    public List<Transform> dogSpawnPoints = new List<Transform>();
    public List<Transform> trollSpawnPoints = new List<Transform>();
    
    [Header("Wave")]
    [SerializeField] private Text wave;
    public int waveAmount;
    private bool resetHealth;
    private bool flagCheck;
    [SerializeField] private Transform randomspawnOrk;
    [SerializeField] private Transform randomspawnDog;
    [SerializeField] private Transform randomspawnTroll;

    [Header("Amount Of Enemies")]
    [SerializeField] Text totalEnemies;

    [Header("SpawnRate")]
    [SerializeField] float spawnWait;
    [SerializeField] float spawnLeastWait;
    [SerializeField] float spawnMostWait;
    [SerializeField] bool stop;


    void Start ()
    {
        resetHealth = true;
        flagCheck = false;
        Create();
        currentAmountOfEnemies = amountOfEnemies;
        StartCoroutine(SpawnRate());
    }

    void Update()
    {
        if (resetHealth)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[0].GetComponent<EnemyHealth>().health = 75;
                enemies[1].GetComponent<EnemyHealth>().health = 50;
                enemies[2].GetComponent<EnemyHealth>().health = 75;
            }
            resetHealth = false;
        }
        // Test
        if (Input.GetKeyDown(KeyCode.P))
        {
            print(enemies[0].GetComponent<EnemyHealth>().health);
        }
        totalEnemies.text = ("Left" + "/" + currentAmountOfEnemies);
        wave.text = ("Wave" + "/" + waveAmount);
        if (currentAmountOfEnemies <= 0)
        {
            flagCheck = true;
            CheckEnemy();
        }
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        randomspawnOrk = orkSpawnPoints[Random.Range(0, orkSpawnPoints.Count)];
        randomspawnDog = dogSpawnPoints[Random.Range(0, dogSpawnPoints.Count)];
        randomspawnTroll = trollSpawnPoints[Random.Range(0, trollSpawnPoints.Count)];
    }
    void CheckEnemy()
    {
        if (flagCheck)
        {
            flagCheck = false;
            amountOfEnemies += upEnemies;
            currentAmountOfEnemies = amountOfEnemies;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyHealth>().UpEnemyHealth(upHealth);
            }

            Create();
            waveAmount++;
            StartCoroutine(SpawnRate());
            spawnLeastWait = spawnLeastWait / downTime;
            spawnMostWait = spawnMostWait / downTime;
        }
    }

    public void EnemyCheck(float enemy)
    {
        Mathf.RoundToInt(currentAmountOfEnemies -= enemy);
    }

    void Create()
    {

        if (waveAmount > 0)
        {
            if (currentAmountOfEnemies <= 0)
            {
                amountOfEnemies = Mathf.RoundToInt(amountOfEnemies + upEnemies);
            }
        }
    }
    IEnumerator SpawnRate()
    {

        ork = Mathf.RoundToInt(amountOfEnemies / 10 * 6);
        dog = Mathf.RoundToInt(amountOfEnemies / 10 * 3);
        troll = Mathf.RoundToInt(amountOfEnemies / 10 * 1);

        yield return new WaitForSeconds(spawnWait);

        for (int i = 0; i < ork; i++)
        {
            Instantiate(enemies[0], randomspawnOrk.transform);
            yield return new WaitForSeconds(spawnWait);
        }
        yield return new WaitForSeconds(spawnWait);

        for (int i = 0; i < dog; i++)
        {
            Instantiate(enemies[1], randomspawnDog.transform);
            yield return new WaitForSeconds(spawnWait);
        }

        yield return new WaitForSeconds(spawnWait);

        for (int i = 0; i < troll; i++)
        {
            Instantiate(enemies[2], randomspawnTroll.transform);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
