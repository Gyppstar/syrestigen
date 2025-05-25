using UnityEngine;
using Oculus.Interaction;
using System.Collections;

public class SpawnOnPinchPose : MonoBehaviour
{
    [Header("Detection via ISelector")]
    [SerializeField, Interface(typeof(ISelector))]
    private UnityEngine.Object _selector;

    private ISelector selector;

    [Header("Prefab & Position")]
    public GameObject cubePrefab;
    public Transform pinchPoint;

    [Header("Matlogik")]
    public FoodManager foodManager;

    [Header("Ljud")]
    public AudioSource deniedSound;
    public AudioSource guideAudioSource;     // ?? Ljudspelare för guide
    public AudioClip guidePopClip;           // ?? Pop-ljudet

    [Header("Guide-inställningar")]
    public GameObject guideImage;
    public float guideDelay = 5f;
    public float hideDelayAfterGesture = 1f;

    private GameObject spawnedCube;
    private bool guideShown = false;

    private void Awake()
    {
        selector = _selector as ISelector;
    }

    private void Start()
    {
        if (selector == null)
        {
            Debug.LogError("? ISelector inte kopplad – dra in t.ex. ActiveStateSelector");
            return;
        }

        if (cubePrefab == null || pinchPoint == null)
        {
            Debug.LogError("? Saknar prefab eller pinchPoint");
            return;
        }

        selector.WhenSelected += OnSelected;

        if (guideImage != null)
        {
            guideImage.SetActive(false);
            Invoke(nameof(ShowGuide), guideDelay);
        }

        Debug.Log("? SpawnOnSelector redo!");
    }

    private void OnDestroy()
    {
        if (selector != null)
        {
            selector.WhenSelected -= OnSelected;
        }
    }

    private void OnSelected()
    {
        if (guideImage != null && guideImage.activeSelf && guideShown)
        {
            StartCoroutine(HideGuideAfterDelay(hideDelayAfterGesture));
        }

        if (foodManager != null && foodManager.IsFull)
        {
            Debug.Log("? Goylie är mätt – kan inte spawna fler frukter.");
            if (deniedSound != null)
            {
                deniedSound.Play();
            }
            return;
        }

        if (spawnedCube != null)
        {
            Destroy(spawnedCube);
            spawnedCube = null;
            Debug.Log("??? Tog bort frukten.");
        }
        else
        {
            spawnedCube = Instantiate(cubePrefab, pinchPoint.position, pinchPoint.rotation);
            spawnedCube.transform.SetParent(pinchPoint, worldPositionStays: false);
            spawnedCube.transform.localPosition = Vector3.zero;
            Debug.Log("? Spawnade ny frukt!");
        }
    }

    private void ShowGuide()
    {
        if (!guideShown && guideImage != null)
        {
            guideImage.SetActive(true);
            guideShown = true;

            if (guideAudioSource != null && guidePopClip != null)
            {
                guideAudioSource.PlayOneShot(guidePopClip);  // ?? Spela popup-ljudet
                Debug.Log("?? Pop-ljud spelas via PlayOneShot.");
            }

            Debug.Log("? Guidebild visad.");
        }
    }

    private IEnumerator HideGuideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (guideImage != null)
        {
            guideImage.SetActive(false);
            guideShown = false;
            Debug.Log("? Guidebild dold efter fördröjning.");
        }
    }
}
