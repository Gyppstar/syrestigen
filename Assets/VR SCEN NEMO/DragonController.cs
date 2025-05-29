using UnityEngine;

public class DragonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float minX = -2f;
    public float maxX = 2f;

    [Header("Rotation Settings")]
    public float idleResetDelay = 2f;
    public float forwardTilt = 20f;

    [Header("Model Facing Setup")]
    public float movementBaseY = 0f;       // Actual forward direction of the dragon model
    public float idleFacingY = 180f;       // The Y angle that faces the camera

    [Header("Audio")]
    public AudioClip collectibleSound;
    public AudioClip hazardSound;

    private AudioSource audioSource;
    private Quaternion targetRotation;
    private float idleTimer = 0f;
    private bool isMoving = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetRotation = Quaternion.Euler(forwardTilt, idleFacingY, 0f);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;

        if (Mathf.Abs(horizontal) > 0.01f)
        {
            isMoving = true;
            idleTimer = 0f;

            position.x += horizontal * moveSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            transform.position = position;

            // Movement turns (face-first)
            float yRot = movementBaseY + (horizontal > 0 ? 90f : -90f);
            targetRotation = Quaternion.Euler(forwardTilt, yRot, 0f);
        }
        else
        {
            if (isMoving)
            {
                idleTimer += Time.deltaTime;
            }

            if (idleTimer >= idleResetDelay)
            {
                isMoving = false;
                targetRotation = Quaternion.Euler(forwardTilt, idleFacingY, 0f);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            GameManager.instance.AddScore(100);
            PlaySound(collectibleSound);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Hazard"))
        {
            GameManager.instance.TakeDamage(1);
            PlaySound(hazardSound);
            other.gameObject.SetActive(false);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}