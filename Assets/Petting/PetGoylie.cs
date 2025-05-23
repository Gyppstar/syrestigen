using UnityEngine;
using Oculus.Haptics;

public class PetGoylie : MonoBehaviour
{
    [Header("Responskomponenter")]
    public AudioSource purrAudio;
    public HapticSource haptics;
    public GameObject heartParticles;
    public Animator animator;  // ← Add this

    [Header("Inställningar")]
    public float activationTime = 1.5f;

    private float petTimer = 0f;
    private bool isBeingPetted = false;
    private int pettingLayerIndex;

    private void Start()
    {
        if (animator != null)
        {
            pettingLayerIndex = animator.GetLayerIndex("Petting");
            animator.SetLayerWeight(pettingLayerIndex, 0f); // Make sure it doesn't play on start
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") || other.name.Contains("Hand"))
        {
            petTimer += Time.deltaTime;

            if (!isBeingPetted && petTimer >= activationTime)
            {
                ActivateGoylieResponse();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") || other.name.Contains("Hand"))
        {
            petTimer = 0f;
            isBeingPetted = false;

            if (purrAudio != null) purrAudio.Stop();
            if (heartParticles != null) heartParticles.SetActive(false);
            if (haptics != null) haptics.Stop();

            if (animator != null)
            {
                animator.SetBool("IsPetting", false);
                animator.SetLayerWeight(pettingLayerIndex, 0f);
            }
        }
    }

    private void ActivateGoylieResponse()
    {
        isBeingPetted = true;
        Debug.Log("🐉 Goylie reagerar på klapp!");

        if (purrAudio != null && !purrAudio.isPlaying)
            purrAudio.Play();

        if (haptics != null)
            haptics.Play();

        if (heartParticles != null)
            heartParticles.SetActive(true);

        if (animator != null)
        {
            animator.SetBool("IsPetting", true);
            animator.SetLayerWeight(pettingLayerIndex, 1f);
        }
    }
}
