using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Time settings")]
    public float dayDuration = 30f;

    [Header("État courant (lecture seule)")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentWeek = 1;
    [SerializeField] private float dayTimer = 0f;
    [SerializeField] private bool isRunning = false;

    public event System.Action<int, int> OnDayChanged;
    public event System.Action<int> OnWeekChanged;
    public event System.Action<float, float> OnDayTick;

    public int CurrentDay => currentDay;
    public int CurrentWeek => currentWeek;
    public float DayTimer => dayTimer;
    public float DayProgress => dayTimer / dayDuration;
    public bool IsRunning => isRunning;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() => StartGame();

    void Update()
    {
        if (!isRunning) return;
        dayTimer += Time.deltaTime;
        OnDayTick?.Invoke(dayTimer, dayDuration);
        if (dayTimer >= dayDuration) AdvanceDay();
    }

    public void StartGame()
    {
        (currentDay, currentWeek, dayTimer, isRunning) = (1, 1, 0f, true);
        Debug.Log("[GameManager] Début — Semaine 1, Jour 1");
    }

    public void SkipToNextDay() { if (isRunning) AdvanceDay(); }
    public void PauseTime() => isRunning = false;
    public void ResumeTime() => isRunning = true;

    private void AdvanceDay()
    {
        dayTimer = 0f;
        if (currentDay >= 7)
        {
            currentDay = 1;
            OnWeekChanged?.Invoke(++currentWeek);
            Debug.Log($"[GameManager] ── Nouvelle semaine ──");
        }
        else currentDay++;

        OnDayChanged?.Invoke(currentDay, currentWeek);
        Debug.Log($"[GameManager] → Jour {currentDay})");
    }
}
