using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Grid size")]
    [SerializeField, Min(1)] private int gridWidth = 10;
    [SerializeField, Min(1)] private int gridHeight = 6;

    [Header("UI")]
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private GameObject gridBoxPrefab;

    [Header("Debug")]
    [SerializeField] private bool showBoxesCoordinates = false;

    public GridBox[,] boxes { get; private set; }

    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        StoreBoxes();
    }

    /// <summary>
    /// Store boxes in the 2D array 'boxes'.
    /// </summary>
    private void StoreBoxes()
    {
        boxes = new GridBox[gridWidth, gridHeight];

        foreach (GridBox b in grid.GetComponentsInChildren<GridBox>())
        {
            boxes[b.boxCoordinates.x, b.boxCoordinates.y] = b;
        }
    }

    public Vector2Int GetGridSize()
    {
        return new Vector2Int(gridWidth, gridHeight);
    }

    [ContextMenu("Generate grid")]
    private void GenerateGrid()
    {
        ResetGrid();

        Transform gridTransform = grid.transform;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject boxInstance = PrefabUtility.InstantiatePrefab(gridBoxPrefab, gridTransform) as GameObject;
                GridBox box = boxInstance.GetComponent<GridBox>();

                StoreBoxes();

                Vector2Int coordinates = new Vector2Int(x, y);
                box.boxCoordinates = coordinates;
                box.name += $" ({coordinates.x};{coordinates.y})";

#if UNITY_EDITOR
                if (showBoxesCoordinates)
                {
                    box.GetComponentInChildren<TextMeshProUGUI>().text = $"{coordinates.x};{coordinates.y}";
                }
                else
                {
                    box.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
#endif
            }
        }

        AdaptGridLayoutProperties();
    }

    [ContextMenu("Reset grid")]
    private void ResetGrid()
    {
        Transform gridTransform = grid.transform;

        var tempList = gridTransform.Cast<Transform>().ToList();

        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    /// <summary>
    /// Adapt grid layout properties of grid to set cell size.
    /// </summary>
    private void AdaptGridLayoutProperties()
    {
        float cellHeight = grid.GetComponent<RectTransform>().rect.height / gridHeight;

        grid.cellSize = Vector2.one * cellHeight;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = gridWidth;
    }
}
