using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameStateType CurrentStateType { get; private set; }

    public GameModel ActiveModel { get; private set; }

    [SerializeField] private GameStateType startingGameState;

    private Dictionary<GameStateType, GameStateBase> gameStates = new Dictionary<GameStateType, GameStateBase>();

    public void GoTo(GameStateType gameState)
    {
        gameStates[CurrentStateType].ExitState();
        CurrentStateType = gameState;
        gameStates[CurrentStateType].EnterState();
    }

    public void AddState(GameStateType type, GameStateBase state)
    {
        if (!gameStates.ContainsKey(type))
        {
            gameStates.Add(type, state);
        }
    }

    private void Start()
    {
        ActiveModel = new GameModel();
        CurrentStateType = startingGameState;
        gameStates[CurrentStateType].EnterState();
    }

}
