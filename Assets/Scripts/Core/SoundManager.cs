using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }
    private AudioSource source;
    private AudioSource musicSource;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        //Keep this object even when we go to new scene
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        //Get and change initial sound volume
        float currentVolume = PlayerPrefs.GetFloat("soundVolume", 1);
        currentVolume += _change;

        //Check if we reach maximum or minimum value
        if(currentVolume > 1)
            currentVolume = 0;
        if(currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        source.volume = currentVolume;
    }

    public void ChangeMusicVolume(float _change)
    {
        float baseVolume = 0.2f;
        //Get and change initial sound volume
        float currentVolume = PlayerPrefs.GetFloat("musicVolume", 0.2f);
        currentVolume += _change;

        //Check if we reach maximum or minimum value
        if (currentVolume > baseVolume)
            currentVolume = 0;
        if (currentVolume < 0)
            currentVolume = baseVolume;

        //Assign final value
        musicSource.volume = currentVolume;
    }
}