using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] playlist;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    private void PlayRandomSong()
    {
        AudioClip randomClip = playlist[Random.Range(0, playlist.Length)];
        audioSource.clip = randomClip;
        audioSource.Play();
        Invoke(nameof(PlayRandomSong), randomClip.length);
    }
}