using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerArmas : MonoBehaviour
{
    [SerializeField] private GameObject _ataqueCono;
    [SerializeField] private GameObject _ataqueBotella;
    [SerializeField] private GameObject ataque;
    [SerializeField] private Objetorecojible objetoRecojibleCono;
    [SerializeField] private Objetorecojible objetoRecojibleBotella;
    [SerializeField] private UI _uiManager;
    private Animator ataqueCono;
    public bool canBePicked = true;
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
                    ataqueCono.SetBool("Attack", true);
                }
                break;
        }
    }
    private void Awake()
    {
        objetoRecojibleCono.RegisterPaco(this);
        objetoRecojibleBotella.RegisterPaco(this);
        ataqueCono = GetComponent<Animator>();
    }
    private void Start()
    {
        canBePicked = true;
    }
    public void RegisterObject(int value)
    {
        ObjectSellector((ObjectStatus)value);
        _uiManager.PonerSprite(value);
        ataque.SetActive(false);
    }    
}
public enum ObjectStatus
{
    botella=0,
    cono=1,
}
