using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button nextButton;

    public bool isPlayerTurn { get; private set; } = false;

    private bool _initalized = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_initalized)
            return;

        nextButton.onClick.AddListener(OnClickNextButton);

        _initalized = true;
    }

    public void SetPlayerTurn(bool isPlayerTurn)
    {
        this.isPlayerTurn = isPlayerTurn;

        if (isPlayerTurn)
        {
            GetComponent<PlayerMovement>().EnableMovement();

            // Enable attack if attacker
            GetComponent<PlayerAttack>()?.EnableAttack();
        }
        else
        {
            // Disable movement
            GetComponent<PlayerMovement>().DisableMovement();

            // Enable attack if attacker
            GetComponent<PlayerAttack>()?.DisableAttack();
        }
    }

    #region UI
    private void OnClickNextButton()
    {
        GameManager.Instance.NextPlayerTurn();
    }
    #endregion
}
