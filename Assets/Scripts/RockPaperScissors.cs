using UnityEngine;
public class RockPaperScissors : BaseMinigame
{
    public enum RPSChoice
    {
        Rock,
        Paper,
        Scissors
    }

    private RPSChoice aiChoice;

    public override System.Enum AIChoice => aiChoice;

    public override void StartMinigame()
    {
        // Simulate AI choice
        aiChoice = (RPSChoice)Random.Range((int)RPSChoice.Rock, (int)RPSChoice.Scissors + 1);
    }
    public override System.Enum GetChoiceFromIndex(int index)
    {
        return (RPSChoice)index; // Converts index to RPSChoice enum
    }

    public override void ResolveMinigame(System.Enum playerChoice)
    {
        RPSChoice playerRPSChoice = (RPSChoice)playerChoice;
        GameResult result = DetermineResult(playerRPSChoice, aiChoice);
        EndGame(result);
    }

    private GameResult DetermineResult(RPSChoice playerChoice, RPSChoice aiChoice)
    {
        if (playerChoice == aiChoice) return GameResult.Draw;

        if ((playerChoice == RPSChoice.Rock && aiChoice == RPSChoice.Scissors) ||
            (playerChoice == RPSChoice.Paper && aiChoice == RPSChoice.Rock) ||
            (playerChoice == RPSChoice.Scissors && aiChoice == RPSChoice.Paper))
        {
            return GameResult.Win;
        }
        return GameResult.Lose;
    }
    public override string GetResultDisplayText(GameResult result)
    {
        return $"Result: {result}"; // Result can be Win, Lose, or Draw
    }

    public override string GetAIChoiceDisplayText()
    {
        return $"AI Chose: {AIChoice}"; // Rock, Paper, or Scissors
    }
}
