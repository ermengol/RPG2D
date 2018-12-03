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

    //Input keys by player input
    private string _leftJoystickKey;
    private string _jumpKey;

    protected float _horizontalAxis;

    protected void DoAwake()
    {
        _leftJoystickKey = kLeftJoystickKey + PlayerIndex.ToString();
        _jumpKey = kJumpKey + PlayerIndex.ToString();
    }

    protected void DoUpdate()
    {
        _horizontalAxis = Input.GetAxis(_leftJoystickKey);

        if (Input.GetButtonDown(_jumpKey))
        {
            DoJump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            DoAttack();
        }
    }

    protected virtual void DoJump()
    {
    }

    protected virtual void DoAttack()
    {
    }
}