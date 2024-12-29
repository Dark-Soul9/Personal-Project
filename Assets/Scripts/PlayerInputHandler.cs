using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public void SubmitPlayerChoice(int choiceIndex)
    {
        GameManager.Instance.PlayerMadeChoice(choiceIndex);
    }
}
