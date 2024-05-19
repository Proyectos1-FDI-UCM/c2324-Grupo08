using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Guarda todas las salas Faciles
    /// </summary>
    public GameObject[] EasyRooms;

    /// <summary>
    /// Guarda todas las salas Dificiles
    /// </summary>
    public GameObject[] HardRooms;

    /// <summary>
    /// Posición donde se colocara la sala a instanciar
    /// </summary>
    private Vector3 RoomPosition;

    /// <summary>
    /// Valor que marca el espacio necesario para centrar las salas de forma correcta
    /// </summary>
    public float RoomOffset = 0f;

    /// <summary>
    /// Numero que contiene el indice de la sala escogida aleatoriamente
    /// </summary>
    private int randomRoom;

    /// <summary>
    /// Sala que se va a instanciar
    /// </summary>
    private GameObject Room;

    /// <summary>
    /// Se coloca en la posición donde queremos instanciar una sala
    /// </summary>
    public Transform BuilderTransform;

    /// <summary>
    /// Vector que guarda cuanto se desplaza una sala con respecto a otra
    /// </summary>
    private Vector3 RoomOffsetVector;

    /// <summary>
    /// Primera sala del mapa y correspondiente al Tutorial
    /// </summary>
    [SerializeField] private GameObject Tutorial;

    /// <summary>
    /// Sala donde espera y habita el Boss
    /// </summary>
    [SerializeField] private GameObject BossFight;

    /// <summary>
    /// Sala entre las difíciles y las fáciles
    /// </summary>
    [SerializeField] private GameObject Intermedia;

    /// <summary>
    /// Entero que guarda el indice de la ultima sala generada
    /// </summary>
    int _currentRoom;

    /// <summary>
    /// Lista que contiene el orden del salas 
    /// </summary>
    public List<GameObject> Map;
    #endregion

    #region Parameters
    /// <summary>
    /// Lista que guarda las salas fáciles que todavia no se han generado
    /// </summary>
    public static List<int> _availableEasyRooms;

    /// <summary>
    /// Lista que guarda las salas dificiles que todavia no se han generado
    /// </summary>
    public static List<int> _availableHardRooms;

    /// <summary>
    /// Sala en la que se encuentra el jugador
    /// </summary>
    public static int ActiveRoom;
    #endregion

    /// <summary>
    /// Generara el mapa que recorrera el jugador. 
    /// Ordenando de forma aleatoria las salas faciles y dificiles, pero manteniendo el orden interno del mapa.
    /// Orden interno del Mapa: 1º Tutorial, 2º Faciles, 3º Intermedia, 4º Dificiles, 5º Boss.
    /// </summary>
    void Awake()
    {
        //Inicialización de todos los valores necesarios a sus valores correspondientes al inicio del juego
        #region SetUp

        //Empezamos en la sala 0 (Tutorial)
        ActiveRoom = 0;

        //Inicialización de las listas  
        _availableEasyRooms = new List<int>();
        _availableHardRooms = new List<int>();
        Map = new List<GameObject>();

        //Se recorren el array de las salas fáciles y se añade el índice de cada sala en el Array 
        for (int i = 0; i < EasyRooms.Length; i++)
        {
            _availableEasyRooms.Add(i);
        }

        //Se recorren el array de las salas difíciles y se añade el índice de cada sala en el Array 
        for (int i = 0; i < HardRooms.Length; i++)
        {
            _availableHardRooms.Add(i);
        }

        RoomOffsetVector = new Vector3(RoomOffset, 0, 0);
        #endregion

        /*
         * SISTEMA DE GENERACIÓN DE SALAS:
         * Para cada Sala de forma individual:
         * 
         *  1º La Salas se instancia.
         *  2º Se añade la Sala al mapa.
         *  3º Se ajusta el BuilderTransform.
         *  
         *  (Los paso 2º y 3º son invertibles en su orden)
         *
         * Si existe más de una Sala dentro de una categoria: 
         * 
         *  1º Se va recorriendo todas las existentes una a una de su categoria. 
         *  2º En cada recorrido se elige una aleatoria. 
         *  3º Entonces el indice de esa sala se elimina de las posibles salas a generar. 
         *  4º Aplicamos el algoritmo individual de cada sala.
         *  5º Saltamos a la siguiente sala dentro de la categoria
         *  
         *  (Los paso 3º y 4º son invertibles en su orden)
         *  
         *  La categoria de las Salas esta comentada encima de cada una.
         */
        #region RoomGenerator

        //Tutorial
        Room = Instantiate(Tutorial, new Vector3(-1,2,0), Quaternion.identity);
        RoomPosition = RoomPosition + RoomOffsetVector;
        BuilderTransform.position = RoomPosition;
        Map.Add(Room);

        //Faciles
        for (int i = 0; i < EasyRooms.Length; i++)
        {
            randomRoom = Random.Range(0, _availableEasyRooms.Count);
            _currentRoom = _availableEasyRooms[randomRoom];
            Room = Instantiate(EasyRooms[_currentRoom], BuilderTransform.position, Quaternion.identity);
            Map.Add(Room);
            _availableEasyRooms.Remove(_currentRoom);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;

        }

        //Intermedia
        RoomOffsetVector = new Vector3(RoomOffset, 0, 0);
        Room = Instantiate(Intermedia, BuilderTransform.position, Quaternion.identity);
        Map.Add(Room);
        RoomPosition = RoomPosition + RoomOffsetVector;
        BuilderTransform.position = RoomPosition;

        //Dificiles
        for (int i = 0; i < HardRooms.Length; i++)
        {
            randomRoom = Random.Range(0, _availableHardRooms.Count);
            _currentRoom = _availableHardRooms[randomRoom];
            Room = Instantiate(HardRooms[_currentRoom], BuilderTransform.position, Quaternion.identity);
            Map.Add(Room);
            _availableHardRooms.Remove(_currentRoom);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;
        }
        //Boss
        Room = Instantiate(BossFight, BuilderTransform.position, Quaternion.identity);
        Map.Add(Room);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //Si hay jugador y su vida es igual o menor a 0 en algún momento activamos la escena de muerte.

        if (FrankMovement.Player != null && FrankMovement.Player.gameObject != null)
        {
            HealthComponent playerHealth = FrankMovement.Player.gameObject.GetComponent<HealthComponent>();

            if (playerHealth != null && playerHealth.Health <= 0)
            {
                MenuManager.Menu.Dead();
            }
        }
    }
}
