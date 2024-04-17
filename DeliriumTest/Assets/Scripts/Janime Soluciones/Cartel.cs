using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelManager : MonoBehaviour, interactables
{
    [SerializeField] List<string> lines;

    public List<string> Lines
    {
        get { return lines; }
    }
    public void interact() 
    {
        StartCoroutine(Dialogmanager.Instance.ShowDialog(this));
        Debug.Log("Interactuo");
    }
}
