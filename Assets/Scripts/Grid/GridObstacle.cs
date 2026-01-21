using UnityEngine;

public class GridObstacle : MonoBehaviour
{
    [SerializeField] private GridObstacleType gridObstacleType = GridObstacleType.None;

    public GridObstacleType GetGridObstacleType()
    {
        return gridObstacleType;
    }
}
