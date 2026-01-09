using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxLife = 100;

    [Header("UI")]
    [SerializeField] Slider lifeSlider;

    public int Life 
    { 
        get
        {
            return _life;
        }
        private set
        {
            _life = Mathf.Clamp(value, 0, maxLife);

            UpdateHealthBar();

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

        SceneLoader.Instance.ReloadGame();
    }

    public void HealToMax()
    {
        Life = maxLife;
    }

    private void UpdateHealthBar()
    {
        lifeSlider.value = Mathf.InverseLerp(0, maxLife, Life);
        lifeSlider.GetComponentInChildren<TextMeshProUGUI>().text = $"{Life} / {maxLife}";
    }
}
