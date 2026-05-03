using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject mapPanel;
    public GameObject loadoutPanel;

    public CanvasGroup mainMenuGroup;
    public CanvasGroup mapGroup;

    public float transitionSpeed = 0.25f;

    private void Start()
    {
        ShowInstant(mainMenuPanel, mainMenuGroup);
        HideInstant(mapPanel, mapGroup);

        if (loadoutPanel != null)
            loadoutPanel.SetActive(false);
    }

    public void OpenMapSelection()
    {
        StartCoroutine(SwitchPanel(mainMenuPanel, mainMenuGroup, mapPanel, mapGroup));
    }

    public void BackToMainMenu()
    {
        StartCoroutine(SwitchPanel(mapPanel, mapGroup, mainMenuPanel, mainMenuGroup));
    }

    public void StartDesertMap()
    {
        Debug.Log("Starting Desert Training Base");

        // Later replace with real scene name:
        // SceneManager.LoadScene("DesertMap");
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