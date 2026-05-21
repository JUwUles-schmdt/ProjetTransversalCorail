using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    public GameObject boutonPause;
    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        ShowMainMenu();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnOptionsButton()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnCreditsButton()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
}
