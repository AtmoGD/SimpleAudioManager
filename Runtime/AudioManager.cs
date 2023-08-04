using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

/*
THANK YOU BRACKEYS
*/

namespace AudioManagement
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public bool singleton = true;
        public bool dontDestroyOnLoad = true;

        public float masterVolume = 1.0f;
        [Range(0.0f, 1.0f)] public float musicVolume = 1.0f;
        [Range(0.0f, 1.0f)] public float sfxVolume = 1.0f;

        public float masterPitch = 1.0f;
        [Range(0.1f, 3.0f)] public float musicPitch = 1.0f;
        [Range(0.1f, 3.0f)] public float sfxPitch = 1.0f;

        public List<Sound> sounds = new List<Sound>();

        void Awake()
        {
            if (singleton)
            {
                if (AudioManager.instance != null)
                {
                    Destroy(gameObject);
                    Debug.LogError("There can only be one AudioManager");
                    return;
                }
                else
                {
                    AudioManager.instance = this;
                }
            }

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);


            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
                if (sound.playOnAwake)
                    sound.source.Play();
            }

            UpdateAllVolume();
            UpdateAllPitch();
        }

        public void Play(string _name)
        {
            Sound sound = sounds.Find(sounds => sounds.name == _name);
            if (sound != null)
                sound.source.Play();
            else
                Debug.LogError("No sound with name " + _name + " exists.");
        }

        public void Stop(string _name)
        {
            Sound sound = sounds.Find(sounds => sounds.name == _name);
            if (sound != null)
                sound.source.Stop();
            else
                Debug.LogError("No sound with name " + _name + " exists.");
        }

        public void Pause(string _name)
        {
            Sound sound = sounds.Find(sounds => sounds.name == _name);
            if (sound != null)
                sound.source.Pause();
            else
                Debug.LogError("No sound with name " + _name + " exists.");
        }

        public void UpdateVolume(SoundType _type)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.type == _type)
                    sound.source.volume = sound.volume * masterVolume * (sound.type == SoundType.Music ? musicVolume : sfxVolume);
            }
        }

        public void UpdateMusicVolume()
        {
            UpdateVolume(SoundType.Music);
        }

        public void UpdateSFXVolume()
        {
            UpdateVolume(SoundType.SFX);
        }

        public void UpdateAllVolume()
        {
            UpdateMusicVolume();
            UpdateSFXVolume();
        }

        public void UpdatePitch(SoundType _type)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.type == _type)
                    sound.source.pitch = sound.pitch * masterPitch * (sound.type == SoundType.Music ? musicPitch : sfxPitch);
            }
        }

        public void UpdateMusicPitch()
        {
            UpdatePitch(SoundType.Music);
        }

        public void UpdateSFXPitch()
        {
            UpdatePitch(SoundType.SFX);
        }

        public void UpdateAllPitch()
        {
            UpdateMusicPitch();
            UpdateSFXPitch();
        }
    }
}