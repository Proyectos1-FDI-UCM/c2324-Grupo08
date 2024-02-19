using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region events
    public static event Action OnPlayerDamaged;
    #endregion
    #region parameters
    public float health,maxHealth;
    #endregion

    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        OnPlayerDamaged?.Invoke();
    }
}
