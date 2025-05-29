using UnityEngine;
using Oculus.Haptics;

public class EggHatchController : MonoBehaviour
{
    [Header("References")]
    public GameObject eggWhole;
    public GameObject eggBrokenRoot;
    public GameObject dragon;
    public ParticleSystem particles;
    public AudioSource shakeAudio;
    public HapticSource haptics;

    [Header("Timings")]
    public float shakeDuration = 2f;
    public float shakeIntensity = 0.005f;
    public float delayBeforeHatch = 1.5f;

    [Tooltip("Time to wait after hatch begins before triggering haptics and audio")]
    public float delayBeforeHapticsAndAudio = 0.5f;

    public float timeBeforeShowDragon = 1.0f;
    public float timeBeforeRemovePieces = 3.0f;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = eggWhole.transform.position;
        StartCoroutine(HatchSequence());
    }

    System.Collections.IEnumerator HatchSequence()
    {
        yield return new WaitForSeconds(delayBeforeHatch);

        yield return new WaitForSeconds(delayBeforeHapticsAndAudio);

        if (shakeAudio != null && !shakeAudio.isPlaying)
            shakeAudio.Play();

        if (haptics != null)
            haptics.Play();

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity;
            eggWhole.transform.position = originalPos + offset;
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shakeAudio != null)
            shakeAudio.Stop();

        if (haptics != null)
            haptics.Stop();

        eggWhole.transform.position = originalPos;

        if (particles != null)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
        }

        yield return new WaitForSeconds(0.5f);

        eggWhole.SetActive(false);
        eggBrokenRoot.SetActive(true);

        foreach (Transform piece in eggBrokenRoot.transform)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                Vector3 drift = new Vector3(
                    Random.Range(-0.05f, 0.05f),
                    -0.1f,
                    Random.Range(-0.05f, 0.05f)
                );
                rb.AddForce(drift * 0.2f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 0.1f, ForceMode.Impulse);
            }
        }

        yield return new WaitForSeconds(timeBeforeShowDragon);
        dragon.SetActive(true);

        if (particles != null)
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        yield return new WaitForSeconds(timeBeforeRemovePieces);
        eggBrokenRoot.SetActive(false);
    }
}
