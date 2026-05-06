using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject loadoutPanel;

    [Header("Weapon Display")]
    public GameObject weaponDisplayPanel;
    public Image weaponImage;
    public TMP_Text weaponNameText;
    public TMP_Text weaponDetailsText;

    [Header("Weapon Sprites")]
    public Sprite ak47Sprite;

    private void Start()
    {
        weaponDisplayPanel.SetActive(false);
    }

    public void OpenLoadout()
    {
        menuPanel.SetActive(false);
        loadoutPanel.SetActive(true);

        weaponDisplayPanel.SetActive(false);
    }

    public void CloseLoadout()
    {
        loadoutPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ShowAK47()
    {
        weaponDisplayPanel.SetActive(true);

        weaponImage.sprite = ak47Sprite;
        weaponImage.enabled = true;

        weaponNameText.text = "AK-47";

        weaponDetailsText.text =
            "Reliable assault rifle used for close and medium-range combat.\n\n" +
            "Damage: High\n" +
            "Range: Medium\n" +
            "Fire Rate: Automatic\n" +
            "Status: Available";
    }
}