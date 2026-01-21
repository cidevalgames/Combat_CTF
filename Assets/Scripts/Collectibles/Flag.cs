using UnityEngine;
using UnityEngine.UI;

public class Flag : Collectible
{
    GridBox winBox = null;

    [Header("Win box colors")]
    [SerializeField] private Color winBoxColor;

    private void Awake()
    {
        type = CollectibleType.Flag;
    }

    public override void OnCollect(PlayerCollection playerCollection)
    {
        transform.SetParent(playerCollection.transform);

        Debug.Log("Attacker collected flag");
    }

    /// <summary>
    /// Set the win box where the attacker needs to bring the flag.
    /// </summary>
    /// <param name="pos">Box position.</param>
    public void SetWinBox(Vector2Int pos)
    {
        winBox = GridManager.Instance.GetBox(pos);
        winBox.transform.GetChild(0).GetComponent<Image>().color = winBoxColor;
    }

    public GridBox GetWinBox()
    {
        return winBox;
    }
}
