using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] HunterSpawner hunterSpawner;
    [SerializeField] GameObject whale;

    public float difficultyMultiplierBase = 1.0f;
    public float timer = 0.0f;

    [Header("General Gameplay Settings")]
    [SerializeField] public int maxHunterNumber = 3;
    [SerializeField] public float hunterSpawnFrequency = 15.0f;
    [SerializeField] public float firstHunterSpawn = 3.0f;
    [SerializeField] public float throwingFrequency = 7.0f;
    [SerializeField] public float harpoonSpeed = 50.0f;
    [SerializeField] public float harpoonRange = 30.0f;

    [Header("Difficulty Settings")]
    [SerializeField] public float difficultyMultiplierModifier = 0.01f;
    [SerializeField] public float difficultyThreshold = 7.0f;

    private void Awake()
    {
        instance = this;
        //GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.GameLoop);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(difficultyMultiplierBase < difficultyThreshold)
        {
            difficultyMultiplierBase += ((60/timer) * difficultyMultiplierModifier);
        }

    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.GameLoop:
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLose()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

public enum GameState
{
    GameLoop,
    Lose
}