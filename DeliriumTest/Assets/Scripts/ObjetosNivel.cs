using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetorecojible : MonoBehaviour
{
    [SerializeField] ObjetoRecojible objetoRecojible;
    [SerializeField] private RecogerObjetos recogerObjetos;
    [SerializeField] private GameObject objeto;
    public bool picked = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            picked = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            picked = false;
        }
    }
    private void ObjetoRecogido()
    {
        recogerObjetos.RegisterObject(objetoRecojible.ObjectID);
        Destroy(objeto);
    }
    
    private void Start()
    {
        picked = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && picked && recogerObjetos.canBePicked)
        {
            ObjetoRecogido();
        }
    }
}
