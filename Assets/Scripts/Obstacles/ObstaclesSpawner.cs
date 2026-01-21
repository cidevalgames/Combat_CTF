using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public static ObstaclesSpawner Instance;

    [SerializeField] private ObstacleSpawnData[] obstacleSpawnData;

    public List<Obstacle> spawnedObstacles { get; private set; } = new List<Obstacle>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void SpawnObstacles()
    {
        foreach (var o in obstacleSpawnData)
        {
            foreach (var p in o.positions)
            {
                Obstacle newObstacle = Instantiate(o.obstacle.prefab, transform).GetComponent<Obstacle>();
                newObstacle.PlaceObstacle(p);
                spawnedObstacles.Add(newObstacle);
            }
        }
    }

    public T GetSpawnedObstacle<T>() where T : Obstacle
    {
        foreach (var s in spawnedObstacles)
        {
            if (s is T)
                return s as T;
        }

        return null;
    }

    public T[] GetSpawnedObstacles<T>() where T : Obstacle
    {
        List<T> obstacles = new List<T>();

        foreach (var s in spawnedObstacles)
        {
            if (s is T)
                obstacles.Add(s as T);
        }

        return obstacles.ToArray();
    }

    [System.Serializable]
    public class ObstacleSpawnData
    {
        public ObstacleData obstacle;
        public Vector2Int[] positions;
    }
}
