using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtracttionComponent : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _attraction;
    #endregion

    #region references
    private Transform _attractionTransform;
    #endregion
    /// <summary>
    /// Colision paco con alcantarilla
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement A = collision.GetComponent<FrankMovement>();
        if (A != null)
        {
            Vector2 _directionVector = _attractionTransform.position - collision.transform.position;
            collision.GetComponent<DashCompnent>().enabled = false;
            if (_directionVector.sqrMagnitude > 0.01f)
            {
                A.atraccion = _attraction * _directionVector.normalized;
            } 
            else 
            {
                A.atraccion = Vector2.zero;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement A = collision.GetComponent<FrankMovement>();
        if (A != null)
        {
            A.atraccion = Vector2.zero;
            collision.GetComponent<DashCompnent>().enabled = true;
        }
    }
    void Start()
    {
        _attractionTransform = transform;
    }

}
