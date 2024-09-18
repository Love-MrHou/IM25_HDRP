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

            // 初始化 SpeechSynthesizer
            synthesizer = new SpeechSynthesizer(speechConfig);
            if (synthesizer == null)
            {
                Debug.LogError("Speech synthesizer initialization failed.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to initialize Azure Speech Synthesizer: " + e.Message);
        }
    }

    void OnDisable()
    {
        if (synthesizer != null)
        {
            synthesizer.StopSpeakingAsync().Wait(); // 停止合成操作
        }
    }

    public async void TriggerSpeech()
    {
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

    public async Task SynthesizeSpeech(string text)
    {
        if (synthesizer == null)
        {
            Debug.LogError("Speech synthesizer is not initialized.");
            return;
        }

        using (var result = await synthesizer.SpeakTextAsync(text))
        {
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                Debug.Log("Speech synthesis succeeded.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                Debug.LogError($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Debug.LogError($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Debug.LogError($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                }
            }
        }
    }
}
