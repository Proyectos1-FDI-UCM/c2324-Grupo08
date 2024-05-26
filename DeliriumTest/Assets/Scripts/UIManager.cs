using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Se adquieren las referencias a las imágenes de la UI
    /// </summary>
    [SerializeField]
    Image imgCono;
    [SerializeField]
    Image imgBotella;

    [SerializeField]
    Image imgChapitas;
    [SerializeField]
    Image imgKebab;
    [SerializeField]
    Image imgEnergetica;

    [SerializeField]
    Image imgIndEng;
    [SerializeField]
    Image imgIndKeb;
    [SerializeField]
    Image imgIndChap;
    /// <summary>
    /// Tras una cantidad de tiempo determinada, se deja de mostrar el p
    /// </summary>
    public float timeMejora;

    static UIManager uiManager;
    public static UIManager UiManager { get { return uiManager; } }

    /// <summary>
    /// Se asegura que solo haya una instancia de la UI siguiendo el patrón Singleton
    /// </summary>
    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else 
        { 
            Destroy(gameObject);
            Debug.Log("Un componente duplicado ha sido borrado => Tipo: \""+ name + "\".");
        }
    }
    /// <summary>
    /// Se muestra el icono de los objetos
    /// </summary>
    public void PonerSprite(int value)
    {
        if (value == 0) imgBotella.enabled = true;
        else if (value == 1) imgCono.enabled = true;


    }
    /// <summary>
    /// Se deshabilita el icono de la botella
    /// </summary>
    public void QuitarSpriteBotella()
    {
        imgBotella.enabled = false;
    }
    /// <summary>
    /// Se deshabilita el icono del cono
    /// </summary>
    public void QuitarSpriteCono()
    {
        imgCono.enabled = false;
    }

    /// <summary>
    /// Dependiendo de la mejora obtenida, se muestra el icono  de esta en la UI
    /// </summary>
    public void PonerMejora(int mejora)
    {
        if (mejora == 3) imgKebab.enabled = true;

        if (mejora == 5) imgChapitas.enabled = true;

        if (mejora == 4) imgEnergetica.enabled = true;
    }

    /// <summary>
    /// Dependiendo de la mejora obtenida, se muestra el indicador esta en la UI
    /// </summary>
    public void PonerIndMejora(int mejora)
    {
        if (mejora == 3) imgIndKeb.enabled = true;

        else if (mejora == 5) imgIndChap.enabled = true;

        else if (mejora == 4) imgIndEng.enabled = true;

        StartCoroutine(DeactivateMejora(mejora));

    }
    /// <summary>
    /// Se deshabilitan todos los sprites al inicio
    /// </summary>
    GameObject Faltante;
    private void Start()
    {
        if (imgBotella == null)
        {
            Debug.Log("Falta la asignación de la imagen de la botella");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgBotella = Faltante.AddComponent<Image>();
            }
            else
            {
                imgBotella = Faltante.GetComponent<Image>();
            }

        }
        if (imgCono == null)
        {
            Debug.Log("Falta la asignación de la imagen del Cono");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgCono = Faltante.AddComponent<Image>();
            }
            else
            {
                imgCono = Faltante.GetComponent<Image>();
            }
        }
        if (imgChapitas == null)
        {
            Debug.Log("Falta la asignación de la imagen de mejora \"Chapas\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgChapitas = Faltante.AddComponent<Image>();
            }
            else
            {
                imgChapitas = Faltante.GetComponent<Image>();
            }
        }
        if (imgEnergetica == null)
        {
            Debug.Log("Falta la asignación de la imagen de mejora \"Energetica\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgEnergetica = Faltante.AddComponent<Image>();
            }
            else
            {
                imgEnergetica = Faltante.GetComponent<Image>();
            }
            
        }
        if (imgKebab == null)
        {
            Debug.Log("Falta la asignación de la imagen de mejora \"Kebab\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgKebab = Faltante.AddComponent<Image>();
            }
            else
            {
                imgKebab = Faltante.GetComponent<Image>();
            }
        }
        if (imgIndKeb == null)
        {
            Debug.Log("Falta la asignación de la imagen indicadora de mejora \"Kebab\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgIndKeb = Faltante.AddComponent<Image>();
            }
            else
            {
                imgIndKeb = Faltante.GetComponent<Image>();
            }
        }
        if (imgIndChap == null)
        {
            Debug.Log("Falta la asignación de la imagen indicadora de mejora \"Chapas\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgIndChap = Faltante.AddComponent<Image>();
            }
            else
            {
                imgIndChap = Faltante.GetComponent<Image>();
            }
        }
        if (imgIndEng == null)
        {
            Debug.Log("Falta la asignación de la imagen indicadora de mejora \"Energetica\"");
            if (Faltante == null)
            {
                Faltante = new GameObject();
                Faltante.name = "ImagenesFaltantesDelUIManager";
                imgIndEng = Faltante.AddComponent<Image>();
            }
            else
            {
                imgIndEng = Faltante.GetComponent<Image>();
            }
        }
        imgBotella.enabled = false;
        imgCono.enabled = false;
        imgChapitas.enabled = false;
        imgKebab.enabled = false;
        imgEnergetica.enabled = false;
        imgIndChap.enabled = false;
        imgIndEng.enabled = false;
        imgIndKeb.enabled = false;
    }

    /// <summary>
    /// Se deshabilita el sprite del indicador de la mejora
    /// </summary>
    IEnumerator DeactivateMejora(int mejora)
    {
        yield return new WaitForSeconds(timeMejora);
        if (mejora == 3) imgIndKeb.enabled = false;

        else if (mejora == 5) imgIndChap.enabled = false;

        else if (mejora == 4) imgIndEng.enabled = false;

        StopCoroutine(DeactivateMejora(mejora));
    }

}
