using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepController : MonoBehaviour
{
    public GameObject sleepPromptVisual;
    public ParticleSystem sleepParticles;
    public Animator goylieAnimator;

    private bool isReady = false;

    public void ShowSleepPrompt()
    {
        isReady = true;
        sleepPromptVisual.SetActive(true);
    }

    void Update()
    {
        if (isReady)
        {
            // Meta Quest vänster X-knapp = primaryButton
            var leftHand = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
            if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool xPressed) && xPressed)
            {
                TriggerSleep();
                isReady = false;
            }
        }
    }

    public void TriggerSleep()
    {
        sleepPromptVisual.SetActive(false);
        if (goylieAnimator != null)
        {
            goylieAnimator.SetTrigger("Sleep");
        }

        if (sleepParticles != null)
        {
            sleepParticles.Play();
        }

        Invoke(nameof(ChangeScene), 3f); // Vänta 3 sekunder
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("DreamScene");
    }
}
