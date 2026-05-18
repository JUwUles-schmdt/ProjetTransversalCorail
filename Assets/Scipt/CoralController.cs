using UnityEngine;
using UnityEngine.UI;

public class CoralController : MonoBehaviour
{
    public int state=0;
    public float energyMax;
    public float currentEnergy;
    public float energyRegen;
    private float cooldown;
    public float resChemicals;
    public float resPhysical;

    public bool exist;
    public bool hovered;


    public GameObject leftNeighbor;
    public GameObject rightNeighbor;
    public GameObject topNeighbor;
    public GameObject bottomNeighbor;
    private static CoralController currentSelected;

    void Start()
    {
        cooldown = energyRegen;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0&&currentEnergy!=energyMax) 
        {
            currentEnergy += 1; 
            cooldown = energyRegen; 
        }
        cooldown -= Time.deltaTime;
    }


    public void OnClick()
    {
        
        
        if (!exist&&hovered) 
        {
            exist = true;
            ColorBlock colors = GetComponent<Button>().colors;
            colors.normalColor = new Color(1f, 1f, 1f, 1f);
            GetComponent<Button>().colors = colors;
        }
        else 
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
        if (neighbor != null && !neighbor.GetComponent<CoralController>().exist)
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

}
