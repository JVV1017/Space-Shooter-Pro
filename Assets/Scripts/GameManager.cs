using UnityEngine;
using UnityEngine.SceneManagement;          // Scene Manager library

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;           // To check if the game is over for real

    private void Update()
    {
        // if the r key was pressed
        // restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);     // To load the Current Game Scene 
        }
    }

    // Function to turn gameover to true
    public void GameOver()
    {
        //Debug.Log("GameManager::GameOver() Called");      // Professional way to debug if a function is called or not
        _isGameOver = true;
    }
}
