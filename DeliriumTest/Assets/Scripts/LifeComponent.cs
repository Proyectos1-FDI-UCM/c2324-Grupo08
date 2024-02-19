using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    #region properties
    private Rigidbody2D rigid;
    #endregion

    #region parameters
    [SerializeField] private float playerHealth;
    [SerializeField] private float maxHealth;
    #endregion
    #region events
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    #endregion
    #region methods
    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        OnPlayerDamaged?.Invoke();
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            OnPlayerDeath?.Invoke();
        }
    }
    
    #endregion
    void Start()
    {
        playerHealth = maxHealth;
    }
}
