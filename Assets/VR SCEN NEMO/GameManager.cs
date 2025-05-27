using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public TMP_Text scoreText;
    public Image[] hearts;

    private int score = 0;
    private int health;

    void Awake()
    {
        // Singleton reference
        if (instance == null)
        {
            instance = this;
            health = hearts.Length;
            UpdateUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score updated to: " + score); // Debug log
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Took damage. Health: " + health); // Debug log
        UpdateUI();

        if (health <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < health;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Freeze the game or trigger a game over screen
        Time.timeScale = 0f;
    }
}