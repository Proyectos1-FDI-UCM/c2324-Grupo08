using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region properties
    static List<EnemiesControler> m_AllEnemies = new List<EnemiesControler>();
    
    #endregion

    #region methods
    public static void RegisterEnemy() 
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allScripts.Length; i++)
        {
            
            if (allScripts[i] is EnemiesControler)
            {

                if (!m_AllEnemies.Contains(allScripts[i] as EnemiesControler)) 
                {
                    m_AllEnemies.Add(allScripts[i] as EnemiesControler);
                    OnScreenCheck._currentenemy.Add(allScripts[i].gameObject);
                } 
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
        RegisterEnemy();
        Debug.Log(m_AllEnemies.Count);
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
