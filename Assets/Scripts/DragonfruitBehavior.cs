using Oculus.Haptics;
using UnityEngine;

public class DragonfruitHaptic : MonoBehaviour
{
    [SerializeField] private AudioSource playbackAudioSource;
    [SerializeField] private AudioClip destructionAudioClip;
    [SerializeField] private HapticClip destructionHapticClip;

    [SerializeField] private bool useLeftController = false;
    [SerializeField] private bool useRightController = false;

    private HapticClipPlayer hapticPlayer;

    private void Awake()
    {
        if (destructionHapticClip == null)
            Debug.LogError("Destruction HapticClip not assigned!");

        // Initialize HapticClipPlayer with the destruction clip
        hapticPlayer = new HapticClipPlayer(destructionHapticClip);
    }

    private void OnDestroy()
    {
        if (hapticPlayer != null)
        {
            hapticPlayer.Dispose();
            hapticPlayer = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goylie"))
        {
            FoodManager manager = Object.FindFirstObjectByType<FoodManager>();
            if (manager != null)
            {
                manager.FeedGoylie();
            }

            PlayDestructionFeedback();

            Destroy(gameObject);
        }
    }

    private void PlayDestructionFeedback()
    {
        if (playbackAudioSource != null && destructionAudioClip != null)
        {
            playbackAudioSource.clip = destructionAudioClip;
            playbackAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip missing for destruction feedback.");
        }

        if (hapticPlayer != null)
        {
            if (!useLeftController && !useRightController)
                hapticPlayer.Play(Controller.Both);
            else if (useLeftController)
                hapticPlayer.Play(Controller.Left);
            else if (useRightController)
                hapticPlayer.Play(Controller.Right);
        }
        else
        {
            Debug.LogWarning("HapticClipPlayer is null.");
        }
    }
}
