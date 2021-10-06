using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float shiftSpeed = 0f;
    [SerializeField] private float slowWalkSpeed = 0f;


    [Header("Components Reference")]
    [SerializeField] private CharacterController characterController = null;

    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    private float screenCenterX = 0f;
    private float oldX;
    private float speedTemp = 0f;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {

        //Testing
        PlayerSingleton.Instance.GetPlayerAnimationsHandler.SwitchAnimation(PlayerAnimationState.Run);
        speedTemp = moveSpeed;
        ForceStop = false;
    }

    private void Update()
    {
        if (!ForceStop)
        {
            TouchInputs();
            // movementDirection = new Vector3(movementJS.Horizontal, 0, 1).normalized;
            //characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
            transform.Translate(new Vector3(shiftSpeed, 0, speedTemp) * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 4.5f), transform.position.y, transform.position.z);
        }
       
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
    }
    #endregion

    #region Public Core Functions
    public void EnableDefaultSpeed(bool value)
    {
        if (value)
        {
            speedTemp = moveSpeed;
        }
        else
        {
            speedTemp = slowWalkSpeed;
        }
    }
    #endregion

    #region Getter And Setter
    public bool ForceStop { get; set; }
    #endregion
}
