using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjetos : MonoBehaviour
{
    [SerializeField] private Cono _cono;
    [SerializeField] private GameObject _ataqueCono;
    public bool canBePicked;
    public void ObjectSellector(ObjectStatus status)
    {
        switch (status)
        {
            case ObjectStatus.botella:
                if (canBePicked)
                {
                    canBePicked=false;
                }
                break;
            case ObjectStatus.cono:
                if(canBePicked)
                {
                    canBePicked=false;
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
}
public enum ObjectStatus
{
    botella=0,
    cono=1,
}
