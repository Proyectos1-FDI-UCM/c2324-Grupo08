using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolector_de_Armas : MonoBehaviour
{
    PlayerAttack _playerAttack;
    [SerializeField] private Arma_Recogible objetoRecojibleCono;
    [SerializeField] private Arma_Recogible objetoRecojibleBotella;
    [SerializeField] private UIManager _uiManager;
    public bool canBePicked;
    public void ObjectSellector(Armas status)
    {
        switch (status)
        {
            case Armas.botella:
                if (canBePicked)
                {
                    canBePicked = false;
                    _playerAttack.Botella();
                }
                break;
            case Armas.cono:
                if (canBePicked)
                {
                    canBePicked = false;
                    _playerAttack.Cono();
                }
                break;
        }
    }
    private void Start()
    {
        canBePicked = true;
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        if(_uiManager == null)
        {
            Debug.Log(name + ": Falta la referencia al Componente \"UIManager\"");
            _uiManager = UIManager.UiManager;
            if (_uiManager == null)
            {
                GameObject Faltante = new GameObject();
                Faltante.name = "UIManager";
                _uiManager = Faltante.AddComponent<UIManager>();
            }
        }
    }
    public void RegisterObject(Arma_Recogible arma)
    {
        if (canBePicked) 
        {
            Destroy(arma.gameObject);
            int value = arma.RecogibleID;
            ObjectSellector((Armas)value);
            _uiManager.PonerSprite(value);
        }
    }
}
public enum Armas
{
    botella = 0,
    cono = 1,
}
