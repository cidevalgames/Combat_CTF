using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxLife = 100;

    public int Life 
    { 
        get
        {
            return _life;
        }
        private set
        {
            _life = Mathf.Clamp(value, 0, maxLife);

            if (value <= 0)
                Die();
        }
    }

    private int _life;

    public void Damage(int damage)
    {
        Life -= damage;
    }

    public void Heal(int heal)
    {
        Life += heal;
    }

    private void Die()
    {
        Debug.Log($"{name} died.");

        Destroy(gameObject);
    }
}
