using System.Collections;
using UnityEngine;

public class TouristBoat : MonoBehaviour
{
    [Header("Références")]
    public GameManager gameManager;
    public GameObject bottlePrefab;
    public RectTransform boatRect;
    public RectTransform gridParent;

    [Header("Paramètres")]
    [Range(0f, 1f)]
    public float spawnChance = 0.30f;
    public float boatSpeed = 250f;
    public float cellWidth = 80f;
    public float bottleFloorY = -145f;

    private bool boatRunning;

    void Start()
    {
        boatRect.gameObject.SetActive(false);
        gameManager.OnDayChanged += OnDayChanged;
    }

    void OnDestroy()
    {
        gameManager.OnDayChanged -= OnDayChanged;
    }

    private void OnDayChanged(int day, int week)
    {
        if (!boatRunning && Random.value <= spawnChance)
            StartCoroutine(RunBoat(Random.Range(1, 4)));
    }

    public void ForceSpawn()
    {
        if (!boatRunning)
            StartCoroutine(RunBoat(Random.Range(1, 4)));
    }

    private IEnumerator RunBoat(int bottleCount)
    {
        boatRunning = true;
        boatRect.gameObject.SetActive(true);

        float startX = -700f;
        float endX = 700f;

        float duration = Mathf.Abs(endX - startX) / boatSpeed;
        float elapsed = 0f;

        int maxCol = gameManager.row1.Length - 2;

        int dropIndex = 0;
        float[] dropTimes = new float[bottleCount];
        int[] dropCols = new int[bottleCount];

        for (int i = 0; i < bottleCount; i++)
        {
            dropCols[i] = Random.Range(0, maxCol + 1);
            dropTimes[i] = duration * 0.2f + i * 0.6f;
        }

        RectTransform parentRT = gridParent;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            Vector2 pos = boatRect.anchoredPosition;
            pos.x = Mathf.Lerp(startX, endX, elapsed / duration);
            boatRect.anchoredPosition = pos;

            while (dropIndex < bottleCount && elapsed >= dropTimes[dropIndex])
            {
                DropBottle(dropCols[dropIndex]);
                dropIndex++;
            }

            yield return null;
        }

        boatRect.gameObject.SetActive(false);
        boatRunning = false;
    }

    private void DropBottle(int col)
    {
        GameObject go = Instantiate(bottlePrefab, gridParent);

        RectTransform rt = go.GetComponent<RectTransform>();

        float x = -(gameManager.row1.Length - 1) * cellWidth / 2f + (col + 0.5f) * cellWidth;
        float y = boatRect.anchoredPosition.y;

        rt.anchoredPosition = new Vector2(x, y);

        go.GetComponent<BottlesController>().Init(
            gameManager,
            col,
            y,
            GetStopY(col)
        );
    }

    private float GetStopY(int col)
    {
        GameObject[][] allRows =
        {
            gameManager.row7,
            gameManager.row6,
            gameManager.row5,
            gameManager.row4,
            gameManager.row3,
            gameManager.row2,
            gameManager.row1
        };

        foreach (var row in allRows)
        {
            if (col >= row.Length || !row[col].activeSelf)
                continue;

            CoralController cc = row[col].GetComponent<CoralController>();

            if (cc != null && cc.exist)
            {
                RectTransform rt = row[col].GetComponent<RectTransform>();
                return rt.anchoredPosition.y;
            }
        }

        return bottleFloorY;
    }
}