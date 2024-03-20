using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D _body;
    [SerializeField] float _velocity;
    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        if (_body == null)
        {
            Debug.Log("El objeto, " + gameObject.name + " no cuenta con un RigidBody2D para ser movido por físicas");
        }
    }
    public void MoveWithNoPhysics(Vector3 direction)
    {
        transform.position +=  direction.normalized * _velocity * Time.deltaTime;
    }
    public void MoveWithNoPhysics(Vector3 direction, float velocity)
    {
        transform.position += direction.normalized * velocity * Time.deltaTime;
    }
    public void Move(Vector3 direction)
    {
         _body.velocity = direction.normalized * _velocity * Time.deltaTime; 
    }

}
