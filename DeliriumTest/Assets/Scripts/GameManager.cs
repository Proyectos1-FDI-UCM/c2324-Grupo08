using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] EasyRooms;
    public GameObject[] HardRooms;
    private Vector3 RoomPosition;
    public float RoomOffset = 0f;
    private int randomRoom;
    private GameObject Room;
    public Transform BuilderTransform;
    private Vector3 RoomOffsetVector;
    /// <summary>
    /// Lista que guarda las salas fáciles que todavia no se han generado
    /// </summary>
    public static List<int> _availableEasyRooms;
    /// <summary>
    /// Lista que guarda las salas dificiles que todavia no se han generado
    /// </summary>
    public static List<int> _availableHardRooms;
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

    //Entero que guarda el indice de la ultima sala generada
    int _currentRoom;

    //Sala en la que se encuentra el jugador
    public static int ActiveRoom;

    //Lista que contiene el orden del salas 
    public List<GameObject> Map;

    void Awake()
    {
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
        Room = Instantiate(Intermedia, new Vector3(63, 2, 0), Quaternion.identity);
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
    }
    // Update is called once per frame
    void Update()
    {
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
