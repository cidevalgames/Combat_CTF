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
}
