using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Base,
        Level
    }

    private GameState currentState;

    public static event Action<GameState> OnSceneChanged;

    private void Start()
    {
        switch (GetCurrentSceneName())
        {
            case "Base":
                currentState = GameState.Base;
                break;
            case "Level":
                currentState = GameState.Level;
                break;
            default:
                break;
        }
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ChangeState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        switch (newState)
        {
            case GameState.Base:
                LoadBaseScene();
                break;
            case GameState.Level:
                LoadLevelScene();
                break;
        }

        OnSceneChanged?.Invoke(newState);
    }

    private void LoadBaseScene()
    {
        SceneManager.LoadScene("Base");
    }

    private void LoadLevelScene()
    {
        SceneManager.LoadScene("Level");
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }

    public void ChangeToBaseState()
    {
        ChangeState(GameState.Base);
    }
}
