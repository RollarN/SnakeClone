using System;

/// <summary>Static class containing events for game end and start.
/// </summary>
public static class GameLoopUtility
{
    /// <summary>Event for Ending the game
    /// </summary>
    public static Action OnGameEnd;
    /// <summary>Event for Starting the game
    /// </summary>
    public static Action OnGameStart;
}