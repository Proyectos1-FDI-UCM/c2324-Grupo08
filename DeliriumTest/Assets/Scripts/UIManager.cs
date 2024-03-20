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
    static UIManager uiManager;
    public static UIManager UiManager { get { return uiManager; } }
    private void Awake()
    {
        if(uiManager == null) { uiManager = this; }
        else { Destroy(this.gameObject); }
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

    private void Start()
    {
        imgBotella.enabled = false;
        imgCono.enabled = false;
    }
}
