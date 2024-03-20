using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private HealthComponent healthComponent;

    public GameObject[] EasyRooms;
    public GameObject[] HardRooms;
    private Vector3 RoomPosition;
    public float RoomOffset = 0f;
    private int randomRoom;
    private GameObject Room;
    public Transform BuilderTransform;
    private Vector3 RoomOffsetVector;
    public Slider vomitSlider;
    public LifeBarComponenet lifepickupbar;
    public List<int> _availableEasyRooms;
    public List<int> _availableHardRooms;
    
    int _currentRoom;
    void Awake()
    {
        RoomOffsetVector = new Vector3(RoomOffset, 0, 0);
        for (int i = 0; i < EasyRooms.Length; i++)
        {
            randomRoom = Random.Range(0, _availableEasyRooms.Count);
            _currentRoom = _availableEasyRooms[randomRoom];
            Room = Instantiate(EasyRooms[_currentRoom], BuilderTransform.position, Quaternion.identity);
            _availableEasyRooms.Remove(_currentRoom);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;
        }
        for (int i = 0; i < HardRooms.Length; i++)
        {
            randomRoom = Random.Range(0, _availableHardRooms.Count);
            _currentRoom = _availableHardRooms[randomRoom];
            Room = Instantiate(HardRooms[_currentRoom], BuilderTransform.position, Quaternion.identity);
            _availableHardRooms.Remove(_currentRoom);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (FrankMovement.Player.gameObject.GetComponent<HealthComponent>().Health <= 0) 
            MenuManager.Menu.Dead();
    }
}
