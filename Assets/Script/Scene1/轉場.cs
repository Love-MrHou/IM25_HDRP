using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;  // 要加载的场景名称

    public GameObject button1;  // "前往教育训练"按钮
    public GameObject button2;  // "前往会议模拟"按钮

    public string playerName = "Player";  // 场景中玩家物体的名称

    // 位置和旋转的定义
    private Vector3 trainingPosition = new Vector3(35.79f, 0.8013755f, -10.62f);
    private Vector3 meetingPosition = new Vector3(38.737f, 0.8013755f, 7.582456f);

    private Vector3 trainingRotation = new Vector3(0.207f, 548.55f, 0.003f);
    private Vector3 meetingRotation = new Vector3(0.207f, 337.486f, 0.003f);

    void Start()
    {
        // 使此物件在场景切换后不被销毁
        DontDestroyOnLoad(gameObject);

        // 监听按钮点击事件
        button1.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangeSceneAndMovePlayer("training"));
        button2.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => ChangeSceneAndMovePlayer("meeting"));
    }

    // 切换场景并移动玩家
    void ChangeSceneAndMovePlayer(string destination)
    {
        StartCoroutine(LoadSceneAndMovePlayer(destination));
    }

    // 异步加载场景并移动玩家
    IEnumerator LoadSceneAndMovePlayer(string destination)
    {
        // 异步加载目标场景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 场景加载完成后查找玩家
        GameObject player = GameObject.Find(playerName);

        if (player != null)
        {
            // 根据按钮点击来移动和旋转玩家
            if (destination == "training")
            {
                player.transform.position = trainingPosition;
                player.transform.eulerAngles = trainingRotation;
                Debug.Log("Player moved to training position.");
            }
            else if (destination == "meeting")
            {
                player.transform.position = meetingPosition;
                player.transform.eulerAngles = meetingRotation;
                Debug.Log("Player moved to meeting position.");
            }

            // 确保激活玩家的摄像头
            ActivatePlayerCamera(player);
        }
        else
        {
            Debug.LogError("未找到玩家对象！");
        }
    }

    // 激活玩家的摄像头
    void ActivatePlayerCamera(GameObject player)
    {
        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            foreach (Camera cam in Camera.allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            playerCamera.gameObject.SetActive(true);
            Debug.Log(player.name + "的摄像头已启用。");
        }
        else
        {
            Debug.LogError("未找到 " + player.name + " 的摄像头！");
        }
    }
}
