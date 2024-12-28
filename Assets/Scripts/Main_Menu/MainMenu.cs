using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // To load the single player game from the main menu
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1);      
    }

    // To load the co-op mode from the main menu
    public void LoadCoOpMode()
    {
        SceneManager.LoadScene(2);
    }
}
