using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenCheck : MonoBehaviour
{
    #region references
    private UnityEngine.Camera _maincam;

    public static List<GameObject> _currentenemy = new List<GameObject>();
    private static List<Transform> _enemytransform = new List<Transform>();
    #endregion
    public static void RegisterEnemy()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allScripts.Length; i++)
        {

            if (allScripts[i] is EnemiesControler)
            {

                if (!_currentenemy.Contains(allScripts[i].gameObject))
                {
                    _currentenemy.Add(allScripts[i].gameObject);
                }
            }
        }
    }
    void Awake()
    {
        _maincam = Camera.main; 
        RegisterEnemy();
        
    }
    private void Start()
    {
        for (int i = 0; i < _currentenemy.Count; i++) { _enemytransform.Add(_currentenemy[i].transform); }
        for (int i = 0; i < _currentenemy.Count; i++)
        {
            if (_enemytransform[i] != null) EnemyCheck(_currentenemy[i], _enemytransform[i]);
        }
    }
    private void Update()
    {
        for (int i = 0; i < _enemytransform.Count; i++)
        {
            if (_enemytransform[i] != null) EnemyCheck(_currentenemy[i], _enemytransform[i]);

        }
    }
    private void EnemyCheck(GameObject _currentEnemy, Transform _enemytransform)
    {
        Vector3 viewPos = _maincam.WorldToViewportPoint(_enemytransform.position);
        bool inScreen = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;
        _currentEnemy.SetActive(inScreen);
        if (inScreen) { LevelManager.RegisterEnemy(_currentEnemy); }
    }
   

}
