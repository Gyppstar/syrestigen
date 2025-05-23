using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FoodManager : MonoBehaviour
{
    [Header("Mättnadsinställningar")]
    public int maxFeedLevel = 5;
    public int currentFeedLevel = 0;
    public AudioSource feedSound;

    [Header("UI - HungerBar")]
    public Image foodBarImage;
    public Sprite emptySprite;
    public Sprite lowSprite;
    public Sprite mediumSprite;
    public Sprite fullSprite;

    [Header("Event (valfritt)")]
    public UnityEvent<float> OnHungerChanged;

    [Header("Haptics")]
    public FeedHapticsPlayer hapticsPlayer;

    [Header("Animation")]
    public Animator animator;  // ← Add this
    private int eatingLayerIndex;

    public bool IsFull => currentFeedLevel >= maxFeedLevel;

    private void Start()
    {
        currentFeedLevel = 0;
        UpdateBar();

        if (animator != null)
        {
            eatingLayerIndex = animator.GetLayerIndex("Eating");
            animator.SetLayerWeight(eatingLayerIndex, 0f);
        }
    }

    public void FeedGoylie()
    {
        if (IsFull)
        {
            Debug.Log("🟡 Goylie är redan mätt.");
            return;
        }

        currentFeedLevel++;
        Debug.Log($"🍎 Goylie matad: {currentFeedLevel}/{maxFeedLevel}");

        if (feedSound != null) feedSound.Play();
        if (hapticsPlayer != null) hapticsPlayer.PlayHaptics();

        UpdateBar();
        OnHungerChanged?.Invoke((float)currentFeedLevel / maxFeedLevel);

        // ✅ Animate eating
        if (animator != null)
        {
            animator.SetBool("IsEating", true);
            animator.SetLayerWeight(eatingLayerIndex, 1f);

            // Optional: Reset after 2 seconds
            Invoke(nameof(ResetEatingAnimation), 2f);
        }

        if (IsFull)
        {
            Debug.Log("✅ Goylie är nu mätt!");
        }
    }

    private void ResetEatingAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsEating", false);
            animator.SetLayerWeight(eatingLayerIndex, 0f);
        }
    }

    private void UpdateBar()
    {
        if (foodBarImage == null) return;

        if (currentFeedLevel >= maxFeedLevel)
            foodBarImage.sprite = fullSprite;
        else if (currentFeedLevel >= maxFeedLevel - 2)
            foodBarImage.sprite = mediumSprite;
        else if (currentFeedLevel >= 1)
            foodBarImage.sprite = lowSprite;
        else
            foodBarImage.sprite = emptySprite;
    }
}
