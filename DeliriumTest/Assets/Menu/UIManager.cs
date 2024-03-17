using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager ui;
    public static UIManager UI { get { return ui; } }
    public void Exit()
    {
        Application.Quit();
    }
    public void Enter()
    {
        SceneManager.LoadScene("PruebaParaEl28");
    }
    public void Dead()
    {
        SceneManager.LoadScene("DeadMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
    private void Awake()
    {
        if (ui == null)
        {
            ui = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
