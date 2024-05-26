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
    /// <summary>
    /// Cambia a la escena del nivel y reproduce la canción del nivel
    /// </summary>
    public void Enter()
    {
        SceneManager.LoadScene("ProcPrueba");
        AudioManager.Instance.LevelMusic();
    }
    /// <summary>
    /// Cambia a la escena de muerte
    /// </summary>
    public void Dead()
    {
        SceneManager.LoadScene("DeadMenu");
    }
    /// <summary>
    /// Cambia a la escena del Menú Principal
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.MenuMusic();
    }
    /// <summary>
    /// Cambia a la escena de lo créditos y reproduce la música del menú, que se reutilizará para esta escena
    /// </summary>
    public static  void Credits()
    {
        SceneManager.LoadScene("Credits");
        AudioManager.Instance.MenuMusic();
    }
    /// <summary>
    /// Si el jugador pulsa la tecla "Escape", se detendrá la ejecución del programa
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }
    private void Awake()
    {
        if (menu == null) 
        {
            menu = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Un componente duplicado ha sido borrado => Tipo: \"" + name + "\".");
        }
         
    }
    /// <summary>
    /// Si la escena corresponde al Menú principal sonará la canción de este mismo
    /// Sucederá lo mismo con el nivel
    /// </summary>
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
