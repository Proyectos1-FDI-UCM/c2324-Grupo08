using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenCheck : MonoBehaviour
{
    #region references
    private UnityEngine.Camera _maincam;

    [SerializeField]
    private GameObject[] _currentenemy;
    [SerializeField]
    private Transform[] _enemytransform;
    #endregion

    void Awake()
    {
        _maincam = Camera.main;
        for (int i = 0; i < _currentenemy.Length; i++)
        {
            if (_enemytransform[i] != null) EnemyCheck(_currentenemy[i], _enemytransform[i]);

        }
    }
    private void Update()
    {

        for (int i = 0; i < _currentenemy.Length; i++)
        {
            if (_enemytransform[i] != null) EnemyCheck(_currentenemy[i], _enemytransform[i]);

        }
    }
    private void EnemyCheck(GameObject _currentEnemy, Transform _enemytransform)
    {
        Vector3 viewPos = _maincam.WorldToViewportPoint(_enemytransform.position);
        _currentEnemy.SetActive(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1);
    }
   

}
