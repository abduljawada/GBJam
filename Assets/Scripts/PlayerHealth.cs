using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] GameObject loseScreen;
    int currentHealth;
    private void Awake()
    {
        currentHealth = maxHealth;

        lifeText.text = "Life" + Environment.NewLine + currentHealth.ToString();
    }
    public void TakeDamage()
    {
        currentHealth--;

        lifeText.text = "Life" + Environment.NewLine + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;

        loseScreen.SetActive(true);
    }
}
