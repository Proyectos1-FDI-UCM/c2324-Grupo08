using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjetos : MonoBehaviour
{
    [SerializeField] private Cono _cono;
    [SerializeField] private GameObject _ataqueCono;
    [SerializeField] private GameObject _ataqueBotella;
    [SerializeField] private GameObject ataque;

    public bool canBePicked;
    public void ObjectSellector(ObjectStatus status)
    {
        switch (status)
        {
            case ObjectStatus.botella:
                if (canBePicked)
                {
                    canBePicked = false;
                    _ataqueBotella.SetActive(true);
                }
                break;
            case ObjectStatus.cono:
                if (canBePicked)
                {
                    canBePicked = false;
                    _ataqueCono.SetActive(true);
                    _cono.AtaqueCono();
                }
                break;
        }
    }
    private void Start()
    {
        canBePicked = true;
    }
    public void RegisterObject(int value)
    {
        ObjectSellector((ObjectStatus)value);
        ataque.SetActive(false);
    }
}
public enum ObjectStatus
{
    botella=0,
    cono=1,
}
