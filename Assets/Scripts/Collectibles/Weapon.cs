using UnityEngine;

public class Weapon : Collectible
{
    [SerializeField, Range(0, 9)] private int ammunitionAmount = 0;

    private void Awake()
    {
        type = CollectibleType.Weapon;
    }

    public override void OnCollect(PlayerCollection player)
    {
        transform.SetParent(player.transform);
    }

    public void AddAmmunition(int amount)
    {
        ammunitionAmount += amount;
    }

    public void RemoveAmmunition(int amount)
    {
        ammunitionAmount -= amount;
    }

    public int GetAmmunitionAmount()
    {
        return ammunitionAmount;
    }
}
