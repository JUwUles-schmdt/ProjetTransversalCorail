using UnityEngine;
using TMPro;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    void Start()
    {
        dayText.text = GameManager.Instance.CurrentDay.ToString();
        GameManager.Instance.OnDayChanged += HandleDayChanged;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnDayChanged -= HandleDayChanged;
    }

    void HandleDayChanged(int day, int week) => dayText.text = day.ToString();
}
