using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField, Min(1)] private int shootRange = 3;
    [SerializeField, Min(5)] private int shootDamage = 20;

    private PlayerPosition _playerPosition;
    private PlayerCollection _playerCollection;

    private bool _canShoot = false;

    private void Awake()
    {
        _playerPosition = GetComponent<PlayerPosition>();
        _playerCollection = GetComponent<PlayerCollection>();
    }

    public void EnableShoot()
    {
        if (!_playerCollection.ContainsCollectible<Weapon>())
            return;

        Weapon w = _playerCollection.GetCollectible<Weapon>() as Weapon;

        if (w.GetAmmunitionAmount() <= 0)
            return;

        Vector2Int playerPos = _playerPosition.position;

        for (int i = 1; i <= shootRange; i++)
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

        _canShoot = true;
    }

    public void DisableShoot()
    {
        GridManager.Instance.DisableBoxesInteraction();

        _canShoot = false;
    }

    public void ShootTarget(GameObject target)
    {
        if (target.GetComponent<PlayerLife>())
        {
            target.GetComponent<PlayerLife>().Damage(shootDamage);
        }

        Weapon w = _playerCollection.GetCollectible<Weapon>() as Weapon;
        w.RemoveAmmunition(1);

        DisableShoot();
    }
}
