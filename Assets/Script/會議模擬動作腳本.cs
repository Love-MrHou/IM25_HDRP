using UnityEngine;

public class RandomStartAnimation : MonoBehaviour
{
    public Animator animator; // 角色的 Animator 组件
    public string parameterName = "SitToTalk"; // Animator 中的参数名称
    public float minDelay = 0f; // 最小延迟时间（秒）
    public float maxDelay = 2f; // 最大延迟时间（秒）

    void Start()
    {
        StartCoroutine(StartAnimationWithRandomDelay());
    }

    private System.Collections.IEnumerator StartAnimationWithRandomDelay()
    {
        // 生成一个在 [minDelay, maxDelay] 范围内的随机延迟时间
        float delay = Random.Range(minDelay, maxDelay);
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);
        // 设置 Animator 参数，触发动画
        animator.SetBool(parameterName, true);
    }
}
