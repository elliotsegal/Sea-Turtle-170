using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    LoadingNextLevel,
    RestartingLevel
}

public class GameManager : MonoBehaviour
{
    public static GameManager singleton { get; private set; }

    public PlayerController player;

    private GameState state;

    private void Awake()
    {
        singleton = this;
        state = GameState.Playing;
    }

    public void EndLevel(bool win)
    {
        if (state != GameState.Playing) return;

        string currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log(currentLevel + " ended " + (win ? "(win)" : "(loss)"));
        if (win)
        {
            state = GameState.LoadingNextLevel;
            //LoadLevel("NextLevel");
        }
        else
        {
            state = GameState.RestartingLevel;
            LoadLevel(currentLevel);
        }
    }

    private IEnumerable LoadLevel(string name)
    {
        Debug.Log("Loading level " + name);
        yield return new WaitForSeconds(1);
        yield return SceneManager.LoadSceneAsync(name);
        state = GameState.Playing;
    }
}
