using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("How much the rotation changes relative to mouse movement.")]
    public float parallaxStrength = 10f;
    [Tooltip("Clamp the maximum rotation (in degrees) from the original rotation.")]
    public Vector2 maxRotation = new Vector2(10f, 10f);

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 normalized = (mousePosition - screenCenter) / screenCenter;

        float rotX = Mathf.Clamp(-normalized.y * parallaxStrength, -maxRotation.y, maxRotation.y);
        float rotY = Mathf.Clamp(normalized.x * parallaxStrength, -maxRotation.x, maxRotation.x);

        Quaternion targetRotation = initialRotation * Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = targetRotation;
    }
}