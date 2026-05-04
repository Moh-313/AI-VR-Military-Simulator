using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Images")]
    public Image cardBackground;
    public Image mapImage;
    public Image glowBorder;

    [Header("Normal State")]
    public Color normalImageColor = new Color(1f, 1f, 1f, 0.78f);
    public Color normalCardColor = new Color(0f, 0f, 0f, 0.35f);

    [Header("Hover State")]
    public Color hoverImageColor = new Color(1f, 1f, 1f, 1f);
    public Color hoverCardColor = new Color(0.45f, 0.32f, 0.12f, 0.65f);

    [Header("Animation")]
    public float normalScale = 1f;
    public float hoverScale = 1.08f;
    public float speed = 10f;

    private Vector3 targetScale;
    private Color targetImageColor;
    private Color targetCardColor;

    private void Start()
    {
        targetScale = Vector3.one * normalScale;
        targetImageColor = normalImageColor;
        targetCardColor = normalCardColor;

        if (mapImage != null)
            mapImage.color = normalImageColor;

        if (cardBackground != null)
            cardBackground.color = normalCardColor;

        if (glowBorder != null)
            glowBorder.enabled = false;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );

        if (mapImage != null)
        {
            mapImage.color = Color.Lerp(
                mapImage.color,
                targetImageColor,
                Time.deltaTime * speed
            );
        }

        if (cardBackground != null)
        {
            cardBackground.color = Color.Lerp(
                cardBackground.color,
                targetCardColor,
                Time.deltaTime * speed
            );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = Vector3.one * hoverScale;
        targetImageColor = hoverImageColor;
        targetCardColor = hoverCardColor;

        if (glowBorder != null)
            glowBorder.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = Vector3.one * normalScale;
        targetImageColor = normalImageColor;
        targetCardColor = normalCardColor;

        if (glowBorder != null)
            glowBorder.enabled = false;
    }
}