using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Blocks blocks;
    [SerializeField] private TMP_Text scoreCurrentText;
    [SerializeField] private TMP_Text scoreMaxText;
    [SerializeField] private GameObject gameOverPanel;
    private Audio Audio;

    private int currentScore = 0;
    private int maxScore = 0;
    private bool isGameOver = false;

    // Khởi tạo và tìm đối tượng Audio trong màn chơi
    void Awake()
    {
        Audio = FindAnyObjectByType<Audio>();
    }

    // Khởi tạo điểm số ban đầu, tải kỷ lục từ PlayerPrefs và ẩn panel Game Over
    void Start()
    {
        currentScore = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        isGameOver = false;

        scoreCurrentText.text = "0";
        scoreMaxText.text = maxScore.ToString();
        gameOverPanel.SetActive(false);
    }

    // Kiểm tra điều kiện thua cuộc mỗi frame nếu người chơi không còn chỗ đặt khối
    void Update()
    {
        if (!isGameOver && blocks != null && blocks.IsGameOver())
        {
            GameOver();
        }
    }

    // Cộng điểm khi đặt khối xuống bàn cờ (mỗi ô của khối tương ứng 1 điểm)
    public void AddPlacementScore(int score)
    {
        currentScore += score;
        scoreCurrentText.text = currentScore.ToString();
        if (currentScore > maxScore)
        {
            maxScore = currentScore;
            scoreMaxText.text = maxScore.ToString();
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
    }

    // Cộng điểm khi xóa hàng hoặc cột, cập nhật UI, lưu điểm cao và phát âm thanh ghi điểm
    public void AddScore(int score)
    {
        currentScore += score;
        scoreCurrentText.text = currentScore.ToString();
        if (currentScore > maxScore)
        {
            maxScore = currentScore;
            scoreMaxText.text = maxScore.ToString();
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }
        if (Audio != null)
        {
            Audio.PlayScore();
        }
    }

    // Xử lý khi kết thúc trò chơi, phát âm thanh Game Over và hiện giao diện thông báo
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (Audio != null)
        {
            Audio.PlayGameOver();
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Khởi động lại trò chơi bằng cách tải lại Scene hiện tại
    public void RestartGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        SceneManager.LoadScene("GameScence");
    }
}