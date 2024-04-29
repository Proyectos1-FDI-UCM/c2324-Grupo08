using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{    // Clase reutilizada tanto como el jugador como los distntos enemigos del juego
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


    Color _initialColor;
    Color _hitColor;
    Color _hitreturnColor;
    private LayerMask _originalLayer;
    #endregion
    #region methods
    // Método usado par acurar al jugadro con la "Bolsa de patatas"
    public void Healing(float healing)
    {
        _health += healing;
        lifebar.DrawHearts();
    }
    #endregion
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health = _maxHealth;
        _damageTime = 10;
        _twitchEffect = 0.1f;
        _hitInterval = 0.1f;
        _hitColor = new Color(56,56,56,0);
        _hitreturnColor = _spriteRenderer.color * new Color(1, 1, 1, 233);
    }

    void Start()
    {
        _drops = GetComponent<Drops>();
        _initialColor = _spriteRenderer.color;
        _originalLayer = gameObject.layer;
    }
    // Método que llama al evento de recibir daño y reduce la vida del jugador a la par que enviarle la información al LifeBarComponenet
    // Cuando un enemigo muera soltará un drop
    public void TakeDamage(float amount)
    {
        _health -= amount;
        StartCoroutine(RepeatHitEffect());
        OnPlayerDamaged?.Invoke();
        if (gameObject.GetComponent<EnemiesControler>() != null)
        {
            _drops.Drop();
        }  
    }
    private void OnDestroy()
    {
        _health = 0;
    }

    private IEnumerator RepeatHitEffect()
    {
        if(this.gameObject.GetComponent<RatMovement>()  != null)
        {
            AudioManager.Instance.RatSound();
        }
        else if(this.gameObject.GetComponent<TrashMovement>() != null)
        {
            AudioManager.Instance.TrashSound();
        }
        if(this.gameObject.GetComponent<FrankMovement>() != null || gameObject.GetComponent<BossController>() != null)
        {
            gameObject.layer = 10;
        }
        for (int i = 0; i < _damageTime; i++)
        {
            yield return StartCoroutine(FrankHit());
            yield return new WaitForSeconds(_hitInterval);
        }
        _spriteRenderer.color = _initialColor;
        gameObject.layer = _originalLayer;
    }
    // Método que permite hacer vulnerable a todos los objetos con este script por unos segundos
    private IEnumerator FrankHit()
    {
        _spriteRenderer.color = _hitColor;
        yield return new WaitForSeconds(_twitchEffect);
        _spriteRenderer.color = _hitreturnColor;
    }
}
