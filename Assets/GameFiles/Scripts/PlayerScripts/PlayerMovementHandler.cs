using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;

    [Header("Components Reference")]
    [SerializeField] private CharacterController characterController = null;

    private VariableJoystick movementJS = null;
    private Vector3 movementDirection = Vector3.zero;
    private float screenCenterX = 0f;
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
        //TouchInputs();
        movementDirection = new Vector3(movementJS.Horizontal, 0, 1).normalized;
        characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
    }
    #endregion

    #region Private Core Functions
    private void TouchInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)
            {
                if (firstTouch.position.x > screenCenterX)
                {
                    movementDirection = new Vector3(1, 0, 1).normalized;
                }
                else if (firstTouch.position.x < screenCenterX)
                {
                    movementDirection = new Vector3(-1, 0, 1).normalized;
                }
            }
        }
    }
    #endregion
}
