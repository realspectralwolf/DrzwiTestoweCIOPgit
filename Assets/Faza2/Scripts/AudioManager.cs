using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] List<AudioSound> audioClips = new();

    [System.Serializable]
    struct AudioSound
    {
        public string id;
        public AudioClip clip;
    }

    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string audioId)
    {
        m_AudioSource.clip = GetSound(audioId);
        m_AudioSource.Play();
    }

    AudioClip GetSound(string id)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            if (audioClips[i].id == id) return audioClips[i].clip;
        }
        return null;
    }
}
