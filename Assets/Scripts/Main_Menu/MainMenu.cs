using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // To load the game from the main menu
    public void LoadGame()
    {
        // Load the game scene
        SceneManager.LoadScene(1);      // Load Game Scene
    }
}
