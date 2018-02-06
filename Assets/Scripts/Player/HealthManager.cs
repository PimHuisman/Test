using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject diePanel;
    public Slider healthSlider;
    public bool paused;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = CalculateHealth();
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
            }
            currentHealth = 0;
            diePanel.SetActive(true);
            Cursor.visible = true;
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void UpHealth(float health)
    {
        currentHealth += health;
        healthSlider.value = CalculateHealth();
    }
    public void Health(float damage)
    {
        //Check for Health 
        currentHealth -= damage;
        healthSlider.value = CalculateHealth();
    }
    public float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
    // when you have died
    public void ResetLevel()
    {
        Time.timeScale = 1;
    }
}


