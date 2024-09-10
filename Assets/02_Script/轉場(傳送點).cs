using UnityEngine;
using UnityEngine.SceneManagement; // 引入場景管理

public class SceneChanger : MonoBehaviour
{
    // 設置要加載的場景名稱
    public string sceneName;
    public GameObject ChangePoint;

    void Start()
    {
        ChangePoint.SetActive(false);
    }


    // 當其他物體進入觸發區域時調用此方法
    private void OnTriggerEnter(Collider other)
    {
        //檢查是否激活且是否在trugger中
        if (ChangePoint.activeSelf)
        {
            // 檢查進入觸發區域的物體是否是玩家
            if (other.CompareTag("Player"))
            {
                // 加載指定的場景
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}