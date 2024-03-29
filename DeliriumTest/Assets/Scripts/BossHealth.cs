using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private HealthComponent _bossHealth;
    private float bossHealth;
    private float bossMaxHealth;
    public Slider _healthBar;
    private void Start()
    {              
        _bossHealth = GetComponent<HealthComponent>();
        RegisterLife();
    }
    private void Update()
    {
        bossHealth = _bossHealth.Health;
        _healthBar.value = bossHealth;
    }
    private void RegisterLife()
    {
        bossMaxHealth = _bossHealth.MaxHealth;       
        _healthBar.maxValue = bossMaxHealth;
    }
}
