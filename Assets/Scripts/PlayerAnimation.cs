//using UnityEngine;

//public class PlayerAnimation : MonoBehaviour
//{
//    private Animator _anim;         // Variable to access animator component
//    private Player _player;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        // Gets the animator's components
//        _anim = GetComponent<Animator>();

//        _player = GetComponent<Player>();

//        // Null Checks animator
//        if (_anim == null)
//            Debug.LogError("The animator is NULL.");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // If player 1,
//        if (_player._isPlayerOne == true)
//        {
//            // Player Left Animation
//            if (Input.GetKeyDown(KeyCode.A))
//            {
//                _anim.SetBool("Turn_Left", true);
//                _anim.SetBool("Turn Right", false);
//            }
//            else if (Input.GetKeyDown(KeyCode.A)))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", false);
//            }

//            // Player Right Animation
//            if (Input.GetKeyDown(KeyCode.D))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", true);
//            }
//            else if (Input.GetKeyDown(KeyCode.A))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", false);
//            }
//        }
        
//        // Or else, you are player 2,
//        else
//        {
//            // Player Left Animation
//            if (Input.GetKeyDown(KeyCode.LeftArrow))
//            {
//                _anim.SetBool("Turn_Left", true);
//                _anim.SetBool("Turn Right", false);
//            }
//            else if (Input.GetKeyDown(KeyCode.LeftArrow))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", false);
//            }

//            // Player Right Animation
//            if (Input.GetKeyDown(KeyCode.RightArrow))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", true);
//            }
//            else if (Input.GetKeyDown(KeyCode.RightArrow))
//            {
//                _anim.SetBool("Turn_Left", false);
//                _anim.SetBool("Turn Right", false);
//            }
//        }
//    }
//}
