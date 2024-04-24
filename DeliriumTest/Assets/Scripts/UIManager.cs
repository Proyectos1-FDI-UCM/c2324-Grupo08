using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    public float timeMejora;

    static UIManager uiManager;
    public static UIManager UiManager { get { return uiManager; } }
    private void Awake()
    {
        if (uiManager == null) 
        { 
            uiManager = this;
        }
        else { Destroy(gameObject); }
    }
    public void PonerSprite(int value)
    {
        if (value == 0) imgBotella.enabled = true;
        else if (value == 1) imgCono.enabled = true;


    }
    public void QuitarSpriteBotella()
    {
        imgBotella.enabled = false;
    }
    public void QuitarSpriteCono()
    {
        imgCono.enabled = false;
    }

    
    public void PonerMejora(int mejora)
    {
        if (mejora == 3) imgKebab.enabled = true;

        if (mejora == 5) imgChapitas.enabled = true;

        if (mejora == 4) imgEnergetica.enabled = true;
    }

    public void PonerIndMejora(int mejora)
    {
        if (mejora == 3) imgIndKeb.enabled = true;

        else if (mejora == 5) imgIndChap.enabled = true;

        else if (mejora == 4)imgIndEng.enabled = true;

        StartCoroutine(DeactivateMejora(mejora));
        
    }

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


    IEnumerator DeactivateMejora(int mejora)
    {
        yield return new WaitForSeconds(timeMejora);
        if (mejora == 3) imgIndKeb.enabled = false;

        else if (mejora == 5) imgIndChap.enabled = false;

        else if (mejora == 4) imgIndEng.enabled = false;

        StopCoroutine(DeactivateMejora(mejora));
    }
    
}
