using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FoodManager : MonoBehaviour
{
    [Header("Mättnadsinställningar")]
    [Tooltip("Hur många frukter som krävs för att Goylie ska bli mätt")]
    public int maxFeedLevel = 5;

    [Tooltip("Antal frukter som Goylie ätit")]
    public int currentFeedLevel = 0;

    [Tooltip("Ljud när frukt matas")]
    public AudioSource feedSound;

    [Header("UI - HungerBar")]
    public Image foodBarImage;
    public Sprite emptySprite;   // 0/5
    public Sprite lowSprite;     // 1–2/5
    public Sprite mediumSprite;  // 3–4/5
    public Sprite fullSprite;    // 5/5

    [Header("Event (valfritt)")]
    public UnityEvent<float> OnHungerChanged;

    // Gör att andra scripts kan se om Goylie är mätt
    public bool IsFull => currentFeedLevel >= maxFeedLevel;

    private void Start()
    {
        currentFeedLevel = 0; // Startar hungrig
        UpdateBar();
    }

    public void FeedGoylie()
    {
        if (IsFull)
        {
            Debug.Log("? Goylie är redan mätt.");
            return;
        }

        currentFeedLevel++;
        Debug.Log($"?? Goylie matad: {currentFeedLevel}/{maxFeedLevel}");

        if (feedSound != null)
        {
            feedSound.Play();
        }

        UpdateBar();
        OnHungerChanged?.Invoke((float)currentFeedLevel / maxFeedLevel);

        if (IsFull)
        {
            Debug.Log("? Goylie är nu mätt!");
        }
    }

    private void UpdateBar()
    {
        if (foodBarImage == null)
        {
            Debug.LogWarning("?? Ingen FoodBar UI tilldelad.");
            return;
        }

        // Visa rätt sprite beroende på mättnadsnivå
        if (currentFeedLevel >= maxFeedLevel)
        {
            foodBarImage.sprite = fullSprite;
        }
        else if (currentFeedLevel >= maxFeedLevel - 2)
        {
            foodBarImage.sprite = mediumSprite;
        }
        else if (currentFeedLevel >= 1)
        {
            foodBarImage.sprite = lowSprite;
        }
        else
        {
            foodBarImage.sprite = emptySprite;
        }
    }
}
