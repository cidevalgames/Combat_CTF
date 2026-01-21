using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector2Int _obstaclePosition = Vector2Int.zero;

    public virtual void PlaceObstacle(Vector2Int pos)
    {
        GridBox box = GridManager.Instance.GetBox(pos);
        transform.position = box.transform.position;
        box.gridObstacle = GetComponent<GridObstacle>();
        _obstaclePosition = pos;
    }    

    public Vector2Int GetObstaclePosition() { return _obstaclePosition; }
}
