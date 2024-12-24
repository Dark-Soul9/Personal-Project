using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public void SubmitPlayerChoice(int choiceIndex)
    {
        // Pass the player's choice to the GameManager
        GameManager.Instance.PlayerMadeChoice(choiceIndex);
    }
}
