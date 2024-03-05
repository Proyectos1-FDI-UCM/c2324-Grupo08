using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetorecojible : MonoBehaviour
{
    [SerializeField] ObjetoRecojible objetoRecojible;
    [SerializeField] private RecogerObjetos recogerObjetos;
    [SerializeField] private GameObject ataque;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            recogerObjetos.ObjectSellector((ObjectStatus)objetoRecojible.ObjectID);
            ataque.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
