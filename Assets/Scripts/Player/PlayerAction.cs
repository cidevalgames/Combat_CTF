using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private GameObject _attacker;
    private GameObject _defender;
    private PlayerAttack _playerAttack;
    private PlayerShoot _playerShoot;

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

            PlayerCollection playerCollection = currentlyPlayingPlayer.GetComponent<PlayerCollection>();

            if (playerCollection.ContainsCollectible<Flag>())
            {
                Flag flag = currentlyPlayingPlayer.GetComponent<PlayerCollection>().GetCollectible<Flag>() as Flag;

                if (flag.GetWinBox() == GridManager.Instance.GetBox(boxCoordinates))
                {
                    GameManager.Instance.WinGame(gameObject);
                }
            }

            return;
        }

        if (!_attacker)
            _attacker = GameManager.Instance.GetAttacker();

        if (!_defender)
            _defender = GameManager.Instance.GetDefender();

        if (GameManager.Instance.IsAttackerTurn())
        {
            if (gridObstacleType == GridObstacle.GridObstacleType.Player)
            {
                if (_attacker.GetComponent<PlayerCollection>().ContainsCollectible<Weapon>())
                {
                    if (!_playerShoot)
                        _playerShoot = _attacker.GetComponent<PlayerShoot>();

                    // Shoot target
                    _playerShoot.ShootTarget(_defender);
                }
                else
                {
                    if (!_playerAttack)
                        _playerAttack = _attacker.GetComponent<PlayerAttack>();

                    // Attack target
                    _playerAttack.AttackTarget(_defender);
                    Debug.Log($"{_attacker} attacks {gridObstacle.name}.");
                }

                GameManager.Instance.NextPlayerTurn();

                return;
            }
        }
        else if (GameManager.Instance.IsDefenderTurn())
        {

        }
    }
}
