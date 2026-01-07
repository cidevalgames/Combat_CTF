using UnityEngine;
using UnityEngine.UI;

public class GridBox : MonoBehaviour
{
    public Vector2Int boxCoordinates = Vector2Int.zero;
    public GridObstacle gridObstacle = null;

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
        if (!gridObstacle)
        {
            PlayerMovement playerMovement = GameManager.Instance.currentlyPlayingPlayer.GetComponent<PlayerMovement>();
            playerMovement.MovePlayer(boxCoordinates);

            return;
        }

        if (gridObstacle.GetType() == typeof(PlayerGridObstacle))
        {
            if (GameManager.Instance.IsAttackerTurn())
            {
                // Attack target
                GameManager.Instance.GetAttacker().GetComponent<PlayerAttack>().AttackTarget(GameManager.Instance.GetDefender());
                Debug.Log($"{GameManager.Instance.currentlyPlayingPlayer.name} attacks {gridObstacle.name}.");

                GameManager.Instance.NextPlayerTurn();
            }

            return;
        }
    }
}
