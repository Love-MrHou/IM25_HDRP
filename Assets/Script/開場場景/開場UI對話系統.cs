using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using System;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource; // ���W����
    public Text dialogueText; // UI��ܮت���r
    public TextAsset subtitlesFile; // �~�� txt �ɮ�
    public float typingSpeed = 0.05f; // �C�Ӧr�Ū���ܶ��j�]��^
    public GameObject button; // ���s

    private string[] subtitles; // �r���奻
    private float[] subtitleTimings; // �C�Ӧr���X�{���ɶ��]��^

    void Start()
    {
        // ���ë��s�A���ݦr����ܧ�����A���
        button.SetActive(false);

        // Ū���r�����e�M�ɶ�
        ParseSubtitles(subtitlesFile.text);

        // �}�l�����W
        audioSource.Play();

        // �}�l��ܦr��
        StartCoroutine(ShowSubtitles());
    }

    void ParseSubtitles(string txtContent)
    {
        // ���txt���e�A�ھڴ���Ť��ΨC��
        string[] lines = txtContent.Split('\n');
        subtitles = new string[lines.Length];
        subtitleTimings = new float[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            // ���]�C�@��榡��: "�ɶ�,�r��"
            string[] parts = lines[i].Split(',');

            if (parts.Length < 2)
            {
                Debug.LogError($"�� {i + 1} �檺�r���榡�����T: {lines[i]}");
                continue; // ���L�榡�����T����
            }

            try
            {
                // ��������ťաA�M��N�ɶ��ഫ���B�I��
                subtitleTimings[i] = float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
                subtitles[i] = parts[1].Trim();
                Debug.Log($"Parsed Subtitle {i}: Time = {subtitleTimings[i]}, Text = {subtitles[i]}");
            }
            catch (FormatException)
            {
                Debug.LogError($"�� {i + 1} �檺�ɶ��榡�����T: {parts[0]}");
            }
        }
    }

    IEnumerator ShowSubtitles()
    {
        dialogueText.text = ""; // �M�Ť�r

        for (int i = 0; i < subtitles.Length; i++)
        {
            // ���ݦr���}�l��ܪ��ɶ��I
            float waitTime = subtitleTimings[i] - (i > 0 ? subtitleTimings[i - 1] : 0);
            Debug.Log($"Waiting for {waitTime} seconds before showing subtitle {i}");
            yield return new WaitForSeconds(waitTime);

            // �v�r��ܷ�e�r��
            yield return StartCoroutine(TypeSentence(subtitles[i]));
        }

        // ��r����ܧ�����A�ߧY��ܫ��s
        button.SetActive(true);
    }

    // �v�r��ܤ奻�A�O�d���e���奻
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // �v�r�K�[���ܮ�

            // �b�y����K�[�����
            if (letter == '�C' || letter == '!' || letter == '�I' || letter == '.')
            {
                dialogueText.text += "\n"; // �K�[�����
            }

            yield return new WaitForSeconds(typingSpeed); // ���ݫ��w�ɶ���A��ܤU�@�Ӧr��
        }
    }
}
