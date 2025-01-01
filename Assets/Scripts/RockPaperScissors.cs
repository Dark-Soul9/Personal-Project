using UnityEngine;
public class RockPaperScissors : Minigame
{
    public override void AiInput()
    {
        aiInput = Random.Range(0, 3);
    }
    public override void PlayerInput(int input)
    {
        playerInput = input;
        DetermineResult(aiInput, playerInput);
    }
    public override MinigameState DetermineResult(int inputAI, int inputPlayer) //rock = 0, paper = 1, scissors = 2
    {
        if (inputPlayer == inputAI)
        {
            return MinigameState.Draw;
        }
        else if (inputPlayer == 0 && inputAI == 2 ||
            inputPlayer == 1 && inputAI == 0 ||
            inputPlayer == 2 && inputAI == 1)
        {
            return MinigameState.Win;
        }
        else
        {
            return MinigameState.Loss;
        }
    }
}