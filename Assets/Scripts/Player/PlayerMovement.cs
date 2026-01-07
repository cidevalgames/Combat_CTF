using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0)] private int movementRange = 1;

    public Vector2Int playerPosition { get; private set; }

    public bool _canMove { get; private set; } = false;

    private void Awake()
    {
        
    }

    public void OnDisable()
    {
        DisableMovement();
    }

    public void EnableMovement()
    {
        for (int i = 1; i <= movementRange; i++)
        {
            // Left movement
            GridManager.Instance.EnableBoxInteraction(playerPosition + Vector2Int.left);
            // Right movement
            GridManager.Instance.EnableBoxInteraction(playerPosition + Vector2Int.right);
            // Up movement
            GridManager.Instance.EnableBoxInteraction(playerPosition - Vector2Int.up);
            // Down movement
            GridManager.Instance.EnableBoxInteraction(playerPosition - Vector2Int.down);
        }

        _canMove = true;
    }

    public void DisableMovement()
    {
        GridManager.Instance.DisableBoxesInteraction();

        _canMove = false;
    }

    public void SetPlayerPosition(Vector2Int pos)
    {
        playerPosition = pos;
    }

    public void MovePlayer(Vector2Int newPos)
    {
        GridBox box = GridManager.Instance.boxes[newPos.x, newPos.y];
        transform.position = box.transform.position;

        playerPosition = newPos;
    }
}
