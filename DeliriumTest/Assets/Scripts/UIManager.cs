using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Se adquieren las referencias a las imágenes de la UI
    /// </summary>
    [SerializeField] 
    Image imgCono;
    [SerializeField] 
    Image imgBotella;

    [SerializeField]
    Image imgChapitas;
    [SerializeField]
    Image imgKebab;
    [SerializeField]
    Image imgEnergetica;

    [SerializeField]
    Image imgIndEng;
    [SerializeField]
    Image imgIndKeb;
    [SerializeField]
    Image imgIndChap;
    /// <summary>
    /// Tras una cantidad de tiempo determinada, se deja de mostrar el p
    /// </summary>
    public float timeMejora;

    static UIManager uiManager;
    public static UIManager UiManager { get { return uiManager; } }

    /// <summary>
    /// Se asegura que solo haya una instancia de la UI siguiendo el patrón Singleton
    /// </summary>
    private void Awake()
    {
        if (uiManager == null) 
        { 
            uiManager = this;
        }
        else { Destroy(gameObject); }
    }
    /// <summary>
    /// Se muestra el icono de los objetos
    /// </summary>
    public void PonerSprite(int value)
    {
        if (value == 0) imgBotella.enabled = true;
        else if (value == 1) imgCono.enabled = true;


    }
    /// <summary>
    /// Se deshabilita el icono de la botella
    /// </summary>
    public void QuitarSpriteBotella()
    {
        imgBotella.enabled = false;
    }
    /// <summary>
    /// Se deshabilita el icono del cono
    /// </summary>
    public void QuitarSpriteCono()
    {
        imgCono.enabled = false;
    }

    /// <summary>
    /// Dependiendo de la mejora obtenida, se muestra el icono  de esta en la UI
    /// </summary>
    public void PonerMejora(int mejora)
    {
        if (mejora == 3) imgKebab.enabled = true;

        if (mejora == 5) imgChapitas.enabled = true;

        if (mejora == 4) imgEnergetica.enabled = true;
    }

    /// <summary>
    /// Dependiendo de la mejora obtenida, se muestra el indicador esta en la UI
    /// </summary>
    public void PonerIndMejora(int mejora)
    {
        if (mejora == 3) imgIndKeb.enabled = true;

        else if (mejora == 5) imgIndChap.enabled = true;

        else if (mejora == 4)imgIndEng.enabled = true;

        StartCoroutine(DeactivateMejora(mejora));
        
    }
    /// <summary>
    /// Se deshabilitan todos los sprites al inicio
    /// </summary>
    private void Start()
    {
        imgBotella.enabled = false;
        imgCono.enabled = false;
        imgChapitas.enabled = false;
        imgKebab.enabled = false;
        imgEnergetica.enabled = false;
        imgIndChap.enabled=false;
        imgIndEng.enabled=false;
        imgIndKeb.enabled=false;
    }

    /// <summary>
    /// Se deshabilita el sprite del indicador de la mejora
    /// </summary>
    IEnumerator DeactivateMejora(int mejora)
    {
        yield return new WaitForSeconds(timeMejora);
        if (mejora == 3) imgIndKeb.enabled = false;

        else if (mejora == 5) imgIndChap.enabled = false;

        else if (mejora == 4) imgIndEng.enabled = false;

        StopCoroutine(DeactivateMejora(mejora));
    }
    
}
