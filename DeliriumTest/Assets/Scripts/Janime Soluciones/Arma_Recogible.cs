using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_Recogible : MonoBehaviour
{
    //ATENCIÓN CON ESTE CAMPO:
    [SerializeField] int ID;
    /* CONSIDERACIONES IMPORTANTES 
     * PARA EL USO CORRECTO DE ESTE COMPONENTE:
     * 1º El Id de los objetos tiene el siguiente orden:
     * { 
     *  Botella = 0;
     *  Cono = 1;
     * } 
     */
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite highlight;
    private SpriteRenderer spriteRenderer;
    public int RecogibleID { get { return ID; } }
    Recolector_de_Armas recogerObjetosP;
    public bool picked = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            if (recogerObjetosP.canBePicked) spriteRenderer.sprite = highlight;
            picked = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FrankMovement player = collision.GetComponent<FrankMovement>();
        if (player != null)
        {
            spriteRenderer.sprite = normal;
            picked = false;
        }
    }
    private void ObjetoRecogido()
    {
        recogerObjetosP.RegisterObject(this);
    }
    public void RegisterPaco(Recolector_de_Armas recogerObjetos)
    {
        recogerObjetosP = recogerObjetos;
        Debug.Log("Información del prefab recogido");
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        picked = false;
        spriteRenderer.sprite = normal;
        recogerObjetosP = FrankMovement.Player.GetComponent<Recolector_de_Armas>();
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
