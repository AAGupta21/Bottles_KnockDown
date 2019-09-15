using System.Collections;
using System.Collections.Generic;

public enum GameStage { MainMenu, Shooting, ShotTaken, GameOver, SetGame, GotOne, SetBall, ResetBall, LoadGame, LoadMenu, RemoveBottles, CleanBottles, ResetBallLoc };

/*

 * Main Menu : Main Menu Canvas shows.
 * Load Menu : Loads the Menu Options, Deactivating others.
 * Shooting : Player is setting direction for the ball.
 * ShotTaken : Player has shot the ball.
 * Gameover : Player has used his last life.
 * SetGame : The bottle is randomly picked and randomly set on the play field (picked from few predetermined positions and prefabs).
 * GotOne : Update the Score.
 * SetBall : Ball is set at spawn position. (after hitting bottle or at start)
 * ResetBall : Balss is set at spawn position and player looses a life. (when player misses)
 * LoadGame : Starts the game.
 * RemoveBottles : Removes all bottles from previous game run(In Restart).
 * CleanBottles : Removes all bottles when main menu is loaded.
 * ResetBallLoc : Reloads ball to spawn point.

*/