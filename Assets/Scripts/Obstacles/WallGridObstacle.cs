using UnityEngine;

public class WallGridObstacle : GridObstacle
{
    private void Awake()
    {
        gridObstacleType = GridObstacleType.Wall;
    }
}
