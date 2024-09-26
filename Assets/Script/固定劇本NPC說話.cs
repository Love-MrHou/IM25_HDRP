using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

public class AzureSpeechSynthesizer : MonoBehaviour
{
    private string subscriptionKey = "84bc71ce611543798efa1d003f9ae3cd"; // 替换为您的订阅金钥
    private string region = "eastus"; // 替换为您的服务区域 
    [SerializeField] private TextAsset speakTextFile; // 用于存放文本文件的变量
    private SpeechConfig speechConfig;
    private SpeechSynthesizer synthesizer;
    private bool isSpeaking = false; // 记录是否正在合成语音

    void Start()
    {
        if (string.IsNullOrEmpty(subscriptionKey) || string.IsNullOrEmpty(region))
        {
            Debug.LogError("Azure subscription key or region is not set.");
            return;
        }

        try
        {
            // 初始化 SpeechConfig
            speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
            speechConfig.SpeechSynthesisVoiceName = "zh-CN-XiaoxiaoNeural"; // 设置语音特性
            Debug.Log("SpeechConfig initialized.");

            // 初始化 SpeechSynthesizer
            synthesizer = new SpeechSynthesizer(speechConfig);
            if (synthesizer == null)
            {
                Debug.LogError("Speech synthesizer initialization failed.");
            }
            else
            {
                Debug.Log("Speech synthesizer initialized successfully.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to initialize Azure Speech Synthesizer: " + e.Message);
        }
    }

    void OnDisable()
    {
        StopSpeech(); // 停止语音播放
    }

    public async void TriggerSpeech()
    {
        // 延时，确保 SpeechSynthesizer 已经完成初始化
        await Task.Delay(500); // 延时 500 毫秒

        if (speakTextFile != null && synthesizer != null)
        {
            string text = speakTextFile.text;
            await SynthesizeSpeech(text);
        }
        else
        {
            if (speakTextFile == null)
                Debug.LogError("Speak text file is not assigned.");

            if (synthesizer == null)
                Debug.LogError("Speech synthesizer is not initialized.");
        }
    }

    void OnEnable()
    {
        if (synthesizer == null)
        {
            try
            {
                // 重新初始化 SpeechSynthesizer
                speechConfig = SpeechConfig.FromSubscription(subscriptionKey, region);
                speechConfig.SpeechSynthesisVoiceName = "zh-CN-XiaoxiaoNeural";
                synthesizer = new SpeechSynthesizer(speechConfig);
                Debug.Log("Speech synthesizer reinitialized successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to reinitialize Azure Speech Synthesizer: " + e.Message);
            }
        }
    }

    public async Task SynthesizeSpeech(string text)
    {
        if (synthesizer == null)
        {
            Debug.LogError("Speech synthesizer is not initialized.");
            return;
        }

        isSpeaking = true; // 标记正在播放语音

        using (var result = await synthesizer.SpeakTextAsync(text))
        {

            if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            }
        }

        isSpeaking = false; // 语音播放结束
    }

    public async void StopSpeech()
    {
        if (isSpeaking && synthesizer != null)
        {
            try
            {
                await synthesizer.StopSpeakingAsync(); // 异步停止语音合成
                Debug.Log("Speech stopped successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to stop speech synthesis: " + e.Message);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopSpeech(); // 当玩家离开时，停止语音播放
        }
    }
}
