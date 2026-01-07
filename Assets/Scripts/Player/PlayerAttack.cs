using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 20;

    public void AttackTarget(GameObject target)
    {
        if (target.GetComponent<PlayerLife>())
        {
            target.GetComponent<PlayerLife>().Damage(attackDamage);
        }
    }
}
