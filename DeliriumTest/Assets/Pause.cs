using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    static bool pause;
    Image pauseMenu;
    [SerializeField] float pauseTime = 2;
    float elapsedTime = 0;
    private void Awake()
    {
        pause = false;
        pauseMenu = GetComponent<Image>();
        pauseMenu.fillAmount = 0.0f;
    }
    public void PauseGame()
    {
        pause = !pause;
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (!(allScripts[i] as Pause) && !(allScripts[i] as Image)) allScripts[i].enabled = !pause;
        }

        FrankMovement.Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator Rellenar()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.fillAmount = Mathf.Lerp(pauseMenu.fillAmount, 1, elapsedTime / pauseTime);

    }
    IEnumerator Vaciar()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.fillAmount = Mathf.Lerp(pauseMenu.fillAmount, 0, elapsedTime / pauseTime);
        if(!Mathf.Approximately(pauseMenu.fillAmount,0)) { pauseMenu.fillAmount = 0; }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
        if (pause)
        {
            elapsedTime += Time.deltaTime;
            StartCoroutine(Rellenar());
        }
        else
        {
            StartCoroutine(Vaciar());
            //if(elapsedTime >= pauseTime) 
            elapsedTime = 0;
        }
    }

}
