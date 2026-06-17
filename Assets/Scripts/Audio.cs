using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource effectAudioSource;
    [SerializeField] private AudioClip mouseupAudio;
    [SerializeField] private AudioClip placeAudio;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip score;

    public void PlayClickAudio()
    {
        effectAudioSource.PlayOneShot(mouseupAudio);
    }

    public void PlayPlaceAudio()
    {
        effectAudioSource.PlayOneShot(placeAudio);
    }
    public void PlayGameOver()
    {
        effectAudioSource.PlayOneShot(gameOver);
    }
    public void PlayScore()
    {
        effectAudioSource.PlayOneShot(score);
    }
    void Awake()
    {
        effectAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
