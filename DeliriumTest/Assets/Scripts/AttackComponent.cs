using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    #region properties
    [SerializeField]
    private int Attack = 1;
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthComponent player = collision.gameObject.GetComponent<HealthComponent>();
        if (player != null)
        {
           player.TakeDamage(Attack);
        }
    }
}
