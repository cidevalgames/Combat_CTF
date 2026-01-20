using UnityEngine;

public class Flag : Collectible
{
    private void Awake()
    {
        type = CollectibleType.Flag;
    }

    public override void OnCollect(PlayerCollection playerCollection)
    {
        transform.SetParent(playerCollection.transform);

        Debug.Log("Attacker collected flag");
    }
}
