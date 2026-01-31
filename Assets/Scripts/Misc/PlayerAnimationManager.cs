using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [field:SerializeField] public Player_SM stateMachine {get; private set;}


    public void UpdateAnimationMovementParameters(float horizontalMovement, float verticalMovement)
    {
        stateMachine.Animator.SetFloat("movementSpeed", verticalMovement, 0.1f, Time.deltaTime);
        stateMachine.Animator.SetFloat("strafeSpeed", horizontalMovement, 0.1f, Time.deltaTime);
    }
}
