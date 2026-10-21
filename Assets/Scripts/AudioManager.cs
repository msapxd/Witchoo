using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(" ------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header(" ------- Audio Clip -------")]
    public AudioClip bg;
    public AudioClip witchWalk;
    public AudioClip catWalk;
    public AudioClip batFly;
    public AudioClip hurt;
    public AudioClip batShreik;
    public AudioClip witchWand;
    public AudioClip catMeow;
    public AudioClip potionPickUp;
    public AudioClip switchCharacter;


    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (Instance != this) return;

        musicSource.clip = bg;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void SwitchMusic(bool isOn)
    {
        musicSource.gameObject.SetActive(isOn);
    }
}

