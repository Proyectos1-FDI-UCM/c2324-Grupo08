using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogmanager : MonoBehaviour
{
    [SerializeField] GameObject cajadedialogo;
    [SerializeField] Text dialogo;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;
    public static Dialogmanager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        cajadedialogo.SetActive(false);
    }

    CartelManager dialog;
    int currentLine=0;
    bool isWriting;
    public IEnumerator ShowDialog(CartelManager dialog)
    {
        yield return new WaitForEndOfFrame();
        dialogo.color = Color.green;
        OnShowDialog?.Invoke();
        this.dialog = dialog;
        cajadedialogo.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }
    public void HandleUpdate()
    {
        if (!isWriting)
        {
            currentLine++;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else 
            {
                currentLine = 0;
                cajadedialogo.SetActive(false);
                OnHideDialog?.Invoke();
            }
        }
    }
    public IEnumerator TypeDialog(string line)
    {
        isWriting = true;
        dialogo.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogo.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
            dialogo.color = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time,1));
        }
        isWriting = false;
        yield return new WaitForSeconds(1f);
        HandleUpdate();
        
    }
}
