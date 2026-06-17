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
    public void AddScore(int score)
    {
        int currentScore = int.Parse(scoreCurrentText.text);
        currentScore += score;
        scoreCurrentText.text = currentScore.ToString();
        int maxScore = int.Parse(scoreMaxText.text);
        if (currentScore > maxScore)
        {
            scoreMaxText.text = currentScore.ToString();
            PlayerPrefs.SetInt("MaxScore", currentScore);
        }
        Audio.PlayScore();
    }
    public void GameOver()
    {
        Audio.PlayGameOver();
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("GameScence");
    }
    void Start()
    {
        scoreCurrentText.text = "0";
        scoreMaxText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();
        gameOverPanel.SetActive(false);
    }
    void Update()
    {
        if (blocks.IsGameOver())
        {
            this.GameOver();
        }
    }
    void Awake()
    {
        Audio = FindAnyObjectByType<Audio>();
    }
}