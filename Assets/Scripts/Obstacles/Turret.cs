using System.Collections;
using UnityEngine;

public class Turret : Obstacle
{
    [SerializeField, Range(1, 100)] private int damageAmount = 10;
    [SerializeField] private GameObject bulletPrefab;

    private Orientation _orientation;

    public override void PlaceObstacle(Vector2Int pos)
    {
        base.PlaceObstacle(pos);

        if (pos.x < GridManager.Instance.GetGridSize().x / 2)
        {
            _orientation = Orientation.Right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            _orientation = Orientation.Left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public IEnumerator Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab).transform;

        if (_orientation == Orientation.Left)
            bullet.rotation = Quaternion.Euler(0, 0, 90);
        else if (_orientation == Orientation.Right)
            bullet.rotation = Quaternion.Euler(0, 0, -90);

        yield return TryShootBoxes(bullet);

        Destroy(bullet.gameObject);
    }

    private IEnumerator TryShootBoxes(Transform bullet)
    {
        int gridSizeX = GridManager.Instance.GetGridSize().x;

        for (int i = 1; i <= gridSizeX; i++)
        {
            Vector2Int direction;

            direction = Vector2Int.zero;

            if (_orientation == Orientation.Left)
                direction = Vector2Int.left;
            else if (_orientation == Orientation.Right)
                direction = Vector2Int.right;

            GridBox currentGridBox = GridManager.Instance.GetBox(GetObstaclePosition() + direction * i);

            if (!currentGridBox)
                break;

            //Debug.Log($"Try to shoot {currentGridBox.boxCoordinates}");

            if (currentGridBox.gridObstacle)
            {
                switch (currentGridBox.gridObstacle.GetGridObstacleType())
                {
                    case GridObstacleType.Player:
                        if (currentGridBox.gridObstacle.gameObject == GameManager.Instance.GetDefender())
                            break;

                        currentGridBox.gridObstacle.GetComponent<PlayerLife>().Damage(damageAmount);

                        yield break;

                    case GridObstacleType.Wall:
                        yield break;
                }
            }

            bullet.position = currentGridBox.transform.position;

            yield return new WaitForSeconds(.1f);
        }
    }

    private enum Orientation
    {
        Left,
        Right
    }
}
