using UnityEngine;
using UnityEngine.UI;

public class HealthBarCutFallDown : MonoBehaviour
{
    private RectTransform rectTransform;
    private float fallDownTimer;
    [SerializeField] float fallSpeed = 10f;
    [SerializeField] float alphaFadeSpeed = 5f;
    [SerializeField] float bumpHeight = 20f;      // Hauteur du bump
    [SerializeField] float bumpDuration = 0.2f;   // Durée du bump
    private float fadeTimer;
    private Image image;
    private Color color;

    private float verticalVelocity;
    private float gravity;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = transform.GetComponent<Image>();
        color = image.color;

        verticalVelocity = (2f * bumpHeight) / bumpDuration;
        gravity = (2f * bumpHeight) / (bumpDuration * bumpDuration);

        fallDownTimer = bumpDuration;
        fadeTimer = 0.5f;
    }

    private void Update()
    {
        fallDownTimer -= Time.deltaTime;
        if (fallDownTimer > 0f)
        {
            rectTransform.anchoredPosition += Vector2.up * verticalVelocity * Time.deltaTime;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

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
    }
}