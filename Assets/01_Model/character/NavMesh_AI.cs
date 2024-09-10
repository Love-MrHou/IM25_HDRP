using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class NavMesh_AI : MonoBehaviour
{
   
    private Animator animator; // Animator組件，用於控制動畫
    private string currentState; // 當前動畫狀態

    // 使用枚舉來管理角色狀態
    public enum CharacterState { Idle, Walking, Talking }
    internal CharacterState currentCharacterState; // 當前角色狀態

    // 狀態驅動的行為參數
    public float idleMinTime = 5f;
    public float idleMaxTime = 8f;
    public float talkingMinTime = 5f;
    public float talkingMaxTime = 15f;
    public float roamRadius = 10f;
    public float collisionCooldown = 15f;

    private float cooldownTimer; // 冷卻計時器

    // Animation States
    const string ANIM_WALK = "Walking";
    const string ANIM_IDLE = "Breathing Idle";
    const string ANIM_TALK = "Talking";

    void Start()
    {
        //Time.timeScale = 3;
        // 自主導航: 初始化NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // 動畫狀態管理: 初始化Animator
        animator = GetComponent<Animator>();

        // 狀態驅動的行為: 設置初始狀態為Idle
        SetState(CharacterState.Idle);

        // 啟動狀態管理協程
        StartCoroutine(StateManager());
    }
    void Update()
    {
        // 錯誤處理: 更新冷卻計時器
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }
    void LateUpdate()
    {
        // 錯誤處理: 確保位置同步
        if (currentCharacterState == CharacterState.Idle || currentCharacterState == CharacterState.Talking)
        {
            transform.position = agent.transform.position;
        }
    }
    public bool CanTalk()
    {
        return currentCharacterState != CharacterState.Talking && cooldownTimer <= 0;
    }

    public void FaceTowards(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
    void OnTriggerEnter(Collider other)
    {
        NavMesh_AI otherCharacter = other.GetComponent<NavMesh_AI>();
        if (otherCharacter != null && CanTalk() && otherCharacter.CanTalk())
        {
            // 請求對方開始對話
            otherCharacter.RequestStartTalking(gameObject);

            // 如果對方同意（通過檢查其狀態變化），我們也開始對話
            if (otherCharacter.currentCharacterState == CharacterState.Talking)
            {
                RequestStartTalking(other.gameObject);
            }
            else
            {
                Debug.Log("對方拒絕");
            }

            // 請求對方面向我們（而不是直接操作）
            //otherCharacter.FaceTowards(transform);
            Debug.Log("結束OnTrigger");
        }
    }
    void OnTriggerExit(Collider other)
    {
        // 交互系統: 處理NPC之間的離開事件
        if (other.gameObject == currentTalkingPartner)
        {
            currentTalkingPartner = null;
            SetState(CharacterState.Idle);
            Debug.Log("退出聊天");
        }
    }    
}
