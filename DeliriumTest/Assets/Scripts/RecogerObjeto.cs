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
    [SerializeField] private float healing;
   
    void Start()
    {
        _healthComponent = GetComponent<HealthComponent>(); 
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
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
