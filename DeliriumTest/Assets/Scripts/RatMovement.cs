using System.Collections;
using UnityEngine;

public class RatMovement : MonoBehaviour, EnemiesControler
{
    #region references
    private Transform _myTransform;
    private Transform _target;
    Rigidbody2D _rigidbody2d;
    [SerializeField] private LayerMask pared;
    #endregion
    #region properties
    private bool _characterClose; //compruba si el jugador esta cerca para cambiar su movimiento
    
    bool hit;
    
    [SerializeField]
    float SecondsToWaitAfterHit;

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
        if (collision.gameObject.GetComponent<FrankMovement>() != null || collision.gameObject.GetComponent<PlayerAttack>() != null) 
        {
            hit = true; 
        }
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
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _myTransform = transform;
        _characterClose = false;
        hit = false;
        _moveTime = 0;
        _stopTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hit)
        {
            StartCoroutine(StopAttack());
        }
        else
        {
            StartCoroutine(Move());
        }
    }
    IEnumerator Move()
    {
        
        if (_characterClose)
        {
            _direction = (_target.position - _myTransform.position).normalized;
            _rigidbody2d.velocity = _direction * 1.5f * _speed * Time.fixedDeltaTime;
        }
        else
        {
            
           if(_moveTime <= 0) 
            {
                _direction = Vector3.zero;
                _stopTime -= Time.fixedDeltaTime;
                if (_stopTime <= 0)
                {
                    x = Random.Range(-4, 5);
                    y = Random.Range(-4, 5);
                    _direction = new Vector3(x, y, 0).normalized;
                    _moveTime = Random.Range(0, 3);
                    _stopTime = Random.Range(1, 2);
                }
                Collider2D collider = Physics2D.OverlapCircle(_myTransform.position, 1f, pared);
                if (collider != null)
                {
                    x = Random.Range(-45, 45);
                    _direction = Quaternion.Euler(0f, 0f, x) * (-collider.gameObject.transform.up);
                }
            }
            _moveTime -= Time.fixedDeltaTime;
            _rigidbody2d.velocity = _direction * _speed * Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnDestroy()
    {
        LevelManager.levelManager.EnemyDefeated(this);
    }

    public IEnumerator StopAttack()
    {
        StopCoroutine(Move());
        _rigidbody2d.velocity = Vector2.zero;
        _rigidbody2d.velocity = (-1.5f * _speed * _direction * Time.fixedDeltaTime);
        yield return new WaitForSeconds(SecondsToWaitAfterHit);
        _rigidbody2d.velocity = Vector2.zero;
        hit = false;       
    }

    
}
