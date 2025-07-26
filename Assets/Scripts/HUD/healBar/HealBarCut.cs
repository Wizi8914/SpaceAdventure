using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealBarCut : MonoBehaviour
{
    private const float barWidth = 100f; // Width of the health bar
    public Image barImage;

    [SerializeField] private Transform damagedBarTemplate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealBar(float beforeDamageFillAmount, float barFillAmount, bool isHeadShot = false)
    {
        Transform damagedBar = Instantiate(damagedBarTemplate, barImage.transform);
        damagedBar.gameObject.SetActive(true);
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(barWidth * barFillAmount, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        damagedBar.GetComponent<Image>().fillAmount = beforeDamageFillAmount - barFillAmount;
        damagedBar.GetComponent<HealthBarCutFallDown>().enabled = true;
        if (isHeadShot) damagedBar.GetComponent<Image>().color = Color.yellow; // Change color for headshot

    }
}
