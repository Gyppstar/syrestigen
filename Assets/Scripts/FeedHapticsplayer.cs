using Oculus.Haptics;
using UnityEngine;

public class FeedHapticsPlayer : MonoBehaviour
{
    [SerializeField] private HapticClip hapticClip;
    [SerializeField] private AudioSource hapticAudioSource;
    [SerializeField] private AudioClip hapticAudio;

    [SerializeField] private bool leftController;
    [SerializeField] private bool rightController;

    private HapticClipPlayer hapticPlayer;

    private void Awake()
    {
        if (hapticClip == null)
            Debug.LogError("Feed haptic clip not assigned!");

        hapticPlayer = new HapticClipPlayer(hapticClip);
    }

    public void PlayHaptics()
    {
        if (hapticAudioSource != null && hapticAudio != null)
        {
            hapticAudioSource.clip = hapticAudio;
            hapticAudioSource.Play();
        }

        if (!leftController && !rightController)
            hapticPlayer.Play(Controller.Both);
        else if (leftController)
            hapticPlayer.Play(Controller.Left);
        else if (rightController)
            hapticPlayer.Play(Controller.Right);
    }

    private void OnDestroy()
    {
        hapticPlayer?.Dispose();
    }
}
