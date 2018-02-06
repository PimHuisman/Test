using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    NavMeshAgent agent;
    // Health
    public float health;
    public float currentHealth;
    public bool dead;
    [SerializeField] private int points;
    [SerializeField] private int killes;
    private Transform score;
    private Transform deadSore;
    private bool flagCheck;
    private float deadpoint = 1;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreManager").transform;
        deadSore = GameObject.FindGameObjectWithTag("WaveSystem").transform;
        flagCheck = true;
        currentHealth = health;
    }
    private void Update()
    {
        isCheck();;
    }

    public void isCheck()
    {
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;
            Score();
            Destroy(gameObject, 10f);
        }
    }

    public void EnemyHealthCheck(float damage)
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentHealth -= damage;
    }
    public void UpEnemyHealth (float upHealth)
    {
        health = health * upHealth;
    }

    void Score()
    {
        if (flagCheck)
        {
            flagCheck = false;
            score.GetComponent<ScoreManager>().Killes(killes);
            score.GetComponent<ScoreManager>().Points(points,0);
            deadSore.GetComponent<WaveSystem>().EnemyCheck(deadpoint);
        }
    }


}
