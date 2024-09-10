using UnityEngine;

public partial class NavMesh_AI
{
    // 動畫狀態管理: 更新當前動畫狀態
    private void ChangeAnimationState(string newState)
    {
        //檢查新的動畫狀態是否與當前狀態相同,如果相同則直接返回,避免不必要的動畫切換。
        if (currentState == newState) return;
        //新的動畫狀態與當前狀態不同,則使用 animator.CrossFade 方法在 0.3 秒內平滑地過渡到新的動畫狀態
        animator.CrossFade(newState, 0.3f, 0);
        //更新 currentState 變數以反映新的動畫狀態
        currentState = newState;
    }
}
