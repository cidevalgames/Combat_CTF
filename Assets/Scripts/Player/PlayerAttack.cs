using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;
    [SerializeField, Min(0)] private int attackRange = 1;

    private PlayerPosition _playerPosition;

    private bool _canAttack = false;

    private void Awake()
    {
        _playerPosition = GetComponent<PlayerPosition>();
    }

    public void AttackTarget(GameObject target)
    {
        if (target.GetComponent<PlayerLife>())
        {
            target.GetComponent<PlayerLife>().Damage(attackDamage);
        }

        DisableAttack();
    }

    public void EnableAttack()
    {
        Vector2Int playerPos = _playerPosition.position;

        for (int i = 1; i <= attackRange; i++)
        {
            Vector2Int direction;

            direction = Vector2Int.zero;

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

                if (!currentGridBox.gridObstacle)
                    continue;

                if (currentGridBox.gridObstacle.GetGridObstacleType() == GridObstacle.GridObstacleType.Player)
                {
                    GridManager.Instance.EnableBoxInteraction(playerPos + direction * i);
                }
            }
        }

        _canAttack = true;
    }

    public void DisableAttack()
    {
        GridManager.Instance.DisableBoxesInteraction();

        _canAttack = false;
    }
}
