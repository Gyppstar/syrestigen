using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus.Haptics;

public class SleepController : MonoBehaviour
{
    [Header("Visuals & Effects")]
    public GameObject sleepPromptVisual;
    public ParticleSystem sleepParticles;

    [Header("Audio & Haptics")]
    public HapticSource haptics;
    public AudioSource sleepAudio;

    [Header("Animator Settings")]
    public Animator goylieAnimator;
    public string sleepLayerName = "Sleep";
    public string sleepBoolName = "IsSleeping";

    private bool isReady = false;

    public void ShowSleepPrompt()
    {
        isReady = true;
        if (sleepPromptVisual != null)
            sleepPromptVisual.SetActive(true);
    }

    void Update()
    {
        if (!isReady) return;

        var leftHand = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool xPressed) && xPressed)
        {
            TriggerSleep();
            isReady = false;
        }
    }

    public void TriggerSleep()
    {
        if (sleepPromptVisual != null)
            sleepPromptVisual.SetActive(false);

        // Set animation boolean and layer weight
        if (goylieAnimator != null)
        {
            int sleepLayerIndex = goylieAnimator.GetLayerIndex(sleepLayerName);
            if (sleepLayerIndex >= 0)
                goylieAnimator.SetLayerWeight(sleepLayerIndex, 1f);

            goylieAnimator.SetBool(sleepBoolName, true);
        }

        // Play haptics
        if (haptics != null)
            haptics.Play();

        // Play audio
        if (sleepAudio != null && !sleepAudio.isPlaying)
            sleepAudio.Play();

        // Play particles
        if (sleepParticles != null)
            sleepParticles.Play();

        Invoke(nameof(ChangeScene), 5f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("VRscene");
    }
}
