using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="New Object", menuName ="ObjectoRecojible")]
public class ObjetoRecojible : ScriptableObject
{
    [SerializeField] private string objectName;
    [SerializeField] private int objectID;
    
    public string ObjectName { get { return objectName; } }
    public int ObjectID { get {  return objectID; } }

}
