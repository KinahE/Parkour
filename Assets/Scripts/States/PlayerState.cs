using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected Player_SM stateMachine;
    public PlayerBaseState(Player_SM stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected IEnumerator DoParkourAction(ParkourAction action)
    {
        stateMachine.SetInAction(true);
        stateMachine.SetControl(false);

        stateMachine.Animator.CrossFade(action.AnimationName, 0.2f);

        while (!stateMachine.Animator.GetCurrentAnimatorStateInfo(0).IsName(action.AnimationName))
        {
            yield return null;
        }

        var animState = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (action.EnableTargetMatching)
        {

            MatchTarget(action);
        }
        float timer = 0f;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            if (action.RotateToObstacle)
            {
                stateMachine.transform.rotation = Quaternion.RotateTowards(stateMachine.transform.rotation, action.TargetRotation, stateMachine.RotationSpeed * Time.deltaTime);
            }


            yield return null;
        }
        Debug.Log("reached");
        stateMachine.SetInAction(false);
        stateMachine.SetControl(true);
    }


    void MatchTarget(ParkourAction action)
    {
        if (stateMachine.Animator.isMatchingTarget) return;
        stateMachine.Animator.MatchTarget(
            action.MatchPosition,
            stateMachine.transform.rotation,
            action.MatchBodyPart,
            new MatchTargetWeightMask(new Vector3(0, 1, 0), 0),
            action.MatchStartTime,
            action.MatchTargetTime
        );
    }
}
