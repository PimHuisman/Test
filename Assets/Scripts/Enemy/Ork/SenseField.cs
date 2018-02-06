using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseField : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool dDead = enemy.GetComponent<EnemyHealth>().dead;
            if (!dDead)
            {
                enemy.GetComponent<EnemyAI>().chasing = true;
                enemy.GetComponent<EnemyAI>().senseField = true;
                enemy.GetComponent<EnemyAI>().isChasing();
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.GetComponent<EnemyAI>().senseField = false;
        }
    }
}
