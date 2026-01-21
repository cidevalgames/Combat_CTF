using UnityEngine;

public class Trap : Obstacle
{
    [SerializeField, Range(1, 100)] private int damageAmount = 20;

    public void DamagePlayer(PlayerLife playerLife)
    {
        playerLife.Damage(damageAmount);
        Destroy(gameObject);
    }
}
