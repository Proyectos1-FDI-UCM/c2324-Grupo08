using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoArrowController : MonoBehaviour
{
    void Awake()
    {
        //Se registra este componente en el levelManager
        LevelManager.levelManager.RegisterArroy(this);
    }
    public void SetActive(bool state)
    {
        //Para activar o desactivar el objeto desde otros componentes
        gameObject.SetActive(state);
    }
}
