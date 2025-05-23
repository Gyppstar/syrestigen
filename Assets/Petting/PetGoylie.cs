using UnityEngine;
using Oculus.Haptics; // Meta XR Haptics

public class PetGoylie : MonoBehaviour
{
    [Header("Responskomponenter")]
    public AudioSource purrAudio;
    public HapticSource haptics; // Meta XR Haptics komponent
    public GameObject heartParticles;

    [Header("Inställningar")]
    public float activationTime = 1.5f; // Tid innan reaktion

    private float petTimer = 0f;
    private bool isBeingPetted = false;

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
        }
    }

    private void ActivateGoylieResponse()
    {
        isBeingPetted = true;
        Debug.Log("?? Goylie reagerar på klapp!");

        if (purrAudio != null && !purrAudio.isPlaying)
            purrAudio.Play();

        if (haptics != null)
            haptics.Play(); // Ingen IsPlaying – Play() funkar oavsett

        if (heartParticles != null)
            heartParticles.SetActive(true);
    }
}
