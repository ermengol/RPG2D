using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputController : MonoBehaviour
{
    enum AirState
    {
        NONE,
        IN_GROUND,
        IN_AIR,
        JUMPING
    }

    //Input keys
    private const string kLeftJoystickKey = "Horizontal_P";
    private const string kJumpKey = "Jump_P";

    public int PlayerIndex = 1;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpForce = 15f;

    private const float _minimumMovement = 0.5f;

    //Input keys by player input
    private string _leftJoystickKey;
    private string _jumpKey;

    //Falling points
    [SerializeField] private float _minDistanceFromGround = 0.01f;
    private Transform _leftBottomRayObject;
    private Transform _rightBottomRayObject;

    private bool _inMidAir = false;
    private bool _facingRight = true;

    private AirState _state = AirState.NONE;

    //Other
    private Rigidbody2D _rigidbody2D;
    private CharacterAnimationController _characterAnimationController;


    void Awake()
    {
        _leftJoystickKey = kLeftJoystickKey + PlayerIndex.ToString();
        _jumpKey = kJumpKey + PlayerIndex.ToString();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAnimationController = GetComponentInChildren<CharacterAnimationController>();

        _leftBottomRayObject = new GameObject("left_ray").transform;
        _leftBottomRayObject.parent = transform;
        _leftBottomRayObject.localPosition = new Vector2(-0.5f, 0f);

        _rightBottomRayObject = new GameObject("right_ray").transform;
        _rightBottomRayObject.parent = transform;
        _rightBottomRayObject.localPosition = new Vector2(0.5f, 0f);
    }

    void Update()
    {
        var horizontalAxis = Input.GetAxis(_leftJoystickKey);

        if (horizontalAxis <= _minimumMovement && horizontalAxis >= -_minimumMovement)
        {
            horizontalAxis = 0f;
        }

        _rigidbody2D.velocity = new Vector2(horizontalAxis * _moveSpeed, _rigidbody2D.velocity.y);
        _characterAnimationController.CharacterSpeed(Mathf.Abs(horizontalAxis));

        CheckAirLogic();
        CheckFlip(horizontalAxis);

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

    void CheckFlip(float movement)
    {
        if (movement == 0 || (movement < 0 && !_facingRight) || (movement > 0 && _facingRight))
        {
            return;
        }

        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    #region jump logic

    private bool falling = false;

    void CheckAirLogic()
    {
        _inMidAir = !IsGrounded();

        bool speedInGround = _rigidbody2D.velocity.y >= 0 && _rigidbody2D.velocity.y < 0.05f;

        if (Input.GetButtonDown(_jumpKey) && _state == AirState.IN_GROUND)
        {
            _inMidAir = true;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            _characterAnimationController.CharacterJump();
            _state = AirState.JUMPING;
        }
        else if (_state != AirState.IN_GROUND && !_inMidAir && speedInGround)
        {
            _state = AirState.IN_GROUND;

            _characterAnimationController.CharacterFalls(false);
            _characterAnimationController.CharacterGrounded();
        }
        else if (_inMidAir)
        {
            _state = AirState.IN_AIR;

            if (_rigidbody2D.velocity.y < 0 && !falling)
            {
                falling = true;
                _characterAnimationController.CharacterFalls(true);
            }
            else if (_rigidbody2D.velocity.y > 0 && falling)
            {
                falling = false;
                _characterAnimationController.CharacterFalls(false);
            }
        }
    }

    bool IsGrounded()
    {
        var leftGroundedRaycast =
            Physics2D.Raycast(_leftBottomRayObject.transform.position, -Vector3.up, _minDistanceFromGround);
        var rightGroundedRaycast =
            Physics2D.Raycast(_rightBottomRayObject.transform.position, -Vector3.up, _minDistanceFromGround);

        var leftRigidBody = leftGroundedRaycast.rigidbody;
        var rightRigidBody = rightGroundedRaycast.rigidbody;

        bool leftGrounded = leftGroundedRaycast.collider != null;
        leftGrounded &= leftGroundedRaycast.distance <= _minDistanceFromGround;
        leftGrounded &= leftRigidBody == null || leftRigidBody != _rigidbody2D;
        bool rightGrounded = rightGroundedRaycast.collider != null;
        rightGrounded &= rightRigidBody == null || rightRigidBody != _rigidbody2D;
        rightGrounded &= rightGroundedRaycast.distance <= _minDistanceFromGround;

        return leftGrounded || rightGrounded;
    }

    #endregion
}