using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region properties
    private int enemies;
    #endregion
    #region methods
    public void RegisterEnemy() { enemies++;}
    public void EnemyDefeated() { enemies--; }

    public void EnterRoomEnable(bool state) 
    {
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
