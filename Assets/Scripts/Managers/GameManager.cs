using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject currentlyPlayingPlayer { get; private set; }

    private GameObject[] _players = new GameObject[2];

    private GameObject _attacker;
    private GameObject _defender;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        // Only for debug
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log($"Current scene: {SceneManager.GetSceneAt(i).name}");

            if (SceneManager.GetSceneAt(i).name == "FeaturePlayer")
                StartCoroutine(StartGame());
        }   
    }

    private IEnumerator StartGame()
    {
        Debug.Log("Start game");

        if (_players[0] == null || _players[1] == null)
            _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in _players)
        {
            if (p.name == "Player Attacker")
                _attacker = p;
            else if (p.name == "Player Defender")
                _defender = p;
        }

        currentlyPlayingPlayer = _attacker;

        // Spawn players
        GridManager gridManager = GridManager.Instance;

        yield return new WaitUntil(() => gridManager.boxes.Length > 0);

        yield return null;

        Debug.Log(gridManager.boxes.Length);

        Vector2Int gridSize = gridManager.GetGridSize();

        _attacker.GetComponent<PlayerSpawn>().SpawnPlayer(Vector2Int.zero);
        _defender.GetComponent<PlayerSpawn>().SpawnPlayer(new Vector2Int(gridSize.x - 1, gridSize.y - 1));

        // Enable movement for player 1
        currentlyPlayingPlayer.GetComponent<PlayerMovement>().EnableMovement();
    }

    /// <summary>
    /// Get player by index.
    /// </summary>
    /// <param name="index">Index must be between 0 and 1.</param>
    public GameObject[] GetPlayers()
    {
        return _players;
    }

    public GameObject GetAttacker()
    {
        return _attacker;
    }

    public GameObject GetDefenser()
    {
        return _defender;
    }
}
