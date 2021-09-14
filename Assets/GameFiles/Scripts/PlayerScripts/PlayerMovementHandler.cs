using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float shiftSpeed = 0f;


    [Header("Components Reference")]
    [SerializeField] private CharacterController characterController = null;

    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    private float screenCenterX = 0f;
    private float oldX;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        movementJS = LevelUIManager.Instance.GetMovementJS;

        //Testing
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);
    }

    private void Update()
    {
        TouchInputs();
       // movementDirection = new Vector3(movementJS.Horizontal, 0, 1).normalized;
        //characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
        transform.Translate(new Vector3(shiftSpeed, 0, moveSpeed) * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-4.5f,4.5f),transform.position.y, transform.position.z);

    }
    #endregion

    #region Private Core Functions
    private void TouchInputs()
    {
        
            float x = 0;
            if (Input.GetMouseButtonDown(0))
            {
                oldX = Input.mousePosition.x;
            }

            if (Input.GetMouseButton(0))
            {
                x = (Input.mousePosition.x - oldX) / 4;
                oldX = Input.mousePosition.x;
            }
        shiftSpeed = x;
        if (x > 0)
        {
            //movementDirection = new Vector3(shiftSpeed, 0, 1 * moveSpeed) * Time.deltaTime;
        }

        else if (x < 0)
        {
          //  movementDirection = new Vector3(-shiftSpeed, 0, 1 * moveSpeed) * Time.deltaTime;
        }
        else
        {
           // movementDirection = new Vector3(0, 0, 1 * moveSpeed) * Time.deltaTime;
        }

    }
    #endregion

    #region

    #endregion
}
