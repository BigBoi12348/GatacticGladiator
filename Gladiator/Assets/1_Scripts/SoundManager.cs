using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        Win, Lose, UIClickEnter, UIClickExit
    }

    public static SoundManager Instance;
    private static Dictionary<Sound, float> soundTimerDictionary;
    private static UnityEngine.GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    [Header("UI Sounds")]
    public SoundAudioClip[] UiSounds;

    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(this);
            DontDestroyOnLoad(this);
        }
        else
        {
            Instance = this;
            Init();
        }
    }

    private static void Init()
    {
        
        soundTimerDictionary = new Dictionary<Sound, float>();
        //soundTimerDictionary[Sound.play] = 0f;
    }

    public void PlaySound2D(Sound sound)
    {
        if(CanPlaySound(sound) && !SettingsData.SoundMuted)
        {
            if(oneShotGameObject == null)
            {
                oneShotGameObject = new UnityEngine.GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public void PlaySound3D(Sound sound, Vector3 position)
    {
        if(CanPlaySound(sound))
        {
            UnityEngine.GameObject soundGameObject = new UnityEngine.GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            
            audioSource.Play();

            Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            // case Sound.play:
            //     if(soundTimerDictionary.ContainsKey(sound))
            //     {
            //         float lastTimePlayed = soundTimerDictionary[sound];
            //         float playerMoveTimerMax = .05f;
            //         if(lastTimePlayed + playerMoveTimerMax < Time.time)
            //         {
            //             soundTimerDictionary[sound] = Time.time;
            //             return true;
            //         }
            //         else
            //         {
            //             return false;
            //         }
            //     }
            //     else
            //     {
            //         return true;
            //     }
        }
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in UiSounds)
        {
            if(soundAudioClip.SoundType == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

    //public static void AddButtonSounds(this)
    

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound SoundType;
        public AudioClip audioClip;
    }
}
