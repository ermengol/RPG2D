using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputController : MonoBehaviour
{
    //Input keys
    private const string kLeftJoystickKey = "Horizontal_P";
    private const string kJumpKey = "Jump_P";
    
    public int PlayerIndex = 1;
    
    [SerializeField] private float _moveSpeed = 100f;
    [SerializeField] private float _jumpForce = 500f;

    //Input keys by player input
    private string _leftJoystickKey;
    private string _jumpKey;
    
    
    //Other
    private Rigidbody2D _rigidbody2D;
    private CharacterAnimationController _characterAnimationController;
    

    void Awake()
    {
        _leftJoystickKey = kLeftJoystickKey + PlayerIndex.ToString();
        _jumpKey = kJumpKey + PlayerIndex.ToString();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAnimationController = GetComponentInChildren<CharacterAnimationController>();
    }

    void Update()
    {
       
        var horizontalAxis = Input.GetAxis(_leftJoystickKey);

        _rigidbody2D.velocity = new Vector2(horizontalAxis * _moveSpeed, _rigidbody2D.velocity.y);
        _characterAnimationController.CharacterSpeed(Mathf.Abs(horizontalAxis));
        
        if (Input.GetButtonDown(_jumpKey))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            _characterAnimationController.CharacterJump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _characterAnimationController.CharacterDie();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _characterAnimationController.CharacterAttack();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _characterAnimationController.CharacterFalls(false);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _characterAnimationController.CharacterFalls(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _characterAnimationController.CharacterHurt();
        }
        
    }
}