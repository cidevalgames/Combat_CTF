using UnityEngine;

public class Ammunition : Collectible
{
    private void Awake()
    {
        type = CollectibleType.Ammunition;
    }

    public override void OnCollect(PlayerCollection playerCollection)
    {
        // TODO: Add ammunition to weapon

        base.OnCollect(playerCollection);
    }
}
