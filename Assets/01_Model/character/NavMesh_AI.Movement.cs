using UnityEngine;
using UnityEngine.AI;

public partial class NavMesh_AI
{
    private NavMeshAgent agent; // NavMesh代理，用於控制NPC的移動
    /*SetNewDestination 方法用於為 NPC 設置一個新的隨機目標位置。
     它調用了 GetBestRandomPoint 方法來獲取一個在導航網格上的隨機點,然後將其設置為 NavMeshAgent 的目標位置。*/
    void SetNewDestination()
    {
        // 隨機目標選擇: 設置新的目標點
        Vector3 newDestination = GetBestRandomPoint(transform.position, roamRadius);
        agent.SetDestination(newDestination);
    }

    /*GetBestRandomPoint 方法用於獲取一個距離原點最遠的導航網格上的隨機點。它會嘗試多次在指定半徑內獲取一個隨機點,
    並選擇距離原點最遠的那個作為最佳目標點。這樣可以確保 NPC 不會過於靠近邊緣。*/
    Vector3 GetBestRandomPoint(Vector3 origin, float distance, int attempts = 5)
    {
        // 隨機目標選擇: 多次嘗試獲取最佳目標點
        NavMeshHit bestHit = new();
        float bestDistance = 0;
        for (int i = 0; i < attempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance + origin;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distance, NavMesh.AllAreas))
            {
                float hitDistance = Vector3.Distance(hit.position, origin);
                // 選擇距離原點最遠的點，確保不靠近邊緣
                if (hitDistance > bestDistance)
                {
                    bestHit = hit;
                    bestDistance = hitDistance;
                }
            }
        }
        return bestHit.position;
    }
}
