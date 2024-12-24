using UnityEngine;

public class CoinToss : BaseMinigame
{
    public enum CoinChoice
    {
        Heads,
        Tails
    }

    private CoinChoice coinOutcome;

    public override System.Enum AIChoice => coinOutcome;

    public override void StartMinigame()
    {
        // Simulate Coin Toss
        coinOutcome = Random.Range(0, 2) == 0 ? CoinChoice.Heads : CoinChoice.Tails;
    }
    public override System.Enum GetChoiceFromIndex(int index)
    {
        return (CoinChoice)index; // Converts index to CoinChoice enum
    }
    public override void ResolveMinigame(System.Enum playerChoice)
    {
        CoinChoice playerCoinChoice = (CoinChoice)playerChoice;
        GameResult result = playerCoinChoice == coinOutcome ? GameResult.Win : GameResult.Lose;
        EndGame(result);
    }
    public override string GetResultDisplayText(GameResult result)
    {
        return $"Coin Result: {AIChoice}"; // Heads or Tails
    }

    public override string GetAIChoiceDisplayText()
    {
        return "";
    }

}

