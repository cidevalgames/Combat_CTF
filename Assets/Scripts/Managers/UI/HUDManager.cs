using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField] private Button buttonAttackerNext;
    [SerializeField] private Button buttonDefenderNext;

    private bool _initialized = false;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        Init();
    }

    private void Init()
    {
        if (_initialized)
            return;

        // Add listeners on buttons
        buttonAttackerNext.onClick.AddListener(OnClickNextButton);
        buttonDefenderNext.onClick.AddListener(OnClickNextButton);

        // Disable buttons interaction
        buttonAttackerNext.interactable = false;
        buttonDefenderNext.interactable = false;

        _initialized = true;
    }

    public void EnableButtonAttackerNext()
    {
        buttonAttackerNext.interactable = true;
        buttonDefenderNext.interactable = false;
    }

    public void EnableButtonDefenderNext()
    {
        buttonAttackerNext.interactable = false;
        buttonDefenderNext.interactable = true;
    }

    private void OnClickNextButton()
    {
        GameManager.Instance.NextPlayerTurn();
    }
}
