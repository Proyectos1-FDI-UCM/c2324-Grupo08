using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region references
    private Drops _drops;
    private SpriteRenderer _spriteRenderer;
    #endregion
    #region events
    public static event Action OnPlayerDamaged;
    #endregion
    #region parameters
    [SerializeField] private LifeBarComponenet lifebar;
    [SerializeField] private float _health;
    public float Health { get { return _health; } set { _health = value; } }
    [SerializeField]
    private float _maxHealth = 6;
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    [SerializeField] private float _damageTime;
    public float InvencibilityTime
    {
        get { return _damageTime; }
    }

    [SerializeField] private float _twitchEffect;
    [SerializeField] private float _hitInterval;
   

    private Color _initialColor;
    [SerializeField] private Color _hitColor;
    [SerializeField] private Color _hitreturnColor;

    [SerializeField] private LayerMask Invencibility;
    private LayerMask _originalLayer;
    #endregion
    #region methods
    public void Healing(float healing)
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
        _originalLayer = gameObject.layer;
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        OnPlayerDamaged?.Invoke();
        if (this.gameObject.GetComponent<EnemiesControler>() != null)
        {
            _drops.Drop();
        }
        if (this.gameObject.GetComponent<FrankMovement>() != null)
        {
            StartCoroutine(RepeatHitEffect());
        }
    }

    private IEnumerator RepeatHitEffect()
    {
        gameObject.layer = 10;
        for (int i = 0; i < _damageTime; i++)
        {
            yield return StartCoroutine(FrankHit());
            yield return new WaitForSeconds(_hitInterval);
        }
        _spriteRenderer.color = _initialColor;
        gameObject.layer = _originalLayer;
    }
    private IEnumerator FrankHit()
    {
        _spriteRenderer.color = _hitColor;
        yield return new WaitForSeconds(_twitchEffect);
        _spriteRenderer.color = _hitreturnColor;
    }
}
