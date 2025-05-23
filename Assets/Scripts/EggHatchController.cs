using UnityEngine;

public class EggHatchController : MonoBehaviour
{
    public GameObject eggWhole;
    public GameObject eggBrokenRoot;
    public GameObject dragon;
    public ParticleSystem particles;

    public float shakeDuration = 2f;
    public float shakeIntensity = 0.005f;
    public float delayBeforeHatch = 1.5f;
    public float timeBeforeShowDragon = 1.0f;         // ⬅️ Tidigare visning av draken
    public float timeBeforeRemovePieces = 3.0f;

    private Vector3 originalPos;
    private AudioSource audioSource;

    void Start()
    {
        originalPos = eggWhole.transform.position;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(HatchSequence());
    }

    System.Collections.IEnumerator HatchSequence()
    {
        yield return new WaitForSeconds(delayBeforeHatch);

        // Spela ljud (om det finns)
        if (audioSource != null)
            audioSource.Play();

        // Skaka ägget
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity;
            eggWhole.transform.position = originalPos + offset;
            elapsed += Time.deltaTime;
            yield return null;
        }

        eggWhole.transform.position = originalPos;

        // Spela partiklar
        if (particles != null)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
        }

        yield return new WaitForSeconds(0.5f);

        // Visa skärvor
        eggWhole.SetActive(false);
        eggBrokenRoot.SetActive(true);

        // Mycket mjukare sprick-kraft
        foreach (Transform piece in eggBrokenRoot.transform)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                // Mjuk glidning + lätt fall
                Vector3 drift = new Vector3(
                    Random.Range(-0.05f, 0.05f),
                    -0.1f,
                    Random.Range(-0.05f, 0.05f)
                );
                rb.AddForce(drift * 0.2f, ForceMode.Impulse);        // ⬅️ Mycket mild kraft
                rb.AddTorque(Random.insideUnitSphere * 0.1f, ForceMode.Impulse);  // ⬅️ Mjuk rotation
            }
        }

        // Visa draken tidigare
        yield return new WaitForSeconds(timeBeforeShowDragon);
        dragon.SetActive(true);

        // Låt partiklarna tonas ut
        if (particles != null)
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        // Skärvor försvinner efter en stund
        yield return new WaitForSeconds(timeBeforeRemovePieces);
        eggBrokenRoot.SetActive(false);
    }
}