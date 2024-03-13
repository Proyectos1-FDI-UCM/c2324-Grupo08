using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region references
    private Drops _drops;
    #endregion
    #region events
    public static event Action OnPlayerDamaged;
    #endregion
    #region parameters
    [SerializeField] private LifeBarComponenet lifebar;
    private float _health;
    public float Health { get { return _health; } set { _health = value; } }
    [SerializeField]
    private float _maxHealth = 6;
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
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

     void Start()
    {
        _drops = GetComponent<Drops>();
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        OnPlayerDamaged?.Invoke();
        if(this.gameObject.GetComponent<EnemiesControler>() != null ) 
        {
            _drops.Drop();
        }
        
    }
}
