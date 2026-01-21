using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int damageAmount = 20;

    public void PlaceTrap(Vector2Int pos)
    {
        GridBox box = GridManager.Instance.GetBox(pos);
        transform.position = box.transform.position;
        box.gridObstacle = GetComponent<GridObstacle>();
    }

    public void DamagePlayer(PlayerLife playerLife)
    {
        playerLife.Damage(damageAmount);
        Destroy(gameObject);
    }
}
