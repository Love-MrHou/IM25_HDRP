using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using UnityEngine;
using UnityEngine.UI;

public class TTSDemo : MonoBehaviour
{
    public AudioSource audioSource;
    public Text textUI;  // �Ψ���ܩM�x�s�n�ഫ���y������r

    private string previousText = "";
    private string currentText = ""; // �Ψ��x�s�ثe�bText UI���n�ഫ����r
    private SpeechSynthesizer synthesizer;

    void Start()
    {
        if (textUI == null)
        {
            Debug.LogError("Text UI is not assigned! Please assign a Text UI element.");
        }

        var config = SpeechConfig.FromSubscription("155998f0555f47ae9ad78430ef6491aa", "eastus");
        config.SpeechSynthesisLanguage = "zh-CN";
        config.SpeechSynthesisVoiceName = "zh-CN-XiaoxiaoNeural";
        var audioConfig = AudioConfig.FromDefaultSpeakerOutput();
        synthesizer = new SpeechSynthesizer(config, audioConfig);
    }

    void Update()
    {
        // �p�GText UI������r�M���e����r���P�A�h�i��y���X��
        if (textUI != null && textUI.text != previousText)
        {
            currentText = textUI.text; // �NText UI������r��s��currentText

            // �p�G��e�y�����b����A����ä��_
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // ����ثe���b���񪺭��W
            }

            // �p�G���b�i��y���X���A����X��
            StopCurrentSpeechSynthesis();

            // ���s�X���ü���s���y��
            SynthesizeAndPlayText(currentText);
            previousText = currentText; // ��spreviousText
        }
    }

    public async void SynthesizeAndPlayText(string text)
    {
        var result = await synthesizer.SpeakTextAsync(text); // �X��Text UI������r

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            var sampleCount = result.AudioData.Length / 2;
            var audioData = new float[sampleCount];
            for (var i = 0; i < sampleCount; ++i)
            {
                audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
            }

            var audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
            audioClip.SetData(audioData, 0);
            audioSource.clip = audioClip;
            audioSource.Play();

            Debug.Log("�y���X�����\�I");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Debug.LogError(cancellation.ErrorDetails);
        }
    }

    // �����e�y���X�������
    public void StopCurrentSpeechSynthesis()
    {
        if (synthesizer != null)
        {
            synthesizer.StopSpeakingAsync().Wait(); // ����ثe���y���X��
        }
    }

    // �Ψӧ�sText UI������r����ơA��~���ݭn��s�ɥi�H�եΦ����
    public void UpdateTextUI(string newText)
    {
        if (textUI != null)
        {
            textUI.text = newText; // �NText UI������r��s
        }
        else
        {
            Debug.LogError("Text UI is not assigned.");
        }
    }
}
