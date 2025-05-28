using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public TMP_Text scoreText;
    public Image[] hearts;

    [Header("Falling Speed Control")]
    public float currentFallSpeed = 1f;
    public float maxFallSpeed = 5f;
    public float fallSpeedIncreaseRate = 0.1f;

    private int score = 0;
    private int health;

    void Awake()
    {
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

    void Update()
    {
        // Speed ramps up over time
        if (currentFallSpeed < maxFallSpeed)
        {
            currentFallSpeed += fallSpeedIncreaseRate * Time.deltaTime;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score updated to: " + score);
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Took damage. Health: " + health);
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
        Time.timeScale = 0f;
    }
}