using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using System;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource; // 音頻播放器
    public Text dialogueText; // UI對話框的文字
    public TextAsset subtitlesFile; // 外部 txt 檔案
    public float typingSpeed = 0.05f; // 每個字符的顯示間隔（秒）
    public GameObject button; // 按鈕

    private string[] subtitles; // 字幕文本
    private float[] subtitleTimings; // 每個字幕出現的時間（秒）

    void Start()
    {
        // 隱藏按鈕，等待字幕顯示完成後再顯示
        button.SetActive(false);

        // 讀取字幕內容和時間
        ParseSubtitles(subtitlesFile.text);

        // 開始播放音頻
        audioSource.Play();

        // 開始顯示字幕
        StartCoroutine(ShowSubtitles());
    }

    void ParseSubtitles(string txtContent)
    {
        // 拆分txt內容，根據換行符分割每行
        string[] lines = txtContent.Split('\n');
        subtitles = new string[lines.Length];
        subtitleTimings = new float[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            // 假設每一行格式為: "時間,字幕"
            string[] parts = lines[i].Split(',');

            if (parts.Length < 2)
            {
                Debug.LogError($"第 {i + 1} 行的字幕格式不正確: {lines[i]}");
                continue; // 跳過格式不正確的行
            }

            try
            {
                // 移除任何空白，然後將時間轉換為浮點數
                subtitleTimings[i] = float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
                subtitles[i] = parts[1].Trim();
                Debug.Log($"Parsed Subtitle {i}: Time = {subtitleTimings[i]}, Text = {subtitles[i]}");
            }
            catch (FormatException)
            {
                Debug.LogError($"第 {i + 1} 行的時間格式不正確: {parts[0]}");
            }
        }
    }

    IEnumerator ShowSubtitles()
    {
        dialogueText.text = ""; // 清空文字

        for (int i = 0; i < subtitles.Length; i++)
        {
            // 等待字幕開始顯示的時間點
            float waitTime = subtitleTimings[i] - (i > 0 ? subtitleTimings[i - 1] : 0);
            Debug.Log($"Waiting for {waitTime} seconds before showing subtitle {i}");
            yield return new WaitForSeconds(waitTime);

            // 逐字顯示當前字幕
            yield return StartCoroutine(TypeSentence(subtitles[i]));
        }

        // 當字幕顯示完成後，立即顯示按鈕
        button.SetActive(true);
    }

    // 逐字顯示文本，保留之前的文本
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // 逐字添加到對話框

            // 在句號後添加換行符
            if (letter == '。' || letter == '!' || letter == '！' || letter == '.')
            {
                dialogueText.text += "\n"; // 添加換行符
            }

            yield return new WaitForSeconds(typingSpeed); // 等待指定時間後再顯示下一個字符
        }
    }
}
