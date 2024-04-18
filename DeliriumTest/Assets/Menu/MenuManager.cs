using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private static MenuManager menu;
    public static MenuManager Menu { get { return menu; } }
    public void Exit()
    {
        Application.Quit();
    }
    public void Enter()
    {
        SceneManager.LoadScene("ProcPrueba");
        AudioManager.Instance.LevelMusic();
    }
    public void Dead()
    {
        SceneManager.LoadScene("DeadMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.MenuMusic();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
    private void Awake()
    {
         menu = this;
    }
    private void Start()
    {
        if (SceneManager.GetSceneByBuildIndex(0).Equals(SceneManager.GetActiveScene()))
        {
            AudioManager.Instance.MenuMusic();
        }
        else if (SceneManager.GetSceneByBuildIndex(1).Equals(SceneManager.GetActiveScene()))
        {
            AudioManager.Instance.LevelMusic();
        }
    }
}
