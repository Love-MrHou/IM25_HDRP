using System.Collections;
using UnityEngine;
public partial class NavMesh_AI
{
    private GameObject currentTalkingPartner; // 當前對話對象

    /*RequestStartTalking 方法用於請求開始與另一個 NPC 對話。
    如果當前狀態不是 Talking,它會設置 currentTalkingPartner 並啟動 StartTalkingCoroutine 協程。*/
    public void RequestStartTalking(GameObject partner)
    {
        if (currentCharacterState != CharacterState.Talking)
        {
            currentTalkingPartner = partner;
            StartCoroutine(StartTalkingCoroutine(partner));
        }
    }
    /*StartTalkingCoroutine 協程首先會讓 NPC 面向對話對象,然後等待一小段時間後,
    切換到 Talking 狀態和對話動畫。最後,它會開始計時,並在一段隨機時間後啟動,EndTalkingAfterDelay 協程來結束對話。*/
    private IEnumerator StartTalkingCoroutine(GameObject partner)
    {
        // 先面向對方
        transform.LookAt(new Vector3(partner.transform.position.x, transform.position.y, partner.transform.position.z));

        // 等待一小段時間讓角色完成轉向
        yield return new WaitForSeconds(0.2f);

        // 然後設置 Talking 狀態和動畫
        SetState(CharacterState.Talking);
        Debug.Log("動畫 : Talking");

        // 開始對話計時
        float talkDuration = Random.Range(talkingMinTime, talkingMaxTime);
        StartCoroutine(EndTalkingAfterDelay(talkDuration));
    }
    /*EndTalkingAfterDelay 協程在指定的延遲時間後,切換回 Idle 狀態,
    清除 currentTalkingPartner,設置冷卻計時器,並記錄相關信息。*/
    private IEnumerator EndTalkingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(CharacterState.Idle);
        currentTalkingPartner = null;
        SetCooldown(); // 再次設置以確保對話後也有冷卻
        Debug.Log("設定對話Delay");
    }
}
