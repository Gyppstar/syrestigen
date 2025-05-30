using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Gameplay")]
    public int score = 0;
    public int lives = 3;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject[] hearts;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
        gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void TakeDamage(int amount)
    {
        lives -= amount;

        if (lives >= 0 && lives < hearts.Length)
        {
            hearts[lives].SetActive(false);
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        finalScoreText.text = "Poäng: " + score;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar spelet
    }

    // Kallas av knappen "Spela igen"
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Kallas av knappen "Till meny"
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // <- byt till ditt meny-scen-namn
    }
}