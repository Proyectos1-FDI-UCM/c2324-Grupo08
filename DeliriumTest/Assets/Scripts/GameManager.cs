using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {

        RoomOffsetVector = new Vector3(RoomOffset, 0, 0);
        for (int i = 0; i < Rooms.Length; i++)
        {
            randomRoom = Random.Range(0, Rooms.Length);
            Room = Instantiate(Rooms[0], BuilderTransform.position, Quaternion.identity);
            Room.GetComponentInChildren<ThrowUpPickUP>().RegisterVomit(vomitSlider);
            Room.GetComponentInChildren<HealthPickUP>().RegisterLifeBar(lifepickupbar);
            RoomPosition = RoomPosition + RoomOffsetVector;
            BuilderTransform.position = RoomPosition;
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
