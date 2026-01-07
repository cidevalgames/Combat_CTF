using UnityEngine;
using UnityEngine.UI;

public class GridBox : MonoBehaviour
{
    public Vector2Int boxCoordinates = Vector2Int.zero;

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
        PlayerMovement playerMovement = GameManager.Instance.currentlyPlayingPlayer.GetComponent<PlayerMovement>();

        playerMovement.MovePlayer(boxCoordinates);
    }
}
