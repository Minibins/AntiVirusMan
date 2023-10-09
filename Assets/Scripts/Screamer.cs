using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    [SerializeField] private AudioSource soundPlayer;
    public void scream(AudioClip sound)
    {
        soundPlayer.clip = sound;
        soundPlayer.Play();
    }
}
