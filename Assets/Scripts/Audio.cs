using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource effectAudioSource;
    [SerializeField] private AudioClip mouseupAudio;
    [SerializeField] private AudioClip placeAudio;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip score;

    // Khởi tạo và lấy tham chiếu AudioSource trên đối tượng
    void Awake()
    {
        effectAudioSource = GetComponent<AudioSource>();
    }

    // Phát âm thanh hiệu ứng khi người chơi chạm hoặc nhấc khối gạch lên
    public void PlayClickAudio()
    {
        if (effectAudioSource != null && mouseupAudio != null)
        {
            effectAudioSource.PlayOneShot(mouseupAudio);
        }
    }

    // Phát âm thanh hiệu ứng khi đặt khối gạch xuống bàn cờ thành công
    public void PlayPlaceAudio()
    {
        if (effectAudioSource != null && placeAudio != null)
        {
            effectAudioSource.PlayOneShot(placeAudio);
        }
    }

    // Phát âm thanh hiệu ứng khi trò chơi kết thúc (Game Over)
    public void PlayGameOver()
    {
        if (effectAudioSource != null && gameOver != null)
        {
            effectAudioSource.PlayOneShot(gameOver);
        }
    }

    // Phát âm thanh hiệu ứng khi người chơi hoàn thành và xóa hàng/cột
    public void PlayScore()
    {
        if (effectAudioSource != null && score != null)
        {
            effectAudioSource.PlayOneShot(score);
        }
    }
}

