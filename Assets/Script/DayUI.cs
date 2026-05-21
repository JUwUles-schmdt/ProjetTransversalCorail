using UnityEngine;
using TMPro;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public GameManager gameManager;

    void Start()
    {
        dayText.text = gameManager.CurrentDay.ToString();
        gameManager.OnDayChanged += HandleDayChanged;
    }

    void OnDisable()
    {
        if (gameManager != null)
            gameManager.OnDayChanged -= HandleDayChanged;
    }

    void HandleDayChanged(int day, int week) => dayText.text = day.ToString();
}
