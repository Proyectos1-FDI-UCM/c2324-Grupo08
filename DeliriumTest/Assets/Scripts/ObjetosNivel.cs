using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetorecojible : MonoBehaviour
{
    [SerializeField] ObjetoRecojible objetoRecojible;
    private RecogerObjetos recogerObjetos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player =collision.GetComponent<FrankMovement>();
        if(player != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (objetoRecojible.ObjectID == 0)
                {
                    recogerObjetos.ObjectSellector((ObjectStatus)objetoRecojible.ObjectID);
                    Destroy(this.gameObject);
                }
                if (objetoRecojible.ObjectID == 1)
                {
                    recogerObjetos.ObjectSellector((ObjectStatus)objetoRecojible.ObjectID);
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void Start()
    {
        recogerObjetos=GetComponent<RecogerObjetos>();
    }
}
