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
            A.atraccion = _attraction * _directionVector.normalized;
            collision.GetComponent<InputManager>().Atrapado(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement A = collision.GetComponent<FrankMovement>();
        if (A != null)
        {
            A.atraccion = Vector2.zero;
            collision.GetComponent<InputManager>().Atrapado(false);
        }
    }
    void Start()
    {
        _attractionTransform = transform;
    }

}
