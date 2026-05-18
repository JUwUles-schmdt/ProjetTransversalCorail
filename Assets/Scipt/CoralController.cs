using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class CoralController : MonoBehaviour
{
    public int state=0;
    public float energyMax;
    public float currentEnergy;
    public float energyRegen;
    private float cooldown;
    public float price;
    public float resChemicals;
    public float resPhysical;
    public Sprite type;


    public bool exist;
    public bool hovered;


    public GameObject leftNeighbor;
    public GameObject rightNeighbor;
    public GameObject topNeighbor;
    public GameObject bottomNeighbor;
    private static CoralController currentSelected;

    private bool isBuilding;
    private bool willDestroy;


    void Start()
    {
        price = energyMax / 2;
        cooldown = energyRegen;

    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding) return;
        if (cooldown <= 0&&currentEnergy!=energyMax) 
        {
            currentEnergy += 1; 
            cooldown = energyRegen; 
        }
        cooldown -= Time.deltaTime;

        if (currentSelected == this)
        {
            FindObjectOfType<UiManager>().changeDatas(type, energyMax, currentEnergy, resChemicals, resPhysical);
        }
    }


    public void OnClick()
    {
        
        
        if (!exist&&hovered&&currentSelected.currentEnergy>price&&!isBuilding) 
        {
            currentSelected.currentEnergy -= price;
            currentEnergy = 0;
            ColorBlock colors = GetComponent<Button>().colors;
            colors.normalColor = new Color(1f, 1f, 1f, 1f);
            GetComponent<Button>().colors = colors;
            StartCoroutine(build(getTimer()));

            if (currentSelected != null)
            {
                currentSelected.HideNeighbors();
            }
        }
        else if (exist&&!isBuilding)
        {
            if (currentSelected != null)
            {
                currentSelected.HideNeighbors();
            }
            currentSelected = this;


            Color transparentRed = new Color(1f, 1f, 1f, 0.5f);

            ShowNeighbor(leftNeighbor, transparentRed);
            ShowNeighbor(rightNeighbor, transparentRed);
            ShowNeighbor(topNeighbor, transparentRed);
            ShowNeighbor(bottomNeighbor, transparentRed);
    }
    }

    void ShowNeighbor(GameObject neighbor, Color color)
    {
        if (neighbor != null && !neighbor.GetComponent<CoralController>().exist)
        {
            neighbor.GetComponent<CoralController>().hovered = true;
            Button button = neighbor.GetComponent<Button>();

            ColorBlock colors = button.colors;
            colors.normalColor = color;
            button.colors = colors;

            neighbor.SetActive(true);
        }
    }

    void HideNeighbor(GameObject neighbor)
    {
        if (neighbor != null && !neighbor.GetComponent<CoralController>().exist && !neighbor.GetComponent<CoralController>().isBuilding)
        {
            neighbor.GetComponent <CoralController>().hovered = false;
            neighbor.SetActive(false);
        }
    }

    void HideNeighbors()
    {
        HideNeighbor(leftNeighbor);
        HideNeighbor(rightNeighbor);
        HideNeighbor(topNeighbor);
        HideNeighbor(bottomNeighbor);
    }



    private IEnumerator build(float time)
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        ColorBlock colors = GetComponent<Button>().colors;
        colors.disabledColor = new Color(1f, 1f, 1f, 0.25f);
        GetComponent<Button>().colors = colors;
        isBuilding = true;
        yield return new WaitForSeconds(time/3);
        colors.disabledColor = new Color(1f, 1f, 1f, 0.5f);
        GetComponent<Button>().colors = colors;
        yield return new WaitForSeconds(time / 3);
        if (willDestroy)
        {
            isBuilding = false;
            this.gameObject.GetComponent<Button>().interactable = true;
            yield break;
        }
        else
        {
        colors.disabledColor = new Color(1f, 1f, 1f, 0.75f);
        GetComponent<Button>().colors = colors;
        }
        yield return new WaitForSeconds(time / 3);
        colors.disabledColor = new Color(1f, 1f, 1f, 1f);
        GetComponent<Button>().colors = colors;
        isBuilding = false;
        exist = true;
        this.gameObject.GetComponent<Button>().interactable = true;
    }

    public float getTimer()
    {
        willDestroy = false;
        float _timer=energyMax*energyRegen;
        _timer = _timer / (resChemicals / FindObjectOfType<GameManager>().waterAcid);
        if (resChemicals / FindObjectOfType<GameManager>().waterAcid<0.5)
        {
            willDestroy=true;
        }
        _timer = _timer / (resPhysical/ FindObjectOfType<GameManager>().getTemp(this.gameObject));
        if (resPhysical / FindObjectOfType<GameManager>().getTemp(this.gameObject)<0.5)
        {
            willDestroy = true;
        }

        return _timer;
    }
}
