using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public delegate void PlayerFinsihedGame();
    public delegate void PlayerStartedGame();
    public delegate void GameStartSetUp();
    public delegate void GameEndSetUp(bool didPlayerWin);

    public static PlayerFinsihedGame playerFinsihedGame;
    public static PlayerStartedGame playerStartGame;
    public static GameStartSetUp gameStartSetUp;
    public static GameEndSetUp gameEndSetUp;
}
