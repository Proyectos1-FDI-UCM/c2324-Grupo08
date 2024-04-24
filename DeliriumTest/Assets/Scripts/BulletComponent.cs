using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _bulletSpeed = 10f;
    private Rigidbody2D _rigidBody;
    private Vector3 _speed;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();   
    }
    /// <summary>
    /// Se llama al instanciarse la bala. Setea su dirección con el vector direction.
    /// </summary>
    /// <param name="direction"></param>
    public void RegisterVector(Vector3 direction)
    {
        _speed = direction;
        
    }
    /// <summary>
    /// El objeto al colisionar con el personaje, los enemigos o los bordes, se destruye
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FrankMovement>() != null || collision.gameObject.GetComponent<EnemiesControler>() != null || collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
            Destroy(gameObject);
    }
    /// <summary>
    /// Al colisionar con algún objeto no previsto, si su velocidad es cero, se destruye
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_rigidBody.velocity.Equals((Vector3.zero)))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    /// <summary>
    /// Mueve por fisicas la bala según su dirección y su velocidad
    /// </summary>
    void FixedUpdate()
    {
        
        _rigidBody.velocity = _speed * 10f * _bulletSpeed * Time.fixedDeltaTime;
        
    }
}
