using System.Collections.Generic;
using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    public static CollectiblesSpawner Instance;

    [SerializeField] private CollectiblesSpawnData[] collectiblesSpawnData;

    public List<Collectible> spawnedCollectibles { get; private set; } = new List<Collectible>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void SpawnCollectibles()
    {
        foreach (var o in collectiblesSpawnData)
        {
            foreach (var p in o.positions)
            {
                Collectible newCollectible = Instantiate(o.collectible.prefab, transform).GetComponent<Collectible>();
                newCollectible.SpawnCollectible(p);
                spawnedCollectibles.Add(newCollectible);
            }
        }
    }

    public T GetSpawnedCollectible<T>() where T : Collectible
    {
        foreach (var s in spawnedCollectibles)
        {
            if (s is T)
                return s as T;
        }

        return null;
    }

    [System.Serializable]
    public class CollectiblesSpawnData
    {
        public CollectibleData collectible;
        public Vector2Int[] positions;
    }
}
