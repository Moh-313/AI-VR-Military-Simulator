using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image cardImage;
    public Image mapImage;

    public Color normalColor = new Color32(64, 54, 38, 255);
    public Color hoverColor = new Color32(120, 96, 55, 255);

    public float normalScale = 1f;
    public float hoverScale = 1.06f;
    public float speed = 10f;

    private Vector3 targetScale;
    private Color targetColor;

    void Start()
    {
        targetScale = Vector3.one * normalScale;
        targetColor = normalColor;

        if (cardImage != null)
            cardImage.color = normalColor;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        if (cardImage != null)
            cardImage.color = Color.Lerp(cardImage.color, targetColor, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = Vector3.one * hoverScale;
        targetColor = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = Vector3.one * normalScale;
        targetColor = normalColor;
    }
}