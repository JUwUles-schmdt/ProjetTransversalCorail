using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


[System.Serializable]
public struct Journal
{
    public string titre;
    public string description;
}

public class GameManager : MonoBehaviour
{


    public GameObject boutonPause;






    [SerializeField] private float NewsPaperCoolDown;
    [HideInInspector] public float NewsPaperCD;
    private int newsPaperCount = 0;


    public GameObject newsPaper;
    public TMP_Text NewsTitle;
    public TMP_Text NewsDescription;


    public List<Journal> journals = new List<Journal>();



    private bool isPlaying=false;
    public GameObject[] rows;
    public GameObject[] row1;
    public GameObject[] row2;
    public GameObject[] row3;
    public GameObject[] row4;
    public GameObject[] row5;
    public GameObject[] row6;
    public GameObject[] row7;


    public float waterTemp;
    public float waterAcid;
    public float waterSpeed;


    private GameObject swapPrefab;






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







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        newsPaper.SetActive(false);

        (currentDay, currentWeek, dayTimer, isRunning) = (1, 1, 0f, true);
        Debug.Log("[GameManager] Début — Semaine 1, Jour 1");





        for (int i = 0; i < rows[0].transform.childCount; i++)
        {
            row1[i] = rows[0].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[1].transform.childCount; i++)
        {
            row2[i] = rows[1].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[2].transform.childCount; i++)
        {
            row3[i] = rows[2].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[3].transform.childCount; i++)
        {
            row4[i] = rows[3].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[4].transform.childCount; i++)
        {
            row5[i] = rows[4].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[5].transform.childCount; i++)
        {
            row6[i] = rows[5].transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < rows[6].transform.childCount; i++)
        {
            row7[i] = rows[6].transform.GetChild(i).gameObject;
        }



        
        for (int i=0; i<row1.Length; i++)
        {
            if (i!=0)
                row1[i].GetComponent<CoralController>().leftNeighbor = row1[i-1];
            if (i != 6)
                row1[i].GetComponent<CoralController>().rightNeighbor = row1[i + 1];
            row1[i].GetComponent<CoralController>().topNeighbor = row2[i];
            if (i!=3) row1[i].SetActive(false);
        }
        for (int i = 0; i < row2.Length; i++)
        {
            if (i != 0)
                row2[i].GetComponent<CoralController>().leftNeighbor = row2[i - 1];
            if (i != 6)
                row2[i].GetComponent<CoralController>().rightNeighbor = row2[i + 1];
            row2[i].GetComponent<CoralController>().topNeighbor = row3[i];
            row2[i].GetComponent<CoralController>().bottomNeighbor = row1[i];
            row2[i].SetActive(false);
        }
        for (int i = 0; i < row3.Length; i++)
        {
            if (i != 0)
                row3[i].GetComponent<CoralController>().leftNeighbor = row3[i - 1];
            if (i != 6)
                row3[i].GetComponent<CoralController>().rightNeighbor = row3[i + 1];
            row3[i].GetComponent<CoralController>().topNeighbor = row4[i];
            row3[i].GetComponent<CoralController>().bottomNeighbor = row2[i];
            row3[i].SetActive(false);
        }
        for (int i = 0; i < row4.Length; i++)
        {
            if (i != 0)
                row4[i].GetComponent<CoralController>().leftNeighbor = row4[i - 1];
            if (i != 6)
                row4[i].GetComponent<CoralController>().rightNeighbor = row4[i + 1];
            row4[i].GetComponent<CoralController>().topNeighbor = row5[i];
            row4[i].GetComponent<CoralController>().bottomNeighbor = row3[i];
            row4[i].SetActive(false);
        }
        for (int i = 0; i < row5.Length; i++)
        {
            if (i != 0)
                row5[i].GetComponent<CoralController>().leftNeighbor = row5[i - 1];
            if (i != 6)
                row5[i].GetComponent<CoralController>().rightNeighbor = row5[i + 1];
            row5[i].GetComponent<CoralController>().topNeighbor = row6[i];
            row5[i].GetComponent<CoralController>().bottomNeighbor = row4[i];
            row5[i].SetActive(false);
        }
        for (int i = 0; i < row6.Length; i++)
        {
            if (i != 0)
                row6[i].GetComponent<CoralController>().leftNeighbor = row6[i - 1];
            if (i != 6)
                row6[i].GetComponent<CoralController>().rightNeighbor = row6[i + 1];
            row6[i].GetComponent<CoralController>().topNeighbor = row7[i];
            row6[i].GetComponent<CoralController>().bottomNeighbor = row5[i];
            row6[i].SetActive(false);
        }
        for (int i = 0; i < row7.Length; i++)
        {
            if (i != 0)
                row7[i].GetComponent<CoralController>().leftNeighbor = row7[i - 1];
            if (i != 6)
                row7[i].GetComponent<CoralController>().rightNeighbor = row7[i + 1];
            row7[i].GetComponent<CoralController>().bottomNeighbor = row6[i];
            row7[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {


    }
    

    public void Journal()
    {
        newsPaper.SetActive(true);
        Time.timeScale = 0f;
        NewsTitle.text = journals[newsPaperCount].titre;
        NewsDescription.text = journals[newsPaperCount].description;
    }

    public void toggleJournal()
    {
        newsPaper.SetActive(!newsPaper.activeSelf);
        if (newsPaper.activeSelf)
        {
            Time.timeScale = 0f;
            NewsTitle.text = journals[newsPaperCount].titre;
            NewsDescription.text = journals[newsPaperCount].description;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void swapBuildPlan(GameObject prefab)
    {
        swapPrefab = prefab;

        ReplaceRow(row1);
        ReplaceRow(row2);
        ReplaceRow(row3);
        ReplaceRow(row4);
        ReplaceRow(row5);
        ReplaceRow(row6);
        ReplaceRow(row7);
    }
    private void ReplaceRow(GameObject[] row)
    {
        CoralController template = swapPrefab.GetComponent<CoralController>();

        for (int i = 0; i < row.Length; i++)
        {
            CoralController cc = row[i].GetComponent<CoralController>();

            if (!cc.exist&&!cc.isBuilding)
            {
                cc.state = template.state;
                cc.energyMax = template.energyMax;
                cc.currentEnergy = 0f;
                cc.energyRegen = template.energyRegen;

                cc.price = template.price;
                cc.resChemicals = template.resChemicals;
                cc.resPhysical = template.resPhysical;

                cc.GetComponent<Image>().sprite = swapPrefab.GetComponent<Image>().sprite;

                cc.type = template.type;

                cc.exist = false;
                cc.hovered = false;
            }
        }
        template.Hide();
    }


    public float getTemp(GameObject position)
    {
        return position.GetComponent<CoralController>().resPhysical/waterTemp;
    }


    public float getLight(GameObject position)
    {
        if (row1.Contains(position) || row2.Contains(position))
        {
            return 1f;
        }
        if (row7.Contains(position) || row3.Contains(position))
        {

            return .9f;
        }
        if (row6.Contains(position) || row4.Contains(position))
        {

            return .8f;
        }
        if (row5.Contains(position))
        {

            return .5f;
        }
        else return 2f;
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
            newsPaperCount += 1;
            Journal();
        }
        else currentDay++;

        OnDayChanged?.Invoke(currentDay, currentWeek);
        Debug.Log($"[GameManager] → Jour {currentDay})");
        
    }
}
