using UnityEngine;
using UnityEngine.Audio;

public class BGM_Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip BGM;

    void Start()
    {
        audioSource.clip = BGM;
        audioSource.loop = true;
        audioSource.Play();
    }
}
