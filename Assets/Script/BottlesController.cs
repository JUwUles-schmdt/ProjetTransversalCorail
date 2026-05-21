using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesController : MonoBehaviour
{
    [Header("Ref")]
    public GameManager gameManager;

    [Header("Fall")]
    public float fallSpeed = 150f;

    [Header("Position dans la grille")]
    public int colIndex = 0;

    public CoralController leftCoral;
    public CoralController rightCoral;

    private RectTransform rt;
    private bool isFalling = true;
    private float targetY;

    public static float EnergyMultiplier(int nbBottles)
    {
        if (nbBottles <= 0)
            return 1f;

        return 1f + 0.30f * nbBottles;
    }

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void Init(GameManager gm, int col, float startY, float stopY)
    {
        gameManager = gm;
        colIndex = col;
        targetY = stopY;

        Vector2 pos = rt.anchoredPosition;
        pos.y = startY;
        rt.anchoredPosition = pos;

        isFalling = true;

        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        while (isFalling)
        {
            float newY = rt.anchoredPosition.y - fallSpeed * Time.deltaTime;

            if (newY <= targetY)
            {
                newY = targetY;
                isFalling = false;
            }

            rt.anchoredPosition = new Vector2(
                rt.anchoredPosition.x,
                newY
            );

            yield return null;
        }

        RegisterOnCorals();
    }

    private void RegisterOnCorals()
    {
        if (colIndex >= 0 && colIndex < gameManager.row1.Length)
            leftCoral = gameManager.row1[colIndex].GetComponent<CoralController>();

        if (colIndex + 1 < gameManager.row1.Length)
            rightCoral = gameManager.row1[colIndex + 1].GetComponent<CoralController>();

        leftCoral?.AddBottle(this);
        rightCoral?.AddBottle(this);
    }

    public void Remove()
    {
        leftCoral?.RemoveBottle(this);
        rightCoral?.RemoveBottle(this);

        Destroy(gameObject);
    }
}