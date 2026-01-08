using UnityEngine;

public abstract class GridObstacle : MonoBehaviour
{
    protected GridObstacleType gridObstacleType = GridObstacleType.None;

    public GridObstacleType GetGridObstacleType()
    {
        return gridObstacleType;
    }

    public enum GridObstacleType
    {
        None,
        Player,
        Trap,
        Wall,
    }
}
