using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region references
    [SerializeField]
    static GameObject Arrow;
    GameManager gameManager;
    #endregion

    #region parameters
    static List<EnemiesControler> m_AllEnemies;
    public List<GameObject> _mejoras;
    #endregion

    #region methods
    public static void RegisterArroy(GameObject arrow)
    {
        if(Arrow == null) Arrow = arrow;
    }
    public static void RegisterEnemy(GameObject enemy)
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
    public static void EnemyDefeated(EnemiesControler enemy)
    {
        if (m_AllEnemies.Contains(enemy))
        {
            m_AllEnemies.Remove(enemy);
        }

        if (m_AllEnemies.Count == 0)
        {
            CamTriggerSecreta.TransitionAvaible(true);
            CamTrigger.TransitionAvaible(true);
            Arrow.SetActive(true);
        }
    }

    #endregion
    private void Awake()
    {
        m_AllEnemies = new List<EnemiesControler>();
        //_mejoras = new List<GameObject>();
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
    }
    void CheckUpgrades()
    {
        bool chapas = FrankMovement.Player.GetComponentInChildren<PlayerAttack>()._mejorado;
        bool kebab = Mathf.Approximately(FrankMovement.Player.GetComponent<HealthComponent>().MaxHealth, 8f);
        bool energetica = FrankMovement.Player.GetComponent<DashCompnent>().dashMejorado;
        if (chapas) { _mejoras.Remove(_mejoras[0]); }
        if (kebab) { _mejoras.Remove(_mejoras[1]); }
        if (energetica) { _mejoras.Remove(_mejoras[2]); }
    }
    void DropUpgrade() 
    {
        int room = GameManager.ActiveRoom;
        CheckUpgrades();
        if (room % 2 == 0)
        {
            Instantiate(_mejoras[Random.Range(0,_mejoras.Count)],gameManager.Map[room].transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_AllEnemies.Count);
        Debug.Log("Active Room " + GameManager.ActiveRoom);
        if (m_AllEnemies.Count == 0 && GameManager.ActiveRoom > 0)
        {
            DropUpgrade();  
        }
    }

}
