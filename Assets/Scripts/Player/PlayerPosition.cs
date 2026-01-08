using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Vector2Int position { get; private set; }

    public void SetPlayerPosition(Vector2Int pos)
    {
        position = pos;
    }
}
