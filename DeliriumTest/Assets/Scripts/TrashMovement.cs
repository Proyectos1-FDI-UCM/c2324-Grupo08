using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMovement : MonoBehaviour, EnemiesControler
{
    #region references
    private Transform _myTransform;
    private Transform _target;
    private ShootComponent _shootComponent;
    #endregion
    #region properties
    private bool _characterClose; //compruba si el jugador esta cerca para cambiar su movimiento, 
    private Vector3 _direction;
    #endregion
    #region parameter
    [SerializeField] private float _speed = 2.0f;
    private float _moveTime, _stopTime;
    private int x, y;
    [SerializeField] private float _stopped, _moving;
    #endregion

    // Start is called before the first frame update
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
            _stopTime = 0;
        }
    }
    void Start()
    {
        _shootComponent = GetComponent<ShootComponent>();
        _myTransform = transform;
        _characterClose = false;
        _moveTime = 0;
        _stopTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterClose)
        {
            if (_moveTime <= 0)
            {
                if (_stopTime <= 0) //Acabado el tiempo de parada, declara la nueva direccion
                {
                    
                    _moveTime = _moving;
                    _stopTime = _stopped;
                    _direction = (_target.position - _myTransform.position).normalized;
                    StartCoroutine(_shootComponent.Disparo(_direction));
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
        LevelManager.levelManager.EnemyDefeated(this);
    }

    public IEnumerator StopAttack()
    {
        
        throw new System.NotImplementedException();
    }
}
