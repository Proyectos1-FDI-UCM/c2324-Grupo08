using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoArrowController : MonoBehaviour
{
    static GoArrowController instance;
    public static GoArrowController GoArrowReference
    {
        get { return instance; }

        private set { instance = value; }
    }
    private void Awake()
    {
        //Sigleton de la Flecha.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Un componente duplicado ha sido borrado => Tipo: \"" + name + "\".");
        }

    }
    public void SetActive(bool state)
    {
        //Para activar o desactivar el objeto desde otros componentes
        instance.gameObject.SetActive(state);
    }
}
