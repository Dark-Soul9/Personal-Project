using UnityEngine;

public class CoinToss : Minigame
{
    public override void AiInput()
    {
        aiInput = Random.Range(0, 2);
    }
    public override void PlayerInput(int input)
    {
        playerInput = input;
        DetermineResult(aiInput, playerInput);
    }
    public override MinigameState DetermineResult(int inputAI, int inputPlayer)
    {
        throw new System.NotImplementedException();
    }
}

