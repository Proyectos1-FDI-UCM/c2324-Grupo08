using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image imgCono;
    [SerializeField] private Image imgBotella;
    public void PonerSprite(int value)
    {
        if (value == 0) imgBotella.enabled = true;
        else if (value == 1) imgCono.enabled = true;
    }
    public void QuitarSprite(Image img)
    {
        img.enabled = false;
    }

    private void Start()
    {
        imgBotella.enabled = false;
        imgCono.enabled = false;
    }
}
