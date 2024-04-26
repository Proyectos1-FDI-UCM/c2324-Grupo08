using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton para garantizar acceso global
    static private AudioManager instance;
    public static AudioManager Instance
    { get { return instance; } }

    /// <summary>
    /// Pistas de Música
    /// </summary>
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    /// <summary>
    /// Efectos de Sonido del Jugador
    /// </summary>
    [SerializeField] private AudioClip _punch;
    [SerializeField] private AudioClip[] _dash;
    [SerializeField] private AudioClip _vomit;


    /// <summary>
    /// Efectos de Sonido de los Pick-Ups
    /// </summary>>
    [SerializeField] private AudioClip _patataPickup;
    [SerializeField] private AudioClip _botellaPickup;


    /// <summary>
    /// Efectos de Sonido del Enemigo
    /// </summary>
    [SerializeField] private AudioClip[] _ratSounds;
    [SerializeField] private AudioClip[] _trashSounds;

    /// <summary>
    /// Fuente de Sonido de la Música
    /// </summary>
    [SerializeField] private AudioSource Music;

    /// <summary>
    /// Fuente de Sonido de los Pick-Ups
    /// </summary>
    [SerializeField] private AudioSource PickUpSFX;

    /// <summary>
    /// Fuente de Sonido del Jugador
    /// </summary>
    [SerializeField] private AudioSource PlayerSFX;

    /// <summary>
    /// Fuente de Sonido del Enemigo
    /// </summary>
    [SerializeField] private AudioSource EnemySFX;


    void Start()
    {

    }

    private void Awake()
    {
        /// <summary>
        /// Singleton para que no se destruya
        /// </summary>
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Se reproduce el sonido de puñetazo
    /// </summary>
    public void Punch()
    {
        PlayerSFX.clip = _punch;
        PlayerSFX.Play();
    }

    /// <summary>
    /// Se reproduce el sonido del Dash
    /// </summary>
    public void Dash()
    {
        int n = Random.Range(0, _dash.Length);
        PlayerSFX.clip = _dash[n];
        PlayerSFX.Play();
    }

    /// <summary>
    /// Se reproduce el sonido de las patatas
    /// </summary>
    public void PatataPickup()
    {
        PickUpSFX.clip = _patataPickup; PickUpSFX.Play();
    }

    /// <summary>
    /// Se reproduce el sonido de la botella de agua
    /// </summary>
    public void AguaPickup()
    {
        PickUpSFX.clip = _botellaPickup; PickUpSFX.Play();
    }

    /// <summary>
    /// Se reproduce uno de los efectos de sonido de la rata aleatoriamente
    /// </summary>
    public void RatSound()
    {
        int n = Random.Range(0, _ratSounds.Length);
        EnemySFX.clip = _ratSounds[n];
        EnemySFX.Play();
    }
    /// <summary>
    /// Se reproduce uno de los efectos de sonido de la basura aleatoriamente
    /// </summary>
    public void TrashSound()
    {
        int n = Random.Range(0, _trashSounds.Length);
        EnemySFX.clip = _trashSounds[n];
        EnemySFX.Play();
    }

    /// <summary>
    /// Se reproduce el efecto de sonido del vómito
    /// </summary>
    public void VomitSound()
    {
        PlayerSFX.clip = _vomit;
        PlayerSFX.Play();
    }
    /// <summary>
    /// Se reproduce la canción del Menú de Inicio
    /// </summary>
    public void MenuMusic()
    {
        Music.clip = _menuMusic;
        Music.Play();
    }

    /// <summary>
    /// Se reproduce la canción del nivel
    /// </summary>
    public void LevelMusic()
    {
        Music.clip = _gameMusic;
        Music.Play();
    }


}
