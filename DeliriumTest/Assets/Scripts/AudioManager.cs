using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para garantizar acceso global
    static private AudioManager instance;
    public static AudioManager Instance
    {get { return instance;}}

    //Player Sounds
    [SerializeField] private AudioClip _punch;
    [SerializeField] private AudioClip[] _dash;

    // Object Sounds
    [SerializeField] private AudioClip _patataPickup;
    [SerializeField] private AudioClip _botellaPickup;

    // Enemy Sounds
    [SerializeField] private AudioClip[] _ratSounds;
    [SerializeField] private AudioClip[] _trashSounds;

    // Audio Sources
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource PickUpSFX;
    [SerializeField] private AudioSource PlayerSFX;
    [SerializeField] private AudioSource EnemySFX;


    void Start()
    {
        
    }

    private void Awake()
    {
        //Parte del patrón Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Se disponen los diferentes efectos de sonido y se emiten
    public void Punch()
    {
        PlayerSFX.clip = _punch; 
        PlayerSFX.Play();
    }

    public void Dash()
    {
        int n = Random.Range(0, _dash.Length);
        PlayerSFX.clip = _dash[n];
        PlayerSFX.Play();
    }
    public void PatataPickup()
    {
        PickUpSFX.clip = _patataPickup; PickUpSFX.Play();
    }

    public void AguaPickup()
    {
        PickUpSFX.clip = _botellaPickup; PickUpSFX.Play();
    }

    public void RatSound()
    {
        int n = Random.Range(0, _ratSounds.Length);
        EnemySFX.clip = _ratSounds[n];
        EnemySFX.Play();
    }
    public void TrashSound()
    {
        int n = Random.Range(0, _trashSounds.Length);
        EnemySFX.clip = _trashSounds[n];
        EnemySFX.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
