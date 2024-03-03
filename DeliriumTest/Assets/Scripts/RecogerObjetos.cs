using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjetos : MonoBehaviour
{
    private PlayerAttack _player;
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
                    _player.AtaqueCono();
                }
                break;
        }     
    }
    private void Start()
    {
        _player = GetComponentInChildren<PlayerAttack>();
        canBePicked = true;
    }
}
public enum ObjectStatus
{
    botella=0,
    cono=1,
}
