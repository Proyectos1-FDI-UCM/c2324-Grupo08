using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region references
    [SerializeField]
    static GameObject Arrow;
    static GameManager gameManager;
    private static LevelManager instance;
    public static LevelManager levelManager { get { return instance; } set { instance = value; } }
    #endregion

    #region parameters
    static List<EnemiesControler> m_AllEnemies;
    static List<GameObject> _mejoras;

    [SerializeField]
    GameObject chapas;
    [SerializeField]
    GameObject kebab;
    [SerializeField]
    GameObject energetica;
    #endregion

    #region methods
    public static void RegisterArroy(GameObject arrow)
    {
        if(Arrow == null) Arrow = arrow;
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
            CamTriggerSecreta.TransitionAvaible(false);
            CamTrigger.TransitionAvaible(false);
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
            CamTriggerSecreta.TransitionAvaible(true);
            CamTrigger.TransitionAvaible(true);
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
        _mejoras = new List<GameObject>();
        _mejoras.Add(chapas);
        _mejoras.Add(kebab);
        _mejoras.Add(energetica);
        gameManager = GetComponent<GameManager>();
    }

    void Start()
    {
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        Arrow.SetActive(false);
        yield return new WaitForSeconds(1f);
        Arrow.SetActive(true);
        CamTrigger.TransitionAvaible(true);
        CamTriggerSecreta.TransitionAvaible(true);
    }
    void CheckUpgrades()
    {
        bool Chapas = FrankMovement.Player.GetComponentInChildren<PlayerAttack>()._mejorado;
        bool Kebab = Mathf.Approximately(FrankMovement.Player.GetComponent<HealthComponent>().MaxHealth, 8f);
        bool Energetica = FrankMovement.Player.GetComponent<DashCompnent>().dashMejorado;
        if (Chapas) { _mejoras.Remove(chapas); }
        if (Kebab) { _mejoras.Remove(kebab); }
        if (Energetica) { _mejoras.Remove(energetica); }
    }
    void DropUpgrade()
    {
        int room = GameManager.ActiveRoom;
        CheckUpgrades();
        if (room % 2 == 0)
        {
            Instantiate(_mejoras[Random.Range(0, _mejoras.Count)], gameManager.Map[room - 1].transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_AllEnemies.Count);
        Debug.Log("Active Room " + GameManager.ActiveRoom);
    }

}
