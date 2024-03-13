using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Salida");
    }
    public void Enter()
    {
        SceneManager.LoadScene("PruebaParaEl28");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
}
