using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUIandObject : MonoBehaviour
{
    public GameObject Canva;
    public GameObject exclamationMark;
    private AzureSpeechSynthesizer speechSynthesizer;

    void Start()
    {
        if (Canva == null)
        {
            Debug.LogError("Canva object is not assigned.");
            return;
        }

        Canva.SetActive(false);

        speechSynthesizer = Canva.GetComponent<AzureSpeechSynthesizer>();
        if (speechSynthesizer == null)
        {
            Debug.LogError("AzureSpeechSynthesizer component is not attached to the Canva object.");
        }
        else
        {
            speechSynthesizer.enabled = false; // 初始时禁用
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 确保只有玩家触发
        {
            exclamationMark.SetActive(false);
            EnableUIWithSpeech();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 确保只有玩家触发
        {
            exclamationMark.SetActive(true);
            DisableUIWithSpeech();
        }
    }

    private void EnableUIWithSpeech()
    {
        Canva.SetActive(true);
        if (speechSynthesizer != null)
        {
            speechSynthesizer.enabled = true; // 启用脚本
            speechSynthesizer.TriggerSpeech(); // 开始语音播放
        }
    }

    private void DisableUIWithSpeech()
    {
        Canva.SetActive(false);
        if (speechSynthesizer != null)
        {
            speechSynthesizer.enabled = false; // 禁用脚本
        }
    }
}
