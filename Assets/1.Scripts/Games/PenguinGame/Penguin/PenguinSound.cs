using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sounds;

    [SerializeField]
    private AudioSource audioSource;

    public void PlayRandomSound()
    {
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
