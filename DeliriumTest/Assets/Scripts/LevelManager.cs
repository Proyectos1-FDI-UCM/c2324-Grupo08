using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region references
    [SerializeField]
    GoArrowController Arrow;
    static GameManager gameManager;
    private static LevelManager instance;
    public static LevelManager levelManager { get { return instance; } set { instance = value; } }
    #endregion

    #region parameters
    static List<EnemiesControler> m_AllEnemies;
    static List<GameObject> _mejoras;
    #endregion
    
    #region properties
    [SerializeField]
    GameObject chapas;
    [SerializeField]
    GameObject kebab;
    [SerializeField]
    GameObject energetica;
    public float _timer;
    #endregion

    #region methods
    public void RegisterArroy(GoArrowController arrow)
    {
        Arrow = arrow;
    }
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
            CamTriggerSecreta.Instance.GetComponent<CamTriggerSecreta>().TransitionAvaible(false);
            CamTrigger.Instance.GetComponent<CamTrigger>().TransitionAvaible(false);
            Arrow.SetActive(false);
        }
    }
    public void EnemyDefeated(EnemiesControler enemy)
    {
        if (m_AllEnemies.Contains(enemy))
        {
            m_AllEnemies.Remove(enemy);
        }

        if (m_AllEnemies.Count == 0)
        {
            CamTriggerSecreta.Instance.GetComponent<CamTriggerSecreta>().TransitionAvaible(true);
            CamTrigger.Instance.GetComponent<CamTrigger>().TransitionAvaible(true);
            DropUpgrade();
            Arrow.SetActive(true);
        }
    }

    #endregion
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        m_AllEnemies = new List<EnemiesControler>();
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
        Arrow.SetActive(false);
    }
    public IEnumerator Go()
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(_timer);
        Arrow.SetActive(true);
        CamTrigger.Instance.GetComponent<CamTrigger>().TransitionAvaible(true);
        CamTriggerSecreta.Instance.GetComponent<CamTriggerSecreta>().TransitionAvaible(true);
    }
    public IEnumerator Go2()
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(_timer);
        CamTriggerSecreta.Instance.GetComponent<CamTriggerSecreta>().TransitionAvaible(true);
    }
    void CheckUpgrades()
    {
        if (FrankMovement.Player != null) // Verifica si el jugador a�n existe
        {
            bool Chapas = FrankMovement.Player.GetComponentInChildren<PlayerAttack>()._mejorado;
            bool Kebab = Mathf.Approximately(FrankMovement.Player.GetComponent<HealthComponent>().MaxHealth, 8f);
            bool Energetica = FrankMovement.Player.GetComponent<DashCompnent>().dashMejorado;
            if (Chapas) { _mejoras.Remove(chapas); }
            if (Kebab) { _mejoras.Remove(kebab); }
            if (Energetica) { _mejoras.Remove(energetica); }
        }
    }
    void DropUpgrade()
    {
        int room = GameManager.ActiveRoom;
        CheckUpgrades();
        if (room % 2 == 0 && room > 0)
        {
            Instantiate(_mejoras[Random.Range(0, _mejoras.Count)], gameManager.Map[room - 1].transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_AllEnemies.Count);
        //Debug.Log("Active Room " + GameManager.ActiveRoom);
    }

}
