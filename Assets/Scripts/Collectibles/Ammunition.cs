using UnityEngine;

public class Ammunition : Collectible
{
    [SerializeField, Range(1, 5)] private int ammunitionAmount = 3;

    private void Awake()
    {
        type = CollectibleType.Ammunition;
    }

    public override void OnCollect(PlayerCollection playerCollection)
    {
        // Add ammunition to weapon
        Weapon w = playerCollection.GetCollectible<Weapon>() as Weapon;
        w.AddAmmunition(ammunitionAmount);

        base.OnCollect(playerCollection);
    }
}
