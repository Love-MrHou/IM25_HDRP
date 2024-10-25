using UnityEngine;

public class ObjectPingPong : MonoBehaviour
{
    public float minY = 5.3f;   // 最小的 y 位置
    public float maxY = 5.5f;   // 最大的 y 位置
    public float speed = 1.0f;  // 移动速度

    private bool movingUp = true; // 控制方向

    void Update()
    {
        // 获取当前物体位置
        Vector3 currentPosition = transform.position;

        // 如果物体正在向上移动
        if (movingUp)
        {
            // 增加 y 位置，按照速度移动
            currentPosition.y += speed * Time.deltaTime;

            // 如果超过最大 y 值，改变方向
            if (currentPosition.y >= maxY)
            {
                currentPosition.y = maxY;
                movingUp = false;
            }
        }
        else
        {
            // 减少 y 位置，按照速度移动
            currentPosition.y -= speed * Time.deltaTime;

            // 如果低于最小 y 值，改变方向
            if (currentPosition.y <= minY)
            {
                currentPosition.y = minY;
                movingUp = true;
            }
        }

        // 更新物体位置
        transform.position = currentPosition;
    }
}
