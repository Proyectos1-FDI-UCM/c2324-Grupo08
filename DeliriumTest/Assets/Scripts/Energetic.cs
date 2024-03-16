using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Energetic : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        FrankMovement player = collision.gameObject.GetComponent<FrankMovement>();
        if (player != null)
        {
            FrankMovement.Player.GetComponent<FrankMovement>().DashUpgrade();
            Destroy(this.gameObject);
        }
    }
}
