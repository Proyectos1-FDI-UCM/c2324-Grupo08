using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeComponent : MonoBehaviour
{
    #region references
    public Sprite fullHeart, halfHeart, noHeart;
    Image heartImage;
    #endregion

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }
    //En función de la vida en ese momento, la vida del jugadro cambia entre estos tipos de vida : lleno, medio y vacio
    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = noHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }
}
public enum HeartStatus
{
    Empty=0,
    Half=1, 
    Full=2,
}
