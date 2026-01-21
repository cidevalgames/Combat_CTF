using UnityEngine;

public class Wall : MonoBehaviour
{
    public void PlaceWall(Vector2Int pos)
    {
        GridBox box = GridManager.Instance.GetBox(pos);
        transform.position = box.transform.position;
        box.gridObstacle = GetComponent<GridObstacle>();
    }
}
