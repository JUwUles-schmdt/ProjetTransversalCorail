using UnityEngine;
using TMPro;

public enum couleur { BrunMarron, VertOlive, BleuViolet, RoseRouge, Jaune, VertFluo, Orange }
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


    public float waterTemp;
    public float waterAcid;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEventCD = Random.Range(EventMinCoolDown, EventMaxCoolDown);

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


}
