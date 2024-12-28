using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;               // UI library

public class UIManager : MonoBehaviour
{
    // Handle to Text
    [SerializeField]
    private Text _scoreText;        // Score text object to store the score of the game
    [SerializeField]
    private Text _bestScoreText;    // Best score text object to store the best score of the game
    [SerializeField]
    private Image _LivesImg;        // To display the right lives image per the player's lives count
    [SerializeField]
    private Sprite[] _liveSprites;  // Array for storing Player's lives images from nolives to 3 lives.
    [SerializeField]
    private Text _gameOverText;     // To turn on the text gameover when the player dies.
    [SerializeField]
    private Text _restartText;      // To turn on the text restart to show the player that they can restart the level
    private GameManager _gameManager;       // To access the components of gameManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the game with score of 0
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);      // Disables the gameover text at the beginning of the game
        _restartText.gameObject.SetActive(false);       // Disables the restart text at the beginning of the game
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();     // Gets the component of GameManager

        // Null checks if the gameManager is null or not
        if (_gameManager == null)
            Debug.LogError("GameManager is NULL.");
    }

    // Updates the score ui text while playing the game
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();       // Score value to modify ui by getting the value that is communicated with the player
    }

    // Updates the best score ui while playing the game 
    public void UpdateBestScore(int bestScore)
    {
        _bestScoreText.text = "Best: " + bestScore.ToString();      // Best score value to modify ui by getting the value that is communicated with the player
    }

    // Updates the lives ui image while playing the game
    public void UpdateLives(int currentLives)
    {
        // Access the display image sprite
        // Then give it a new one based on the currentLives index
        _LivesImg.sprite = _liveSprites[currentLives];
        
        // If the lives are 0, then the GameOverSequence function activates
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    // To activate a sequence of events after game is over.
    void GameOverSequence()
    {
        _gameManager.GameOver();                        // Calling the GameOver function from GameManager to let GameManager know the game is over
        _gameOverText.gameObject.SetActive(true);       // Turns on the gameover text
        _restartText.gameObject.SetActive(true);        // Turns on the restart text
        StartCoroutine(GameOverTextFlicker());          // Starts the coroutine to flicker the gameover text
    }

    // To loop gameover and nothing text after .5 seconds to get the flicker effect like the in retro games
    IEnumerator GameOverTextFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Resume Play
    public void ResumePlay()
    {
        _gameManager.ResumeGame();
    }

    // Back to main menu where we load the main menu scene
    public void Back_To_Main_Menu()
    {
        SceneManager.LoadScene(0);
    }
}
