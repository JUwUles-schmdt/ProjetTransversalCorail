using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDefeatManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject defeatPanel;

    private void Start()
    {
        DisablePanel();
    }

    public void GameOver()
    {
        defeatPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void DisablePanel()
    {
        winPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }
}
