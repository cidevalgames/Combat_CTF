using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void SpawnPlayer(Vector2Int spawnPosition)
    {
        GridBox box = GridManager.Instance.boxes[spawnPosition.x, spawnPosition.y];
        transform.position = box.transform.position;

        GetComponent<PlayerMovement>().SetPlayerPosition(spawnPosition);

        box.gridObstacle = GetComponent<GridObstacle>();

        //Debug.Log($"Spawn player at position {spawnPosition}");
    }

#if UNITY_EDITOR
    [ContextMenu("Spawn player at random position")]
    private void SpawnPlayerAtRandomPosition()
    {
        Vector2Int gridSize = GridManager.Instance.GetGridSize();

        int x = Random.Range(0, gridSize.x - 1);
        int y = Random.Range(0, gridSize.y - 1);

        Vector2Int spawnPosition = new Vector2Int(x, y);

        SpawnPlayer(spawnPosition);
    }
#endif
}
