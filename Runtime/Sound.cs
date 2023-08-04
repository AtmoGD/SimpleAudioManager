using UnityEngine.Audio;
using UnityEngine;

namespace AudioManagement
{
    public enum SoundType
    {
        Music,
        SFX
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public SoundType type;
        [Range(0.0f, 1.0f)] public float volume = 1.0f;
        [Range(0.1f, 3.0f)] public float pitch = 1.0f;
        public bool loop = false;
        public bool playOnAwake = true;
        [HideInInspector] public AudioSource source;
    }

}