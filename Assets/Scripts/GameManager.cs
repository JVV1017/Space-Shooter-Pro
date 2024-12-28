using UnityEngine;
using UnityEngine.SceneManagement;          // Scene Manager library

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;           // To check if the game is over for real
    public bool _isCoOpMode = false;    // Differentiates the modes
    [SerializeField]
    private GameObject _pauseMenuPanel; // To use the pause menu panel from the inspector
    private Animator _pauseAnimator;        // Pause Menu Animator object to animate the pause menu

    private void Start()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();      // Access the pause menu animator

        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;    // Uses an update mode from the animator object called UnscaledTime which can animate UI even while Time.timeScale is 0 (frozen)

        // Null checking
        if (_pauseAnimator == null)
            Debug.LogError("The Pause Menu Animator is NULL.");
    }

    private void Update()
    {
        // if its single player mode, pressing r then reopens the single player scene
        if (_isCoOpMode == false)
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
                SceneManager.LoadScene(1);
        
        // if its co-op mode, pressing r then reopens the co-op mode scene
        if (_isCoOpMode == true)
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
                SceneManager.LoadScene(2); 

        // if the escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();                 // quit the application

        // if p key is pressed, pause the game and enable pause menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);            // enables the pause menu panel
            _pauseAnimator.SetBool("isPaused", true);   // Animates the pause menu animation when pressed the p key
            Time.timeScale = 0;             // timeScale = 0 (freezes frame even including animations), 0.5 (2x slower than realtime, useful for slow motion effects) and 1.0 (fast as realtime)
        }  
    }

    // Function to turn gameover to true
    public void GameOver()
    {
        //Debug.Log("GameManager::GameOver() Called");      // Professional way to debug if a function is called or not
        _isGameOver = true;
    }

    // A public resume game function to disable the pause menu panel and unfreeze the game to be used by the UIManager
    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
