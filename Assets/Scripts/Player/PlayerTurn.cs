using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public bool isPlayerTurn { get; private set; } = false;

    public void SetPlayerTurn(bool isPlayerTurn)
    {
        this.isPlayerTurn = isPlayerTurn;

        if (isPlayerTurn)
        {
            GetComponent<PlayerMovement>().EnableMovement();

            // Enable attack if attacker
            GetComponent<PlayerAttack>()?.EnableAttack();
            // Enable shoot if attacker
            GetComponent<PlayerShoot>()?.EnableShoot();

            // Enable next button
            if (GameManager.Instance.IsAttackerTurn())
                HUDManager.Instance.EnableButtonAttackerNext();
            else if (GameManager.Instance.IsDefenderTurn())
                HUDManager.Instance.EnableButtonDefenderNext();
}
        else
        {
            // Disable movement
            GetComponent<PlayerMovement>().DisableMovement();

            // Enable attack if attacker
            GetComponent<PlayerAttack>()?.DisableAttack();
        }
    }
}
