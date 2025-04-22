using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

                       
    // public AudioSource musicPlayer;
    // public AudioSource sfxPlayer;
    // public AudioSource guardPlayer;
    
    // public AudioClip menuMusic;
    // public List<AudioClip> levelMusic;
    // public List<AudioClip> mcSfx;
    // public List<AudioClip> guardSfx;
    // public List<AudioClip> kompisSfx;
    // public List<AudioClip> ambience;

//     SoundManager SM;
//   //  PlayerManager PM;
    

//     public GameObject menuCanvas;

//     public Slider volumeSlider;
//     public static float volume;
   // [SerializeField] static SoundManager mcSound = null; //Scripts kan hämta funktioner från SoundManager

    public float lowPitchRange = 0.55f;
    public float highPitchRange = 1.55f;         //Använd för ljudeffekter?


    void OnEnable()
    {

        // SceneManager.sceneLoaded += OnSceneLoaded;
        Console.WriteLine("Scene loaded");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // SceneManager.sceneLoaded += OnSceneLoaded;


        // Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = "tmp";
        if (sceneName == ("MainMenu"))

        {
            // musicPlayer.clip = menuMusic;
            // menuCanvas.SetActive(true);
            Console.WriteLine("MainMenu loaded");

        }


        else if (sceneName == ("Scene1"))
        {

            // musicPlayer.clip = levelMusic[1];
            // menuCanvas.SetActive(false);
            Console.WriteLine("Scene1 loaded");
        }
        // musicPlayer.Play();
        Console.WriteLine("Scene loaded");
        

    }

    void Start()
    {


        bool SM = true;
        if (SM == null)

            // SM = this;
            Console.WriteLine("SM = this");

        else if (SM != this)
            // Destroy(gameObject);
            Console.WriteLine("Destroy(gameObject)");

        // DontDestroyOnLoad(gameObject);

        // musicPlayer.volume = volumeSlider.value;
        Console.WriteLine("musicPlayer.volume = volumeSlider.value");

       // PM = new PlayerManager();
        // PM.controller = GetComponent<CharacterController>();
    }

    /*
    public void sfxShuffle(params AudioClip[] clips)
    {
        int shuffleIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfx.pitch = randomPitch;                                            //Beep boop robot speak
        sfx.clip = clips[shuffleIndex];
        sfx.Play();
    }
    */
    /*  
      public void Update()
      {
          if ()
          if (PM.controller.isGrounded == true && PM.controller.velocity.magnitude > 6 && sfxPlayer.isPlaying == false)
          {
              sfxPlayer.clip = mcSfx[0];
              sfxPlayer.pitch = Random.Range(lowPitchRange, highPitchRange);
              sfxPlayer.volume = Random.Range(lowPitchRange, highPitchRange);
              sfxPlayer.Play();

          }

      }
      */
    public void VolumeSetting()
    {
        // musicPlayer.volume = volumeSlider.value;
        Console.WriteLine("musicPlayer.volume = volumeSlider.value");
    }

 
}
