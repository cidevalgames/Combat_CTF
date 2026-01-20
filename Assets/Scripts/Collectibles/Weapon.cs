using UnityEngine;

public class Weapon : Collectible
{
    private void Awake()
    {
        type = CollectibleType.Weapon;
    }

    public override void OnCollect(PlayerCollection player)
    {
        transform.SetParent(player.transform);
    }
}
