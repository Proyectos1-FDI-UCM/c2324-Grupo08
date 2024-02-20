using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    #region properties
    [SerializeField]
    private int Attack;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(Attack);
        }
    }
}