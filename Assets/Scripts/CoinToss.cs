using UnityEngine;

public class CoinToss : BaseMinigame
{
    public enum CoinChoice
    {
        Heads,
        Tails
    }

    public AnimationClip coinHeadsAnimation;
    public AnimationClip coinTailsAnimation;

    private CoinChoice simulatedTossResult; // Stores the result of the coin toss

    public override void StartMinigame()
    {
        SimulateCoinToss();
    }
    public override System.Enum GetChoiceFromIndex(int index)
    {
        return (CoinChoice)index; // Converts index to CoinChoice enum
    }
    public override void ResolveMinigame(System.Enum playerChoice)
    {
        CoinChoice playerCoinChoice = (CoinChoice)playerChoice;
        GameResult result = playerCoinChoice == simulatedTossResult ? GameResult.Win : GameResult.Lose;
        EndGame(result);
    }
    public override System.Enum AIChoice => simulatedTossResult; // The result of the coin toss, not an AI choice

    private void SimulateCoinToss()
    {
        // Simulate the coin toss (randomly select heads or tails)
        simulatedTossResult = (CoinChoice)Random.Range(0, 2);
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

