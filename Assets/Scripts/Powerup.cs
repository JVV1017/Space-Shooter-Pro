using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;        // Variable to store the speed for powerups movement

    [SerializeField]            // 0 = Triple Shot, 1 = Speed, 2 = Shields
    private int powerupID;      // Variable to distinguish each powerup through an id system

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move down at a speed of 3 (adjust in the inspector)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);        // Remember: Time.deltaTime = RealTime

        // When we leave the screen, destroy this object
        if (transform.position.y < -6f)
            Destroy(this.gameObject);
    }

    // Collider trigger when the enemy collides with the player (enables player's powerup)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only be collectable by the Player
        if (other.tag == "Player")
        {
            // Handle to the component I want (the player)
            Player player = other.transform.GetComponent<Player>();

            // Assign the handle to the component while null checking at the same time
            if (player != null)
            {
                // A switch case to define each power up status
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            // on collected, destroy the powerup object
            Destroy(this.gameObject);
        }
    }
}
