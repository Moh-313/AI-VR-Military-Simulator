using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject mapPanel;
    public GameObject loadoutPanel;

    public void OpenMapSelection()
    {
        mainMenuPanel.SetActive(false);
        mapPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mapPanel.SetActive(false);

        if (loadoutPanel != null)
            loadoutPanel.SetActive(false);

        mainMenuPanel.SetActive(true);
    }

    public void StartDesertMap()
    {
        Debug.Log("Starting Desert Training Base");

        // Later replace this with the real scene name:
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
}