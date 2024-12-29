using UnityEngine;
using System.Collections.Generic;

public static class MinigameFactory
{
    private static List<BaseMinigame> minigames = new List<BaseMinigame>
    {
        //new RockPaperScissors(),
        new CoinToss(),
        //new NumberGuessMinigame(),
        //new MemoryMatchingMinigame()
    };

    public static BaseMinigame GetRandomMinigame()
    {
        int index = Random.Range(0, minigames.Count);
        return minigames[index];
    }
}
