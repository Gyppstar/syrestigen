using UnityEngine;
using Oculus.Interaction;

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

    private GameObject spawnedCube;

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
}
