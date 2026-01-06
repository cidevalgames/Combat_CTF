using System.Linq;
using Unity.Mathematics;
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

    [ContextMenu("Generate grid")]
    private void GenerateGrid()
    {
        ResetGrid();

        Transform gridTransform = grid.transform;

        for (int i = 0; i < gridWidth * gridHeight; i++)
        {
            GameObject.Instantiate(gridBoxPrefab, gridTransform);
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

    private void AdaptGridLayoutProperties()
    {
        float cellHeight = grid.GetComponent<RectTransform>().rect.height / gridHeight;

        grid.cellSize = Vector2.one * cellHeight;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = gridWidth;
    }
}
