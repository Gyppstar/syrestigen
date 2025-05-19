using UnityEngine;
using Oculus.Interaction;

public class SpawnOnSelector : MonoBehaviour
{
    [Header("Detection via ISelector")]
    [SerializeField, Interface(typeof(ISelector))]
    private UnityEngine.Object _selector;

    private ISelector selector;

    [Header("Prefab & Position")]
    public GameObject cubePrefab;
    public Transform pinchPoint;

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
        if (spawnedCube != null)
        {
            // Gest igen = ta bort kub
            Destroy(spawnedCube);
            spawnedCube = null;
            Debug.Log("??? Tog bort kuben.");
        }
        else
        {
            // Skapa ny kub i handen
            spawnedCube = Instantiate(cubePrefab, pinchPoint.position, pinchPoint.rotation);
            spawnedCube.transform.SetParent(pinchPoint, worldPositionStays: false);
            spawnedCube.transform.localPosition = Vector3.zero;
            Debug.Log("? Spawnade ny kub vid pinchPoint!");
        }
    }
}
