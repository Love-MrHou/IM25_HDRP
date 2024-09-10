using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public partial class NavMesh_AI
{
    IEnumerator StateManager()
    {
        while (true)
        {
            switch (currentCharacterState)
            {
                case CharacterState.Idle:
                    // 狀態驅動的行為: Idle狀態處理
                    yield return new WaitForSeconds(Random.Range(idleMinTime, idleMaxTime));
                    if (currentCharacterState == CharacterState.Idle) // 確保狀態沒有被其他事件改變
                        SetState(CharacterState.Walking);
                    break;
                case CharacterState.Walking:
                    // 狀態驅動的行為: Walking狀態處理
                    SetNewDestination();
                    yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending);
                    Debug.Log("到達指定地點 切換IDLE");
                    //animator.CrossFade(ANIM_IDLE, 0.3f, 0);  // 0.25秒內平滑過渡到Idle動畫
                    SetState(CharacterState.Idle);
                    break;
                case CharacterState.Talking:
                    if (currentTalkingPartner != null)
                    {
                        // 先面向對方
                        transform.LookAt(new Vector3(currentTalkingPartner.transform.position.x, transform.position.y, currentTalkingPartner.transform.position.z));
                        yield return new WaitForSeconds(0.2f);  // 等待轉向完成

                        // 設置動畫
                        ChangeAnimationState(ANIM_TALK);

                        // 等待對話時間
                        yield return new WaitForSeconds(Random.Range(talkingMinTime, talkingMaxTime));

                        SetState(CharacterState.Idle);
                        cooldownTimer = collisionCooldown;
                    }
                    else
                    {
                        SetState(CharacterState.Idle);
                    }
                    break;
            }
        }
    }
    void SetState(CharacterState newState)
    {
        currentCharacterState = newState;
        switch (newState)
        {
            case CharacterState.Idle:
                // 動畫狀態管理: 切換到Idle動畫
                ChangeAnimationState(ANIM_IDLE);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                agent.ResetPath();
                break;
            case CharacterState.Walking:
                // 動畫狀態管理: 切換到Walking動畫
                ChangeAnimationState(ANIM_WALK);
                agent.isStopped = false;
                break;
            case CharacterState.Talking: // 修改SetState方法，允許外部直接設置狀態
                ChangeAnimationState(ANIM_TALK);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                agent.ResetPath();
                break;
        }
    }
}
