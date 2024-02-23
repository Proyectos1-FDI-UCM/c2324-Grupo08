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
    #region references

    #endregion
    #region propiedades
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();   
    }

    public void RegisterVector(Vector3 direction)
    {
        _speed = direction;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<FrankMovement>() != null || collision.gameObject.GetComponent<EnemiesControler>() != null
            || collision.gameObject.layer == 7) 
            Destroy(gameObject);
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        _rigidBody.velocity = _speed * _bulletSpeed;
    }
}
