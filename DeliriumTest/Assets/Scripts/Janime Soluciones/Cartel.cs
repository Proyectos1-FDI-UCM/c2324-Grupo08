using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelManager : MonoBehaviour, interactables
{
    [SerializeField] List<string> lines;
    [SerializeField] Sprite sign;
    [SerializeField] Sprite activeSign;
    SpriteRenderer spriteRenderer;

    public List<string> Lines
    {
        get { return lines; }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sign;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<FrankMovement>() != null) 
        {
            spriteRenderer.sprite = activeSign;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FrankMovement>() != null)
        {
            spriteRenderer.sprite = sign;
        }
    }
    public void interact() 
    {
        StartCoroutine(Dialogmanager.Instance.ShowDialog(this));
        StartCoroutine(LevelManager.levelManager.Go());
    }
}
