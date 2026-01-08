using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0)] private int movementRange = 1;

    public bool _canMove { get; private set; } = false;

    private PlayerPosition _playerPosition;

    private void Awake()
    {
        _playerPosition = GetComponent<PlayerPosition>();
    }

    public void EnableMovement()
    {
        Vector2Int playerPos = _playerPosition.position;
        Vector2Int direction = Vector2Int.zero;

        for (int i = 1; i <= movementRange; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        direction = Vector2Int.left;
                        break;
                    case 1:
                        direction = Vector2Int.right;
                        break;
                    case 2:
                        direction = Vector2Int.up;
                        break;
                    case 3:
                        direction = Vector2Int.down;
                        break;
                }

                GridBox currentGridBox = GridManager.Instance.GetBox(playerPos + direction * i);

                if (!currentGridBox)
                    continue;

                // If there is no grid obstacle
                if (!currentGridBox.gridObstacle)
                {
                    GridManager.Instance.EnableBoxInteraction(playerPos + direction * i);
                }
            }
        }

        _canMove = true;
    }

    public void DisableMovement()
    {
        GridManager.Instance.DisableBoxesInteraction();

        _canMove = false;
    }

    public void MovePlayer(Vector2Int newPos)
    {
        Vector2Int playerPos = _playerPosition.position;

        GridBox oldBox = GridManager.Instance.boxes[playerPos.x, playerPos.y];
        oldBox.gridObstacle = null;

        GridBox box = GridManager.Instance.boxes[newPos.x, newPos.y];
        transform.position = box.transform.position;
        box.gridObstacle = GetComponent<GridObstacle>();

        _playerPosition.SetPlayerPosition(newPos);

        DisableMovement();

        if (GameManager.Instance.IsAttackerTurn())
        {
            GameManager.Instance.GetAttacker().GetComponent<PlayerAttack>().EnableAttack();
        }
    }
}
