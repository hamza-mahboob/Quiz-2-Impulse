using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip destoryed;
    public AudioClip hit;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void DestroyedSound()
    {
        audioSource.PlayOneShot(destoryed);
    }

    public void HitSound()
    {
        audioSource.PlayOneShot(hit);
    }
}
