using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region properties
    static List<EnemiesControler> m_AllEnemies = new List<EnemiesControler>();
    
    #endregion

    #region methods
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
    }
    public static void EnemyDefeated(EnemiesControler enemy) { m_AllEnemies.Remove(enemy); }

    #endregion
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_AllEnemies.Count);
        if (m_AllEnemies.Count == 0)
        {
            CamTrigger.TransitionAvaible(true);
        }
        else if(m_AllEnemies.Count > 0) 
        {
            CamTrigger.TransitionAvaible(false);
        }
    }
    
}
