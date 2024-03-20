using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RatMovement : MonoBehaviour, EnemiesControler
{
    #region references
    private Transform _myTransform;
    private Transform _target;
    [SerializeField] private LayerMask pared;
    #endregion
    #region properties
    private bool _characterClose; //compruba si el jugador esta cerca para cambiar su movimiento
    bool hit;
    private Vector3 _direction;
    #endregion
    #region parameter
    [SerializeField] private float _speed = 2.0f;
    private float _moveTime, _stopTime;
    private int x, y;
    #endregion

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
    }
    private void OnCollisionExit2D()
    {
       hit = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null) 
        {
            _characterClose = true;
            _target = other.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FrankMovement>() != null)
        {
            _characterClose = false;
            _moveTime = 0;
            _stopTime = 0.5f;
        }
    }
    void Start()
    {
        _myTransform = transform;
        _characterClose = false;
        hit = false;
        _moveTime = 0;
        _stopTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterClose && !hit)
        {
            _direction = (_target.position - _myTransform.position).normalized;
            _myTransform.position += _direction * _speed * Time.deltaTime;
        }
        else
        {
            if (_moveTime <= 0)
            {
                if (_stopTime <= 0) //Acabado el tiempo de parada, declara la nueva direccion
                {
                    x = Random.Range(-4, 5);
                    y = Random.Range(-4, 5);
                    _direction = new Vector3(x, y, 0).normalized;
                    _moveTime = Random.Range(0, 3);
                    _stopTime = Random.Range(1, 2);

                    Collider2D collider = Physics2D.OverlapCircle(_myTransform.position, 1f, pared);
                    if (collider != null)
                    {
                        x = Random.Range(-45, 45);
                        _direction = Quaternion.Euler(0f, 0f, x) * (-collider.gameObject.transform.up);
                    }
                }
                else //Tiempo de parada
                {
                    _stopTime -= Time.deltaTime;
                    _direction = Vector3.zero;
                }
            }
            _myTransform.position += _direction * _speed * Time.deltaTime;
            _moveTime -= Time.deltaTime;
        }
    }
    private void OnDestroy()
    {
        LevelManager.EnemyDefeated(this);
    }
}
