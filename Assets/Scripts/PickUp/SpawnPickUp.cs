using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUp : MonoBehaviour
{
    public float randomTime;
    public bool randomOne;
    [SerializeField] private float currentTime;
    [SerializeField] private float leastTime;
    [SerializeField] private float mostTime;
    [SerializeField] private GameObject[] pickUp;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform randomSpawnPoints;
    [SerializeField] private string[] typeOf;
    [SerializeField] private string[] infoOf;
    [SerializeField] private string info;
    [SerializeField] private string type;

    void Start()
    {
        RandomTime();
    }

    void Update ()
    {
        Timer();
    }

    void RandomTime()
    {
        randomTime = Random.Range(leastTime, mostTime);
        currentTime = randomTime;
    }

    void Timer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            currentTime = Random.Range(leastTime, mostTime);
            int newTypeOf = Random.Range(0, typeOf.Length);
            //if ()
            {
                int newInfoOf = Random.Range(0, infoOf.Length);
                info = infoOf[newInfoOf];
            }
            type = typeOf[newTypeOf];
            int randomPickup = Random.Range(0,pickUp.Length);
            randomSpawnPoints = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(pickUp[randomPickup], randomSpawnPoints.transform);

            if(randomOne)
            {
                RandomTime();
                randomOne = false;
            }
            currentTime = randomTime;
        }
    }
}
