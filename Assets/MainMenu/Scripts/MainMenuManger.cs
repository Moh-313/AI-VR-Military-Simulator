using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject mapPanel;
    public GameObject loadoutPanel;
    public GameObject missionBriefingPanel;

    [Header("Canvas Groups")]
    public CanvasGroup mainMenuGroup;
    public CanvasGroup mapGroup;

    [Header("Transition")]
    public float transitionSpeed = 0.25f;

    [Header("Main Background")]
    public Image mainBackground;
    public Sprite desertBackground;
    public Sprite airBaseBackground;
    public Sprite waterBaseBackground;

    [Header("Mission Briefing UI")]
    public Image miniMapImage;
    public TMP_Text briefingTitleText;

    [Header("Desert Briefing Data")]
    public Sprite desertMiniMap;
    public string desertSceneName = "DesertScene";

    private string selectedSceneName = "";
    private bool desertSelected = false;

    private void Start()
    {
        ShowInstant(mainMenuPanel, mainMenuGroup);
        HideInstant(mapPanel, mapGroup);

        if (loadoutPanel != null)
            loadoutPanel.SetActive(false);

        if (missionBriefingPanel != null)
            missionBriefingPanel.SetActive(false);

        if (mainBackground != null && desertBackground != null)
            mainBackground.sprite = desertBackground;
    }

    public void OpenMapSelection()
    {
        StartCoroutine(SwitchPanel(mainMenuPanel, mainMenuGroup, mapPanel, mapGroup));
    }

    public void BackToMainMenu()
    {
        if (missionBriefingPanel != null)
            missionBriefingPanel.SetActive(false);

        StartCoroutine(SwitchPanel(mapPanel, mapGroup, mainMenuPanel, mainMenuGroup));
    }

    public void SelectDesertMap()
    {
        desertSelected = true;
        selectedSceneName = desertSceneName;

        if (mainBackground != null && desertBackground != null)
            mainBackground.sprite = desertBackground;

        OpenMissionBriefing();
    }

    public void SelectAirBaseMap()
    {
        desertSelected = false;
        selectedSceneName = "";

        if (mainBackground != null && airBaseBackground != null)
            mainBackground.sprite = airBaseBackground;

        Debug.Log("Air Base is locked for now.");
    }

    public void SelectWaterBaseMap()
    {
        desertSelected = false;
        selectedSceneName = "";

        if (mainBackground != null && waterBaseBackground != null)
            mainBackground.sprite = waterBaseBackground;

        Debug.Log("Water Base is locked for now.");
    }

    public void OpenMissionBriefing()
    {
        if (!desertSelected)
        {
            Debug.Log("Only Desert has briefing for now.");
            return;
        }

        if (missionBriefingPanel != null)
            missionBriefingPanel.SetActive(true);

        if (briefingTitleText != null)
            briefingTitleText.text = "DESERT OPERATION BRIEFING";

        if (miniMapImage != null && desertMiniMap != null)
            miniMapImage.sprite = desertMiniMap;
    }

    public void CloseMissionBriefing()
    {
        if (missionBriefingPanel != null)
            missionBriefingPanel.SetActive(false);
    }

    public void DeployMission()
    {
        if (selectedSceneName == "")
        {
            Debug.Log("No mission selected.");
            return;
        }

        Debug.Log("Loading VR scene: " + selectedSceneName);
        SceneManager.LoadScene(selectedSceneName);
    }

    public void OpenLoadout()
    {
        Debug.Log("Open Loadout");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private IEnumerator SwitchPanel(GameObject fromPanel, CanvasGroup fromGroup, GameObject toPanel, CanvasGroup toGroup)
    {
        yield return FadeOut(fromGroup);

        fromPanel.SetActive(false);
        toPanel.SetActive(true);

        yield return FadeIn(toGroup);
    }

    private IEnumerator FadeOut(CanvasGroup group)
    {
        group.interactable = false;
        group.blocksRaycasts = false;

        while (group.alpha > 0)
        {
            group.alpha -= Time.deltaTime / transitionSpeed;
            yield return null;
        }

        group.alpha = 0;
    }

    private IEnumerator FadeIn(CanvasGroup group)
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;

        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime / transitionSpeed;
            yield return null;
        }

        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private void ShowInstant(GameObject panel, CanvasGroup group)
    {
        panel.SetActive(true);
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private void HideInstant(GameObject panel, CanvasGroup group)
    {
        panel.SetActive(false);
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}