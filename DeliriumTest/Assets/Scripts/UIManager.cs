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
        if(mejora == 5) imgChapitas.enabled = true;
        if(mejora == 4) imgEnergetica.enabled = true;
    }

    private void Start()
    {
        imgBotella.enabled = false;
        imgCono.enabled = false;
        imgChapitas.enabled = false;
        imgKebab.enabled = false;
        imgEnergetica.enabled = false;
    }
}
