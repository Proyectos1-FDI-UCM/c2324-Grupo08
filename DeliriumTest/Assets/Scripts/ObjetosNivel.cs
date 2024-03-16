using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetorecojible : MonoBehaviour
{
    [SerializeField] ObjetoRecojible objetoRecojible;
    private RecogerObjetos recogerObjetosP;
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
        recogerObjetosP.RegisterObject(objetoRecojible.ObjectID);
        Destroy(gameObject);
    }
    public void RegisterPaco(RecogerObjetos recogerObjetos)
    {
        recogerObjetosP = recogerObjetos;
        Debug.Log("Información del prefab recogido");
    }
    private void Start()
    {
        picked = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && picked && recogerObjetosP.canBePicked)
        {
            ObjetoRecogido();
            Debug.Log("Objeto Recogido");
        }
    }
}
