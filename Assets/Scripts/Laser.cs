using UnityEngine;

public class Laser : MonoBehaviour
{
    // Speed Variable of 8
    [SerializeField]
    private float _speed = 8.0f;        // Variable to store the speed for laser movement
    //private bool _isEnemyLaser = false;     // To know if this laser is enemy's or player's

    // Update is called once per frame
    void Update()
    {
        MoveUp();
        // If this laser is not enemies, use the player's move up laser otherwise enemy's move down laser
        //if (_isEnemyLaser == false)
        //    MoveUp();
        //else
        //    MoveDown();
    }

    // Player's laser = move up
    void MoveUp()
    {
        // Translate the laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Check if the laser position is greater than 8 on the y 
        if (transform.position.y > 8f)
        {
            // Check if this object has a parent
            if (transform.parent != null)           // Remember: transform = transform of this object
                Destroy(transform.parent.gameObject);       // Destroys this object's parent gameObject which also means destroying all the child gameObjects

            Destroy(this.gameObject);           // Destroys the laserobject by specifying this script's gameobject
            // Destroy(this.gameObject, 5f);   // Alternatively, you can use 5f with Destroy to destroy the object after 5 seconds
        }
    }

    //// Enemy's laser = move up
    //void MoveDown()
    //{
    //    // Translate the laser up
    //    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    //    // Check if the laser position is greater than 8 on the y 
    //    if (transform.position.y < -8f)
    //    {
    //        // Check if this object has a parent
    //        if (transform.parent != null)           // Remember: transform = transform of this object
    //            Destroy(transform.parent.gameObject);       // Destroys this object's parent gameObject which also means destroying all the child gameObjects

    //        Destroy(this.gameObject);           // Destroys the laserobject by specifying this script's gameobject
    //    }
    //}

    //// This function enables the bool variable to tell the enemy that the laser fired is its laser
    //public void AssignEnemyLaser()
    //{
    //    _isEnemyLaser = true;
    //}

    //// Collider trigger when the enemy laser collides with the player (damages the player)
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player" && _isEnemyLaser == true)
    //    {
    //        Player player = other.GetComponent<Player>();

    //        if (player != null)
    //            player.Damage();
    //    }
    //}
}
