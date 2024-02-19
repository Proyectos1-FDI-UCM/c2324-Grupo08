using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    #region references
    private Transform _mytransform;
    private Transform _target;
    #endregion
    #region properties
    private bool _characterClose; //compruba si el jugador esta cerca para cambiar su movimiento, 
    private Vector3 _direction;
    #endregion
    #region parameter
    [SerializeField] private float _speed = 2.0f;
    private float _moveTime, _stopTime;
    private int x, y;
    #endregion


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null) 
        {
            _characterClose = true;
            _target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _characterClose = false;
        }
    }
    void Start()
    {
        _mytransform = transform;
        _characterClose = false;
        _moveTime = 0;
        _stopTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterClose)
        {
            _direction = (_target.position - _mytransform.position).normalized;
            _mytransform.position += _direction * _speed * Time.deltaTime;
        }
        else
        {
            if (_moveTime <= 0)
            {
                if (_stopTime <= 0)
                {
                    x = Random.Range(-4, 5);
                    y = Random.Range(-4, 5);
                    _direction = new Vector3(x, y, 0).normalized;
                    _moveTime = Random.Range(0, 3);
                    _stopTime = Random.Range(1, 3);
                }
                else
                {
                    _stopTime -= Time.deltaTime;
                    _direction = Vector3.zero;
                }
            }
            _mytransform.position += _direction * _speed * Time.deltaTime;
            _moveTime -= Time.deltaTime;
        }
    }
}
