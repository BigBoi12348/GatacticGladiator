using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerAttack, DashEffect, EnemyDeath, Hammersmash, SwordHit, ArrowShoot, EnemyStep
    }

    public static SoundManager Instance;
    private static Dictionary<Sound, float> soundTimerDictionary;
    private static UnityEngine.GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    [Header("Sounds")]
    public SoundAudioClip[] Sounds;

    private void Awake() 
    {
        if(Instance != null)
        {
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
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            
            if(sound == Sound.PlayerAttack)
            {
                audioSource.volume = (0.5f);
                audioSource.pitch = Random.Range(0.95f, 1.1f);
                //audioSource.panStereo = Random.Range(0.9f, 1.1f); 
                audioSource.reverbZoneMix = Random.Range(0.9f, 1.05f);
            }
            if(sound == Sound.DashEffect)
            {
                audioSource.volume = (0.2f);
            }
            if(sound == Sound.EnemyDeath)
            {
                audioSource.volume = (0.3f);
            }
            if(sound == Sound.EnemyStep)
            {
                audioSource.pitch = Random.Range(0.95f, 1.1f);
                audioSource.volume = (0.2f);
            }
            audioSource.Play();

            Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            // case Sound.EnemyStep:
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
            default:
                return true;
        }
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in Sounds)
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
