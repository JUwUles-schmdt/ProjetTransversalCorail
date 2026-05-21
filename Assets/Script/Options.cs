using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.Mathematics.Geometry;
using System;
using Unity.Mathematics;
using TMPro;
using UnityEngine.SceneManagement;
public class Options : MonoBehaviour
{
    public GameObject menu;
    public AudioClip[] musics;
    public AudioSource audioSource;
    private int currentMusic=0;
    private float volume=0.7f;
    public TextMeshProUGUI textMeshPro;
    public static Options instance;
    public GameManager manager;
    public MainMenuManager mainMenuManager;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);



        if (musics != null && !audioSource.isPlaying)
        {
            currentMusic = UnityEngine.Random.Range(0, musics.Length); 
            audioSource.clip = musics[currentMusic];
            audioSource.Play();
        }
        textMeshPro.text = System.Math.Round(volume * 100).ToString();
            mainMenuManager = FindAnyObjectByType<MainMenuManager>();
            manager = FindAnyObjectByType<GameManager>();

        if (manager != null)
        {
            manager.boutonPause.GetComponent<Button>().onClick.AddListener(openMenu);
        }
        if (mainMenuManager != null)
        {
            mainMenuManager.boutonPause.GetComponent<Button>().onClick.AddListener(openMenu);
        }
    }

    void Update()
    {
        if (FindAnyObjectByType<GameManager>()&& !manager)
        {

            manager = FindAnyObjectByType<GameManager>();
            manager.boutonPause.GetComponent<Button>().onClick.AddListener(openMenu);
        }

        audioSource.volume = volume;
        if (!audioSource.isPlaying)
        {
            currentMusic = UnityEngine.Random.Range(0, musics.Length);
            audioSource.clip = musics[currentMusic];
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void openMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
    }



    public void closeMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        if (mainMenuManager!= null) mainMenuManager.mainMenuPanel.SetActive(true);
        
    }

    public void Volume()
    {
        volume = FindObjectOfType<Slider>().value;
        textMeshPro.text = System.Math.Round(volume*100).ToString();
    }
}
