using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private Animator playerAnimator = null; 
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Public Core Functions
    public void SwitchAnimation(PlayerAnimationState state)
    {
        switch (state)
        {
            case PlayerAnimationState.Idle:
                playerAnimator.SetBool("b_Run", false);
                playerAnimator.SetBool("b_Push", false);
                playerAnimator.SetBool("b_SlowWalk", false);
                break;
            case PlayerAnimationState.Run:
                playerAnimator.SetBool("b_Run", true);
                playerAnimator.SetBool("b_Push", false);
                playerAnimator.SetBool("b_SlowWalk", false);
                break;
            case PlayerAnimationState.SlowWalk:
                playerAnimator.SetBool("b_SlowWalk", true);
                break;
            case PlayerAnimationState.Victory:
                playerAnimator.SetTrigger("t_Victory");
                playerAnimator.SetBool("b_SlowWalk", false);
                break;
            case PlayerAnimationState.Push:
                playerAnimator.SetBool("b_Run", false);
                playerAnimator.SetBool("b_Push", true);
                playerAnimator.SetBool("b_SlowWalk", false);
                break;
            case PlayerAnimationState.Defeat:
                playerAnimator.SetTrigger("t_Defeat");
                playerAnimator.SetBool("b_SlowWalk", false);
                break;
        }
    }
    #endregion
}
