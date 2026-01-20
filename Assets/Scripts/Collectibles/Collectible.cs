using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] private CollectableBy collectableBy;

    private protected CollectibleType type;

    public bool TryCollect(GameObject player)
    {
        GameObject attacker;
        GameObject defender;

        switch (collectableBy)
        {
            case CollectableBy.Attacker:
                attacker = GameManager.Instance.GetAttacker();

                if (player == attacker)
                {
                    return true;
                }

                break;

            case CollectableBy.Defender:
                defender = GameManager.Instance.GetDefender();

                if (player == defender)
                {
                    return true;
                }

                break;

            case CollectableBy.Both:
                defender = GameManager.Instance.GetDefender();
                attacker = GameManager.Instance.GetAttacker();

                if (player == defender || player == attacker)
                {
                    return true;
                }

                break;
        }

        return false;
    }

    public void SpawnCollectible(Vector2Int spawnPosition)
    {
        GridBox box = GridManager.Instance.boxes[spawnPosition.x, spawnPosition.y];
        transform.position = box.transform.position;
        box.collectible = this;
    }

    public virtual void OnCollect(PlayerCollection playerCollection)
    {
        Destroy(gameObject);
    }

    public enum CollectableBy
    {
        None,
        Attacker,
        Defender,
        Both
    }
}
