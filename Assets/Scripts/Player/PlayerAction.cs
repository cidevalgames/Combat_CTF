using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public void DoAction(GridObstacle gridObstacle, Vector2Int boxCoordinates)
    {
        GridObstacle.GridObstacleType gridObstacleType = GridObstacle.GridObstacleType.None;

        if (gridObstacle)
            gridObstacleType = gridObstacle.GetGridObstacleType();

        if (gridObstacleType == GridObstacle.GridObstacleType.None)
        {
            // Move player if there is no grid obstacle
            GameObject currentlyPlayingPlayer = GameManager.Instance.currentlyPlayingPlayer;
            PlayerMovement playerMovement = currentlyPlayingPlayer.GetComponent<PlayerMovement>();
            playerMovement.MovePlayer(boxCoordinates);

            return;
        }

        if (GameManager.Instance.IsAttackerTurn())
        {
            if (gridObstacleType == GridObstacle.GridObstacleType.Player)
            {
                GameObject attacker = GameManager.Instance.GetAttacker();
                PlayerAttack playerAttack = attacker.GetComponent<PlayerAttack>();

                // Attack target
                playerAttack.AttackTarget(GameManager.Instance.GetDefender());
                Debug.Log($"{GameManager.Instance.currentlyPlayingPlayer.name} attacks {gridObstacle.name}.");

                GameManager.Instance.NextPlayerTurn();

                return;
            }
        }
        else if (GameManager.Instance.IsDefenderTurn())
        {

        }
    }
}
