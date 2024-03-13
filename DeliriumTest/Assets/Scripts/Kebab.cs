using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kebab : MonoBehaviour
{
    #region references
    [SerializeField] private LifeBarComponenet lifeBarComponent;
    [SerializeField] private VomitComponent vomitComponent;
    [SerializeField] private float cambioVomito;
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if(player != null)
        {
            lifeBarComponent.HealthUp();
            vomitComponent._vomitcuantity = cambioVomito;
            Destroy(this.gameObject);
        }
    }
}
