using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerArmas : MonoBehaviour
{
    #region referneces
    [SerializeField] private GameObject _ataqueCono;
    [SerializeField] private GameObject _ataqueBotella;
    [SerializeField] private GameObject ataque;
    [SerializeField] private Objetorecojible objetoRecojibleCono;
    [SerializeField] private Objetorecojible objetoRecojibleBotella;
    [SerializeField] private UIManager _uiManager;
    #endregion

    #region parameters
    public bool canBePicked = true;
    #endregion

    #region properties
    private Animator ataqueCono;
    #endregion

    public void ObjectSellector(ObjectStatus status)
    {
        switch (status)
        {
            case ObjectStatus.botella: // Activa el ataque tipo botella
                if (canBePicked)
                {
                    canBePicked = false;
                    _ataqueBotella.SetActive(true);
                }
                break;
            case ObjectStatus.cono: // Activa el ataque tipo cono
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
    //Permite recoger la información de lo prefabs de cono y botella
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
