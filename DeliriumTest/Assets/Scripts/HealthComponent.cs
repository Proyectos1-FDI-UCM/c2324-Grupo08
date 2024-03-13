using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region events
    public static event Action OnPlayerDamaged;
    #endregion
    #region parameters
    [SerializeField] private LifeBarComponenet lifebar;
    private float _health;
    public float Health { get { return _health; } }
    [SerializeField]
    private float _maxHealth = 6;
    public float MaxHealth { get { return _maxHealth; } }
    #endregion
    #region methods
    public void Healing(float healing )
    {       
        _health += healing;
        lifebar.DrawHearts();
    }
    #endregion
    private void Awake()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        OnPlayerDamaged?.Invoke();
        if(Mathf.Approximately(_health,0f) || _health < 0f) Destroy(gameObject);
    }
}
