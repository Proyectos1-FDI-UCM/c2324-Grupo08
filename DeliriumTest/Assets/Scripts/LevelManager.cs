using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region references
    #endregion
    #region properties
    private int enemies;
    private  bool _transTrue = true;
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
        if (enemies == 0) {

            CamTrigger.TransitionAvaible(_transTrue);
        }
    }
}
