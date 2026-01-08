using UnityEngine;

public class PlayerGridObstacle : GridObstacle
{
    private void Awake()
    {
        gridObstacleType = GridObstacleType.Player;
    }
}
