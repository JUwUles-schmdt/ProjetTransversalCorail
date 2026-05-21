using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingRodButton : MonoBehaviour
{
    [Header("Ref")]
    public GameManager gameManager;
    public TMP_Text chargesText;
    public Image buttonImage;

    [Header("Charges")]
    public int maxCharges = 3;

    [Header("Visuels")]
    public Color activeColor = new Color(1f, 0.85f, 0.3f);
    public Color inactiveColor = new Color(0.6f, 0.6f, 0.6f);
    public Color emptyColor = new Color(0.3f, 0.3f, 0.3f);

    private int charges;
    private bool isActive = false;

    public static FishingRodButton Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        charges = maxCharges;
    }

    void Start()
    {
        UpdateUI();
    }

    public void OnButtonClicked()
    {
        if (charges <= 0)
        {
            return;
        }

        isActive = !isActive;
        UpdateUI();
    }

    public void TryFishBottle()
    {
        if (!isActive || charges <= 0) return;

        BottlesController[] bottles = FindObjectsByType<BottlesController>(FindObjectsSortMode.None);

        if (bottles == null || bottles.Length == 0) return;

        foreach (BottlesController b in bottles)
        {
            b.Remove();
        }

        charges--;
        isActive = false;
        UpdateUI();
    }

    public void Recharge(int amount = 1)
    {
        charges = Mathf.Min(charges + amount, maxCharges);
        UpdateUI();
    }

    public bool IsActive => isActive;

    private void UpdateUI()
    {
        if (chargesText != null)
            chargesText.text = $"x{charges}";

        if (buttonImage != null)
        {
            if (charges <= 0)
                buttonImage.color = emptyColor;
            else if (isActive)
                buttonImage.color = activeColor;
            else
                buttonImage.color = inactiveColor;
        }

    }
}
