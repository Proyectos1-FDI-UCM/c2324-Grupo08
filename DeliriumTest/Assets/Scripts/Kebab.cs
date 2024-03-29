using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
    #region references
    [SerializeField] private LifeBarComponenet lifeBarComponent;
    [SerializeField] private VomitComponent vomitComponent;
    private float cambioVomito = 0.5f;
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if(player != null)
        {
            lifeBarComponent.HealthUP();
            vomitComponent._vomitcuantity = cambioVomito;
            Destroy(this.gameObject);
        }
    }
}
