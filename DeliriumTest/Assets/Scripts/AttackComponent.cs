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
    /// <summary>
    /// Al colisionar, si el objeto afectado tiene vida, le resta la cantidad Attack. 
    /// Si el objeto es una rata que ataca a Paco, inicia un cooldown de ataque
    /// </summary>
    /// <param name="collision"></param>
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
