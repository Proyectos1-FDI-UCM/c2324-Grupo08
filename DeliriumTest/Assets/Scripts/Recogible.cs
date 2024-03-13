using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recogible : MonoBehaviour
{
    [SerializeField] private int _objectID;
    /* 
     * 1 = Bolsa de Patatas
     * 2 = Botella de Agua
     * 3 = Kebab
     * 4 = Bebida Energética
     * 5 = Cubo de Chapas
     */
     
     

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RecogerObjeto recogida = collision.GetComponent<RecogerObjeto>();
        if (recogida != null)
        {
            recogida.Recogida(_objectID, this.gameObject);
        }
    }
    void Update()
    {
        
    }
}
