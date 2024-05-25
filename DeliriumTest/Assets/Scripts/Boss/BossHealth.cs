using UnityEngine;
using UnityEngine.UI;

//AVISO: Esta clase es para poder reconvertir los parámetros de vida
//de este objeto y que los recoja el slider de la barra de vida
//del boss.

public class BossHealth : MonoBehaviour
{
    #region References

    /// <summary>
    /// Conponente de vida del boss.
    /// </summary>
    private HealthComponent _bossHealth;

    /// <summary>
    /// Vida actual del boss.
    /// </summary>
    private float bossHealth;

    /// <summary>
    /// Vida maxima del boss.
    /// </summary>
    private float bossMaxHealth;

    /// <summary>
    /// Slider que muestra la vida del boss
    /// </summary>
    public Slider _healthBar;
    #endregion

    private void OnDestroy()
    {
        //Desactiva la barra de vida e indica que el boss a sido derrotado
        LevelManager.levelManager.EnemyDefeated(GetComponent<BossController>());
        Canvas Health = _healthBar.GetComponentInParent<Canvas>();
        if (Health != null)
        {
            Health.enabled = false;
        }

    }
    private void Awake()
    {
        //Desactiva la barra de vida para evitar que aparezca en pantalla al inicio del juego
        _healthBar.GetComponentInParent<Canvas>().enabled = false;
    }
    private void Start()
    {
        //Recoge los datos de vida del Boss y los manda al Slider
        _bossHealth = GetComponent<HealthComponent>();
        RegisterLife();
    }
    private void Update()
    {
        //Si el boss no esta en pantalla lo desactivamos
        GetComponent<BossController>().enabled = gameObject.activeSelf;

        //Actualiza los datos del boss y activa la barra de vida si el boss ya se encuentra en pantalla
        bossHealth = _bossHealth.Health;
        _healthBar.value = bossHealth;
        _healthBar.GetComponentInParent<Canvas>().enabled = GetComponent<BossController>().enabled;

        //Si el boss muere lo destruye
        if (_bossHealth.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Recoge los valores de vida y los guarda en las variables correspondientes
    /// </summary>
    private void RegisterLife()
    {
        bossMaxHealth = _bossHealth.MaxHealth;
        _healthBar.maxValue = bossMaxHealth;
    }
}
