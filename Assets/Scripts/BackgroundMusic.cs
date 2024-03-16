using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] playlist;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }
    void PlayRandomSong()
    {
        AudioClip randomClip = playlist[Random.Range(0, playlist.Length)];
        audioSource.clip = randomClip;
        audioSource.Play();
        Invoke(nameof(PlayRandomSong), randomClip.length);
    }
}