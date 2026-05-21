using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;



[System.Serializable]
public struct chaine
{
    public CoralController root;
    public List<CoralController> corals;
    public float energy;
    public float maxEnergy;

}





public class CoralController : MonoBehaviour
{
    public int state = 0;
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
    public static CoralController currentSelected;

    public bool isBuilding { get; private set; }
    private bool willDestroy;


    public chaine chain;
    private bool isPartOfChain = false;

    public GameManager gameManager;

    public List<CoralController> childs = new List<CoralController>();
    private List<BottlesController> bottles = new List<BottlesController>();

    public float BottlesPenaltyMultiplier => BottlesController.EnergyMultiplier(bottles.Count);

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        price = energyMax / 2;
        cooldown = energyRegen;
        if (exist)
        {
            currentSelected = this;
            chain.corals.Add(this);
            chain.maxEnergy += energyMax;
            isPartOfChain = true;
        }
        chain.root = this;


    }

    // Update is called once per frame
    void Update()
    {
        if (exist)
        {
            if (gameManager.getTemp(this.gameObject) < 0.5f || gameManager.waterAcid > resChemicals * 2)
            {
                destroy();
            }
        }
        if (isBuilding) return;
        if (cooldown <= 0 && chain.root.chain.energy < chain.root.chain.maxEnergy)
        {
            if (isPartOfChain)
            {
                if (chain.root == this)
                {
                    chain.energy += (1f * getMultiplier()) / BottlesPenaltyMultiplier;
                }
                else
                {
                    chain.root.chain.energy += (0.25f * getMultiplier()) / BottlesPenaltyMultiplier;
                }
                cooldown = energyRegen;
            }
        }
        if (chain.root.chain.energy >= chain.root.chain.maxEnergy)
        {
            chain.root.chain.energy = chain.root.chain.maxEnergy;
        }
        cooldown -= Time.deltaTime;
        for (int i = 0; i < chain.corals.Count; i++)
        {
            chain.corals[i].currentEnergy = chain.energy;
        }
        if (currentSelected == this)
        {
            float pv = currentEnergy;
            if (pv < 0)
            {
                pv = 0;
            }
            FindObjectOfType<UiManager>().changeDatas(type, chain.root.chain.maxEnergy, pv, resChemicals, resPhysical);
        }
    }


    public void OnClick()
    {


        if (!exist && hovered && currentSelected.chain.root.chain.energy > price && !isBuilding)
        {
            currentSelected.chain.root.chain.energy -= price;
            currentEnergy = 0;
            ColorBlock colors = GetComponent<UnityEngine.UI.Button>().colors;
            colors.normalColor = new Color(1f, 1f, 1f, 1f);
            GetComponent<UnityEngine.UI.Button>().colors = colors;
            StartCoroutine(build(getTimer()));
            if (type == currentSelected.type && !willDestroy)
            {
                currentSelected.chain.root.chain.corals.Add(this);
                currentSelected.chain.root.chain.maxEnergy += energyMax;
                isPartOfChain = true;
                chain.root = currentSelected.chain.root;
            }
            else if (!willDestroy)
            {
                currentSelected.childs.Add(this);
                chain.corals.Add(this);
                chain.maxEnergy += energyMax;
                isPartOfChain = true;
            }

            if (currentSelected != null)
            {
                currentSelected.HideNeighbors();
            }
        }
        else if (exist && !isBuilding)
        {
            if (currentSelected != null)
            {
                currentSelected.HideNeighbors();
            }
            currentSelected = this;




            if (leftNeighbor != null)
                ShowNeighbor(leftNeighbor, _GetColor(leftNeighbor));
            if (rightNeighbor != null)
                ShowNeighbor(rightNeighbor, _GetColor(rightNeighbor));
            if (topNeighbor != null)
                ShowNeighbor(topNeighbor, _GetColor(topNeighbor));
            if (bottomNeighbor != null)
                ShowNeighbor(bottomNeighbor, _GetColor(bottomNeighbor));
        }
    }

    void ShowNeighbor(GameObject neighbor, Color color)
    {
        if (neighbor != null && !neighbor.GetComponent<CoralController>().exist)
        {
            neighbor.GetComponent<CoralController>().hovered = true;
            UnityEngine.UI.Button button = neighbor.GetComponent<UnityEngine.UI.Button>();

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
            neighbor.GetComponent<CoralController>().hovered = false;
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

    private float getMultiplier()
    {
        float multiplier = 1;
        if (FindObjectOfType<GameManager>().getTemp(this.gameObject) < 1)
        {
            multiplier -= FindObjectOfType<GameManager>().getTemp(this.gameObject);
        }
        if (FindObjectOfType<GameManager>().waterAcid > resChemicals)
        {
            multiplier -= resChemicals / FindObjectOfType<GameManager>().waterAcid;
        }
        return multiplier;
    }



    private IEnumerator build(float time)
    {
        this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        ColorBlock colors = GetComponent<UnityEngine.UI.Button>().colors;

        colors.disabledColor = new Color(1f, 1f, 1f, 0.25f);
        GetComponent<UnityEngine.UI.Button>().colors = colors;
        isBuilding = true;
        yield return new WaitForSeconds(time / 3);
        colors.disabledColor = new Color(1f, 1f, 1f, 0.5f);
        GetComponent<UnityEngine.UI.Button>().colors = colors;
        yield return new WaitForSeconds(time / 3);
        if (willDestroy)
        {
            isBuilding = false;
            this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
            yield break;
        }
        else
        {
            colors.disabledColor = new Color(1f, 1f, 1f, 0.75f);
            GetComponent<UnityEngine.UI.Button>().colors = colors;
        }
        yield return new WaitForSeconds(time / 3);
        colors.disabledColor = new Color(1f, 1f, 1f, 1f);
        GetComponent<UnityEngine.UI.Button>().colors = colors;
        isBuilding = false;
        exist = true;
        this.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public float getTimer()
    {
        willDestroy = false;
        float _timer = energyMax * energyRegen;
        _timer = _timer / (resChemicals / FindObjectOfType<GameManager>().waterAcid);

        if (resChemicals / FindObjectOfType<GameManager>().waterAcid < 0.5)
        {
            willDestroy = true;
        }

        _timer = _timer / FindObjectOfType<GameManager>().getTemp(this.gameObject);

        if (resPhysical / FindObjectOfType<GameManager>().getTemp(this.gameObject) < 0.5)
        {
            willDestroy = true;
        }

        _timer = _timer * FindObjectOfType<GameManager>().getLight(this.gameObject);

        return _timer;
    }




    private Color _GetColor(GameObject _case)
    {
        float total = 0;


        if (FindObjectOfType<GameManager>().getTemp(_case) > 1)
        {
            total++;
        }
        if (!FindObjectOfType<GameManager>().row4.Contains(_case))
        {
            total++;
        }
        if (FindObjectOfType<GameManager>().waterAcid > resChemicals)
        {
            total++;
        }
        if (FindObjectOfType<GameManager>().waterSpeed > resPhysical)
        {
            total++;
        }



        if (FindObjectOfType<GameManager>().waterAcid > resChemicals * 2 || FindObjectOfType<GameManager>().getTemp(_case) > resPhysical * 2)
        {
            return new Color(.5f, 0f, 0f, 0.5f);
        }






        if (total == 1 || total == 2) return new Color(.5f, .5f, 0f, 0.5f);
        if (total >= 3) return new Color(1f, 0f, 0f, 0.5f);
        return new Color(0f, 1f, 0f, 0.5f);
    }




    public void destroy()
    {
        if (gameManager.row1[3] == this.gameObject)
        {
            FindAnyObjectByType<WinDefeatManager>().GameOver();
        }
        for (int i = 0; i < childs.Count(); i++)
        {
            childs[i].destroy();
        }
        chain.root.chain.energy -= energyMax;
        chain.root.chain.maxEnergy -= energyMax;
        exist = false;
        chain.root = this;
        ColorBlock cb = gameObject.GetComponent<UnityEngine.UI.Button>().colors;
        cb.normalColor = new Color(1f, 1f, 1f, 0);
        gameObject.GetComponent<UnityEngine.UI.Button>().colors = cb;
        childs.Clear();

    }


    public void Hide()
    {
        currentSelected.HideNeighbors();
    }

    public void AddBottle(BottlesController bottle)
    {
        if (!bottles.Contains(bottle))
            bottles.Add(bottle);
    }

    public void RemoveBottle(BottlesController bottle)
    {
        bottles.Remove(bottle);
    }

}
