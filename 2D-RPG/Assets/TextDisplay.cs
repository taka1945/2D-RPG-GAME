﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public Slider bossHealthBar;
    public Text bossName;

    private HealthComponent playerHealth;
    private MovementComponent playerStamina;
    private bool hasBossMusicStarted = false;

    [HideInInspector]
    public GameObject boss;
    [HideInInspector]
    public HealthComponent bossHealth; // Set in Boss Room boss selection

	// Use this for initialization
	void Start ()
    {
        playerHealth = GameManagerSingleton.instance.player.GetComponent<HealthComponent>();
        playerStamina = GameManagerSingleton.instance.player.GetComponent<MovementComponent>();

        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.Stamina, playerStamina.maxStamina);

        bossHealthBar.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetHealthBarsAndMusic();
	}

    void SetHealthBarsAndMusic()
    {
        if (boss != null)
        {
            if (Vector2.Distance(boss.transform.position, GameManagerSingleton.instance.player.transform.position) < 12f)
            {
                bossHealthBar.gameObject.SetActive(true);
                if (!hasBossMusicStarted)
                {
                    SoundManager.instance.musicSource.clip = boss.GetComponent<BossMusic>().bossMusic;
                    StartCoroutine(SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 5f));
                    hasBossMusicStarted = true;

                }
            }
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
            StartCoroutine(SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 5f));
        }

        healthBar.value = CalculateFillPercentage(playerHealth.health, playerHealth.maxHealth);
        staminaBar.value = CalculateFillPercentage(playerStamina.Stamina, playerStamina.maxStamina);

        if(bossHealthBar.IsActive())
            bossHealthBar.value = CalculateFillPercentage(bossHealth.health, bossHealth.maxHealth);
    }

    float CalculateFillPercentage(float current, float max)
    {
        return current / max;
    }
}
