using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static private AudioManager instance;
    public static AudioManager Instance
    {get { return instance;}}
    [SerializeField] private AudioClip punch;
    [SerializeField] private AudioClip patataPickup;
    [SerializeField] private AudioClip botellaPickup;
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource SFX;
    

    void Start()
    {
        
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Punch()
    {
        SFX.clip = punch; SFX.Play();
    }

    public void PatataPickup()
    {
        SFX.clip = patataPickup; SFX.Play();
    }

    public void AguaPickup()
    {
        SFX.clip = botellaPickup; SFX.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
