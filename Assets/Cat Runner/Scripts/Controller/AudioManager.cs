using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip backgroundMusic;
    public AudioClip pickCoinSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip magnetSound;
    public AudioClip unlockSound;
    public AudioClip playBGSound;
    public AudioClip buttonClickSound;

    private AudioSource musicSource;
    private AudioSource soundSource;
    private AudioManagerUI audioManagerUI;

    public enum BackgroundMusicType
    {
        Menu, 
        Play 
    }

    private BackgroundMusicType currentBackgroundMusicType = BackgroundMusicType.Menu;

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

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        soundSource = gameObject.AddComponent<AudioSource>();

        SetBackgroundMusicType(BackgroundMusicType.Menu);
    }

    private void Update()
    {
        UpdateAudioStates();
    }

    private void UpdateAudioStates()
    {
        bool isMusicOn = IsMusicOn();
        bool isSoundOn = IsSoundOn();

        if (isMusicOn)
        {
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            if (musicSource.isPlaying)
            {
                musicSource.Pause();
            }
        }

        if (isSoundOn)
        {
            if (!soundSource.isPlaying)
            {
                soundSource.UnPause();
            }
        }
        else
        {
            if (soundSource.isPlaying)
            {
                soundSource.Pause();
            }
        }
    }

    public void SetBackgroundMusicType(BackgroundMusicType bgType)
    {
        currentBackgroundMusicType = bgType;

        if (currentBackgroundMusicType == BackgroundMusicType.Menu)
        {
            musicSource.clip = backgroundMusic;
        }
        else if (currentBackgroundMusicType == BackgroundMusicType.Play)
        {
            musicSource.clip = playBGSound;
        }

        musicSource.Play();
        musicSource.loop = true;
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayButtonClickSound()
    {
        soundSource.clip = buttonClickSound;
        soundSource.Play();
    }

    public void PlayPickCoinSound()
    {
        soundSource.clip = pickCoinSound;
        soundSource.Play();
    }

    public void JumpSound()
    {
        soundSource.clip = jumpSound;
        soundSource.Play();
    }

    public void DeathSound()
    {
        soundSource.clip = deathSound;
        soundSource.Play();
    }

    public void MagnetSound()
    {
        soundSource.clip = magnetSound;
        soundSource.Play();
    }

    public void UnlockSound()
    {
        soundSource.clip = unlockSound;
        soundSource.Play();
    }

    public bool IsMusicOn()
    {
        return PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
    }

    public bool IsSoundOn()
    {
        return PlayerPrefs.GetInt("IsSoundOn", 1) == 1;
    }
}
