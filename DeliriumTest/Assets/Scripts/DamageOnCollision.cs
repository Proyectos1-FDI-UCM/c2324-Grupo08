using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
