using System.Collections;
using System;
using UnityEngine;

public class Damage : MonoBehaviour
{
    #region properties
    [SerializeField]
    private int _Attack = 1;
    public int Attack { get { return _Attack; } set { _Attack = value; } }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent target = collision.gameObject.GetComponent<HealthComponent>();
        if (target != null)
        {
            target.TakeDamage(Attack);
            if (this.gameObject.GetComponent<RatMovement>() != null && collision.gameObject.GetComponent<FrankMovement>() != null)
            {
                gameObject.GetComponent<RatMovement>().StopAttack();
            }
        }
    }
}
