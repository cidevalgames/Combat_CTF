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
            //Debug.Log($"Current scene: {SceneManager.GetSceneAt(i).name}");

            if (SceneManager.GetSceneAt(i).name == "FeaturePlayer")
                StartCoroutine(StartGame());
        }   
    }

    private IEnumerator StartGame()
    {
        if (_players[0] == null || _players[1] == null)
            _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in _players)
        {
            if (p.name == "Player Attacker")
                _attacker = p;
            else if (p.name == "Player Defender")
                _defender = p;

            p.GetComponent<SpriteRenderer>().enabled = false;
        }

        currentlyPlayingPlayer = _attacker;

        // Spawn players
        GridManager gridManager = GridManager.Instance;

        yield return new WaitUntil(() => gridManager.boxes.Length > 0);

        yield return null;

        foreach (GameObject p in _players)
        {
            p.GetComponent<SpriteRenderer>().enabled = true;
        }

        Vector2Int gridSize = gridManager.GetGridSize();

        //_attacker.GetComponent<PlayerSpawn>().SpawnPlayer(Vector2Int.zero);
        //_defender.GetComponent<PlayerSpawn>().SpawnPlayer(new Vector2Int(gridSize.x - 1, gridSize.y - 1));

        // Test for attack
        _attacker.GetComponent<PlayerSpawn>().SpawnPlayer(new Vector2Int(4, 5));
        _defender.GetComponent<PlayerSpawn>().SpawnPlayer(new Vector2Int(5, 5));

        EnablePlayer(_attacker);
    }

    public void NextPlayerTurn()
    {
        DisablePlayer(currentlyPlayingPlayer);

        if (currentlyPlayingPlayer == _attacker)
            currentlyPlayingPlayer = _defender;
        else if (currentlyPlayingPlayer == _defender)
            currentlyPlayingPlayer = _attacker;

        EnablePlayer(currentlyPlayingPlayer);
    }

    private void EnablePlayer(GameObject player)
    {
        // Enable movement
        player.GetComponent<PlayerMovement>().EnableMovement();
    }

    private void DisablePlayer(GameObject player)
    {
        // Disable movement
        player.GetComponent<PlayerMovement>().DisableMovement();
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
