using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    private List<Collectible> collection = new();

    public void Collect(Collectible collectible)
    {
        // Return if player can't collect this collectible
        if (!collectible.TryCollect(gameObject))
            return;

        collectible.OnCollect(this);

        collection.Add(collectible);
    }

    public bool ContainsCollectible<TCollectible>() where TCollectible : Collectible { return GetCollectible<TCollectible>() != null; }

    public Collectible GetCollectible<TCollectible>() where TCollectible : Collectible
    {
        foreach (var collectible in collection)
        {
            if (collectible is TCollectible) return (TCollectible)collectible;
        }

        return null;
    }
}
