using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Rooms;
    private Vector3 RoomPosition;
    public float RoomOffset = 0f;
    private int randomRoom;
    private GameObject Room;
    public Transform BuilderTransform;
    private Vector3 RoomOffsetVector;
    public Slider vomitSlider;
    public LifeBarComponenet lifepickupbar;
    public List<int> _availableRooms;
    int _currentRoom;
    void Awake()
    {

        RoomOffsetVector = new Vector3(RoomOffset, 0, 0);
        for (int i = 0; i < Rooms.Length; i++)
        {
            randomRoom = Random.Range(0, _availableRooms.Count);
            Debug.Log("Randomnumber:" + randomRoom);
            _currentRoom = _availableRooms[randomRoom];
            Debug.Log("Current:" + _currentRoom);
            Room = Instantiate(Rooms[_currentRoom], BuilderTransform.position, Quaternion.identity);
            _availableRooms.Remove(_currentRoom);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;
            Debug.Log("Vueltas");
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
