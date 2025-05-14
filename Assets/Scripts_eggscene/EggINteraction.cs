using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EggINteraction : MonoBehaviour
{
    public Transform leftHandTarget;
    public Transform rightHandTarget;

    public float flySpeed = 2f;
    public float arriveDistance = 0.05f;

    private bool isFlying = false;
    private Transform targetHand;

    public void OnEggClicked()
    {
        if (isFlying) return;

        Debug.Log("Ägg klickat!");
        targetHand = GameManager_menu.Instance.isLeftHanded ? rightHandTarget : leftHandTarget;
        StartCoroutine(FlyToHand());
    }

    IEnumerator FlyToHand()
    {
        isFlying = true;

        while (Vector3.Distance(transform.position, targetHand.position) > arriveDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetHand.position, flySpeed * Time.deltaTime);
            yield return null;
        }

        isFlying = false;
        Debug.Log("Ägget är i handen!");
        StartCoroutine(CrackAndHatch());
    }

    IEnumerator CrackAndHatch()
    {
        Renderer rend = GetComponent<Renderer>();

        for (int i = 0; i < 3; i++)
        {
            if (rend != null)
                rend.material.color = Color.red;

            TriggerHaptics();
            yield return new WaitForSeconds(0.2f);

            if (rend != null)
                rend.material.color = Color.white;

            yield return new WaitForSeconds(0.2f);
        }

        gameObject.SetActive(false); // göm ägget

        SpawnAnimal(); // placeholder-djur
        yield return new WaitForSeconds(2f); // vänta lite

        SceneManager.LoadScene("MainGameScene"); // byt scen
    }

    void SpawnAnimal()
    {
        GameObject animal = GameObject.CreatePrimitive(PrimitiveType.Cube);
        animal.transform.localScale = Vector3.one * 0.1f;

        Vector3 handPos = GameManager_menu.Instance.isLeftHanded
            ? rightHandTarget.position
            : leftHandTarget.position;

        animal.transform.position = handPos + new Vector3(0, 0.05f, 0); // ovanpå handen
    }

    void TriggerHaptics()
    {
        var node = GameManager_menu.Instance.isLeftHanded ? UnityEngine.XR.XRNode.LeftHand : UnityEngine.XR.XRNode.RightHand;
        var device = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(node);

        if (device.TryGetHapticCapabilities(out var capabilities) && capabilities.supportsImpulse)
        {
            device.SendHapticImpulse(0, 0.5f, 0.2f);
        }
    }
}
