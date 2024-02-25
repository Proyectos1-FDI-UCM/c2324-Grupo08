using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region properties
    static List<EnemiesControler> m_AllEnemies = new List<EnemiesControler>();
    private  bool _transTrue = true;
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
                } 
            }
        }
    }
    public static void EnemyDefeated(EnemiesControler enemy) { m_AllEnemies.Remove(enemy); }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        RegisterEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AllEnemies.Count == 0)
        {
            CamTrigger.TransitionAvaible(_transTrue);
        }
        else
        {
            CamTrigger.TransitionAvaible(false);
        }
    }
}
