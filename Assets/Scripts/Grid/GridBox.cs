using UnityEngine;
using UnityEngine.UI;

public class GridBox : MonoBehaviour
{
    public Vector2Int boxCoordinates = Vector2Int.zero;
    public GridObstacle gridObstacle = null;
    public Collectible collectible = null;

    private Button _button;

    private bool _initialized = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_initialized) return;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameObject currentlyPlayingPlayer = GameManager.Instance.currentlyPlayingPlayer;
        PlayerAction playerAction = currentlyPlayingPlayer.GetComponent<PlayerAction>();

        // Do player action
        playerAction.DoAction(gridObstacle, boxCoordinates);
    }
}
