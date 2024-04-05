using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    static bool pause;
    Image pauseMenu;
    [SerializeField]List<GameObject> pauseelements = new List<GameObject>();
    [SerializeField] float pauseTime = 2;
    float elapsedTime = 0;
    private void Awake()
    {
        pause = false;
        pauseMenu = GetComponent<Image>();
        for (int i = 0; i < pauseelements.Count; i++) { pauseelements[i].SetActive(pause); }
        pauseMenu.fillAmount = 0.0f;
    }
    public IEnumerator PauseGame()
    {
        elapsedTime = 0;
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(pauseTime + 0.2f);
        }
        else
        {
            Time.timeScale = 1;
        }
        
        for (int i = 0; i < pauseelements.Count; i++) { pauseelements[i].SetActive(pause); }
    }
    public void PauseButtonMethod()
    {
        elapsedTime = 0;
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        for (int i = 0; i < pauseelements.Count; i++) { pauseelements[i].SetActive(pause); }
    }
    IEnumerator Rellenar()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.fillAmount = Mathf.Lerp(0, 1, elapsedTime / pauseTime);

    }
    IEnumerator Vaciar()
    {
        yield return new WaitForEndOfFrame();
        pauseMenu.fillAmount = Mathf.Lerp(1, 0, elapsedTime / pauseTime);

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && elapsedTime > (pauseTime + 0.15f))
        {
            StartCoroutine(PauseGame());
        }
        if (pause)
        {
            StartCoroutine(Rellenar());
        }
        else
        {
            StartCoroutine(Vaciar());
        }
        elapsedTime += Time.unscaledDeltaTime; 
       
    }

}
