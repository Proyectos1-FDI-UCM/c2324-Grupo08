using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region References
    /// <summary>
    /// Referencia a la flecha indicativa del paso de sala.
    /// </summary>
    [SerializeField]
    GoArrowController Arrow;

    /// <summary>
    /// Referencia al GameManager.
    /// </summary>
    static GameManager gameManager;

    /// <summary>
    /// Referencia a la mejora de las chapas.
    /// </summary>
    [SerializeField]
    GameObject chapas;

    /// <summary>
    /// Referencia a la mejora del kebab.
    /// </summary>
    [SerializeField]
    GameObject kebab;

    /// <summary>
    /// Referencia a la mejora de la energetica.
    /// </summary>
    [SerializeField]
    GameObject energetica;

    /// <summary>
    /// Referencia a la clase que gestiona el trigger 
    /// de paso normal entre salas.
    /// </summary>
    CamTrigger _trigger;

    CamTriggerSecreta _triggerSecreta;

    /// <summary>
    /// Instancia del levelManager.
    /// </summary>
    private static LevelManager instance;
    /// <summary>
    /// Accesor a la instancia del LevelManager.
    /// </summary>
    public static LevelManager levelManager { get { return instance; } private set { instance = value; } }

    #endregion

    #region Parameters
    /// <summary>
    /// Lista de TODOS los enemigos en el mapa.
    /// </summary>
    static List<EnemiesControler> m_AllEnemies;

    /// <summary>
    /// Lista con TODAS las mejoras para Paco.
    /// </summary>
    static List<GameObject> _mejoras;
    #endregion

    #region Properties
    /// <summary>
    /// Tiempo de espera hasta la Activacíon del Arrow una vez 
    /// iniciadas las correspondientes corutinas.
    /// </summary>
    public float _timer;
    #endregion

    #region Methods
    /// <summary>
    /// Asigna un valor a Arrow
    /// </summary>
    /// <param name="arrow">Flecha que usaremos para indicar que ya se puede avanzar</param>
    public void RegisterArroy(GoArrowController arrow)
    {
        Arrow = arrow;
    }

    /// <summary>
    /// Registra en m_AllEnemies al objeto si es un enemigo que no estuviera registrado.
    /// Con esto se lleva la cuenta de cuantos enemigos hay en sala.
    /// Una vez detecte que hay enemigos bloquea el paso de salas y desactiva Arrow.
    /// </summary>
    /// <param name="enemy">Enemigo a registrar dentro de m_AllEnemies</param>
    public void RegisterEnemy(GameObject enemy)
    {
        EnemiesControler enemiesControler = enemy.GetComponent<EnemiesControler>();
        if (enemiesControler != null)
        {
            if (!m_AllEnemies.Contains(enemiesControler))
            {
                m_AllEnemies.Add(enemiesControler);
            }
        }
        if (m_AllEnemies.Count > 0)
        {
            _trigger.TransitionAvaible(false);
            _triggerSecreta.TransitionAvaible(false);
            Arrow.SetActive(false);
        }
    }

    /// <summary>
    /// Elimina al enemigo que estaba registrado en la sala de 
    /// la lista m_AllEnemies para mantener la cuenta de los enemigos.
    /// En caso de dectectar que con la eliminación de un enemigo no quedan ninguno
    /// activara el Arrow y la transición de salas. Ademas si nos encontramos en la mitad del
    /// recorrido de las salas faciles o dificiles soltara una mejora.
    /// </summary>
    /// <param name="enemy">Enemigo que ha sido derrotado</param>
    public void EnemyDefeated(EnemiesControler enemy)
    {
        if (m_AllEnemies.Contains(enemy))
        {
            m_AllEnemies.Remove(enemy);
        }

        if (m_AllEnemies.Count <= 0)
        {
            int room = GameManager.ActiveRoom;
            _trigger.TransitionAvaible(true);
            _triggerSecreta.TransitionAvaible(true);

            //Se instancia una mejora en la sala que coincida con la intermedia dentro de las salas faciles o las dificiles
            if ((room == 1 + gameManager.EasyRooms.Length / 2 || room == gameManager.Map.Count - (1 + gameManager.HardRooms.Length / 2)) && room > 0)
            {
                DropUpgrade(gameManager.Map[room]);
            }
            if (Arrow != null) 
            { 
                Arrow.SetActive(true); 
            }
        }
    }

    /// <summary>
    /// Comprueba que mejoras tiene el jugador.
    /// </summary>
    void CheckUpgrades()
    {
        if (FrankMovement.Player != null) // Verifica si el jugador aún existe
        {
            //Comprueba si el Jugador ya alguna mejora
            bool Chapas = FrankMovement.Player.GetComponentInChildren<PlayerAttack>()._mejorado;
            bool Kebab = Mathf.Approximately(FrankMovement.Player.GetComponent<HealthComponent>().MaxHealth, 8f);
            bool Energetica = FrankMovement.Player.GetComponent<DashCompnent>().dashMejorado;

            //Si la tenia la elimina de la lista de posibles mejoras
            if (Chapas) { _mejoras.Remove(chapas); }
            if (Kebab) { _mejoras.Remove(kebab); }
            if (Energetica) { _mejoras.Remove(energetica); }
        }
    }

    /// <summary>
    /// Instancia una mejora aleatoria que el jugador no tenga en el centro de la Sala (room) 
    /// </summary>
    /// <param name="room"></param> Sala en la que se instanciará la mejora
    public void DropUpgrade(GameObject room)
    {
        CheckUpgrades();
        Instantiate(_mejoras[Random.Range(0, _mejoras.Count)], room.transform.position, Quaternion.identity);
    }

    /// <summary>
    ///Comprueba si estamos en la última sala y si lo estamos al nos envia a los créditos
    /// </summary>
    public void CheckEndGame()
    {
        if (GameManager.ActiveRoom >= gameManager.Map.Count)
        {
            MenuManager.Credits();
        }
    }
    #endregion

    #region Coroutines

    /// <summary>
    /// Gestiona la salida de la secreta.
    /// </summary>
    /// <returns>Espera _timer segundos para permitir el acceso a la siguiente sala</returns>
    public IEnumerator Go()
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(_timer);
        Arrow.SetActive(true);
        _trigger.TransitionAvaible(true);
        _triggerSecreta.TransitionAvaible(true);
    }

    /// <summary>
    /// Gestiona la entrada a la sala secreta.
    /// </summary>
    /// <returns>Espera _timer segundos a habilitar el paso a la sala intermedia</returns>
    public IEnumerator Go2()
    {
        Arrow.SetActive(false);
        _trigger.TransitionAvaible(false);
        yield return new WaitForSeconds(_timer);
        _triggerSecreta.TransitionAvaible(true);
    }
    #endregion

    private void Awake()
    {
        //Sigleton del LevelManager.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Inicialización de variables
        m_AllEnemies = new List<EnemiesControler>();

        //Insertamos las mejoras en la lista
        _mejoras = new List<GameObject>
        {
            chapas,
            kebab,
            energetica
        };

        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        Arrow = GoArrowController.GoArrowReference;
        Arrow.SetActive(false);
        _triggerSecreta = CamTriggerSecreta.Instance;
        _trigger = CamTrigger.Instance;
    }

}
