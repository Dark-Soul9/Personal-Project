using UnityEngine;
public abstract class Minigame : MonoBehaviour
{
    public enum MinigameState { Win, Loss, Draw}
    public MinigameState currentState;
    public int aiInput;
    public int playerInput;
    public abstract void AiInput();
    public abstract void PlayerInput(int input);
    public abstract MinigameState DetermineResult(int inputAI, int inputPlayer);
}


