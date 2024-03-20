using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class RecogerObjeto : MonoBehaviour
{
    [SerializeField] private Slider vomitBar;
    [SerializeField] private float ReduceVomit;
    [SerializeField] private LifeBarComponenet lifebar;
    private HealthComponent _healthComponent;
    private Damage _damage;
    private PlayerAttack _playerAttack;
    private InputManager _inputManager;
    [SerializeField] private float healing;
    [SerializeField] private LifeBarComponenet lifeBarComponent;
    [SerializeField] private VomitComponent vomitCuantity;
    private float cambioVomito = 0.5f;
    /* 
    * 1 = Bolsa de Patatas
    * 2 = Botella de Agua
    * 3 = Kebab
    * 4 = Bebida Energética
    * 5 = Cubo de Chapas
    */
    void Start()
    {
        _healthComponent = GetComponent<HealthComponent>(); 
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        _damage = GetComponentInChildren<Damage>();
        _inputManager = GetComponent<InputManager>();
    }

    public void Recogida(int ObjID, GameObject pickedObj)
    {

        if(ObjID == 1) 
        { 
            vomitBar.value = vomitBar.value - ReduceVomit;
            Destroy(pickedObj);
        }

        else if (ObjID == 2)
        {
            if (_healthComponent.Health != _healthComponent.MaxHealth)
            {
                _healthComponent.Healing(healing);
                lifebar.DrawHearts();
                Destroy(pickedObj);
            }           
        }
        else if( ObjID == 3)
        {
            lifeBarComponent.HealthUP();
            vomitCuantity._vomitcuantity = cambioVomito;
            Destroy(pickedObj);
        }
        else if(ObjID == 5)
        {
            _inputManager.DisableCooldown();
            _playerAttack.Chapas();
            Destroy(pickedObj);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
