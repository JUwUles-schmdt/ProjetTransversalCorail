using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum especes {Gros, Moyen, Petit }
public class GameManager : MonoBehaviour
{
    [SerializeField] private float EventMaxCoolDown;
    [SerializeField] private float EventMinCoolDown;
    [HideInInspector] public float EventCD;
    private float currentEventCD;


    [SerializeField] private float NewsPaperCoolDown;
    [HideInInspector] public float NewsPaperCD;
    private int newsPaperCount = 0;


    public GameObject newsPaper;
    public TMP_Text News;
    public GameObject button1;
    public TMP_Text Choix1;
    public GameObject button2;
    public TMP_Text Choix2;



    private bool isPlaying=false;
    public GameObject[] rows;
    public GameObject[] row1;
    public GameObject[] row2;
    public GameObject[] row3;
    public GameObject[] row4;
    public GameObject[] row5;
    public GameObject[] row6;
    public GameObject[] row7;


    [SerializeField] private List<GameObject> specialPositions = new List<GameObject>();


    public float waterTemp;
    public float waterAcid;


    private GameObject swapPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEventCD = Random.Range(EventMinCoolDown, EventMaxCoolDown);


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
        if(isPlaying)EventCD += Time.deltaTime;
        if (isPlaying) NewsPaperCD += Time.deltaTime;
        if (NewsPaperCD >= NewsPaperCoolDown&& isPlaying) Journal();
        if (EventCD >= currentEventCD&& isPlaying) EvenementHumain();



    }

    private void EvenementHumain()
    {
        int randomNumber = Random.Range(1, 5);
        switch (randomNumber) 
        {
            case 1: //petrolier

                break;
            case 2: //pecheur

                break;
            case 3: //

                break;

        }

        currentEventCD = Random.Range(EventMinCoolDown, EventMaxCoolDown);
    }

    private void Journal()
    {

        switch (newsPaperCount)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
            case 9:

                break;
            case 10:

                break;
            case 11:

                break;
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

            if (!cc.exist)
            {
                cc.state = template.state;
                cc.energyMax = template.energyMax;
                cc.currentEnergy = 0f;
                cc.energyRegen = template.energyRegen;

                cc.price = template.price;
                cc.resChemicals = template.resChemicals;
                cc.resPhysical = template.resPhysical;

                cc.type = template.type;

                cc.exist = false;
                cc.hovered = false;

                // reset visuel
                Button btn = row[i].GetComponent<Button>();
                ColorBlock colors = btn.colors;
                colors.normalColor = new Color(1f, 1f, 1f, 1f);
                btn.colors = colors;
            }
        }
    }


    public float getTemp(GameObject position)
    {
        if (specialPositions.Contains(position))
        {
            return 75f;
        }

        return 50f;
    }

}
