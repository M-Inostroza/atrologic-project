using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level");
    }
}