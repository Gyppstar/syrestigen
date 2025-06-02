using UnityEngine;

public class DollWiggle : MonoBehaviour
{
    public Transform followTarget;  // T.ex. handankare eller controller
    public float positionDamping = 5f;
    public float rotationDamping = 5f;
    public float maxWiggleAngle = 10f;

    private Vector3 velocity;
    private Quaternion targetRotation;

    void LateUpdate()
    {
        if (followTarget == null) return;

        // Följ långsamt efter handens position (dock-effekt)
        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position, ref velocity, 1f / positionDamping);

        // Räkna ut skillnad i rörelseriktning → skapa mjuk rotation
        Vector3 direction = velocity.normalized;
        if (direction != Vector3.zero)
        {
            Quaternion wiggle = Quaternion.LookRotation(direction);
            targetRotation = Quaternion.Lerp(targetRotation, wiggle, Time.deltaTime * rotationDamping);
        }

        // Begränsa hur mycket han får vrida sig (så det inte blir galet)
        Quaternion limitedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxWiggleAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, limitedRotation, Time.deltaTime * rotationDamping);
    }
}