using UnityEngine;
using UnityEngine.UI;

public class HealthBarCutFallDown : MonoBehaviour
{
    private RectTransform rectTransform;
    private float fallDownTimer;
    [SerializeField] float fallSpeed = 10f;
    [SerializeField] float alphaFadeSpeed = 5f;
    [SerializeField] float bumpHeight = 20f;
    [SerializeField] float bumpDuration = 0.2f;
    private float fadeTimer;
    private Image image;
    private Color color;

    private float verticalVelocity;
    private float gravity;

    private Vector2 bumpDirection;
    private float angularVelocity;
    private float currentAngle;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
        color = image.color;

        verticalVelocity = (2f * bumpHeight) / bumpDuration;
        gravity = (2f * bumpHeight) / (bumpDuration * bumpDuration);

        float angle = Random.Range(-15f, 15f);
        float rad = angle * Mathf.Deg2Rad;
        bumpDirection = new Vector2(Mathf.Sin(rad), 1f).normalized;

        angularVelocity = Random.Range(-30f, 30f);
        currentAngle = 0f;

        fallDownTimer = bumpDuration;
        fadeTimer = 0.5f;
    }

    private void Update()
    {
        fallDownTimer -= Time.deltaTime;
        if (fallDownTimer > 0f)
        {
            rectTransform.anchoredPosition += bumpDirection * verticalVelocity * Time.deltaTime;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            rectTransform.anchoredPosition += new Vector2(bumpDirection.x, -1f).normalized * fallSpeed * Time.deltaTime;

            fadeTimer -= Time.deltaTime;
            if (fadeTimer <= 0f)
            {
                color.a -= alphaFadeSpeed * Time.deltaTime;
                image.color = color;
                if (color.a <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        currentAngle += angularVelocity * Time.deltaTime;
        rectTransform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}