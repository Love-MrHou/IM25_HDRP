using Ink.Runtime;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class 系統分析師腳本
    : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        StartDialog(_inkAssets);
        NextDialog(); // 初始化对话
    }

    public Text diaglogText; // 显示剧情的text
    public Button[] buttons; // 选项的按钮
    public TextAsset _inkAssets; // inky剧本
    private List<string> dialogHistory = new List<string>(); // 存储对话记录
    private int currentDialogIndex = -1; // 当前的剧情index
    Story story = null;

    public bool StartDialog(TextAsset inkAssets)
    {
        if (story != null) return false;
        story = new Story(inkAssets.text); // 初始化Story
        return true;
    }

    public void ShowPreviousDialog()
    {
        if (currentDialogIndex > 0) // 确保不会超出范围
        {
            currentDialogIndex--;
            diaglogText.text = dialogHistory[currentDialogIndex]; // 显示上一句对话

            // 隐藏所有选项按钮
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("已经是第一句对话了。");
        }

        Debug.Log(dialogHistory[currentDialogIndex]);
    }

    public void NextDialog()
    {
        if (story == null) return;

        // 检查对话是否结束
        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            Debug.Log("Dialog Ended. No more choices.");
            story = null; // 对话结束
            return;
        }

        // 检查用户是否在浏览历史对话
        if (currentDialogIndex < dialogHistory.Count - 1)
        {
            currentDialogIndex++;
            diaglogText.text = dialogHistory[currentDialogIndex];

            // 隐藏按钮，如果没有选项
            if (story.currentChoices.Count > 0 && currentDialogIndex == dialogHistory.Count - 1)
            {
                SetChoices(); // 显示选项
            }
            else
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }
        else if (story.canContinue)
        {
            // 如果历史记录已经到底，继续下一句故事
            string nextLine = story.Continue();
            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                dialogHistory.Add(nextLine);
                currentDialogIndex++; // 移动到下一句对话
                diaglogText.text = nextLine; // 显示对话
            }
            else
            {
                Debug.LogWarning("Empty line detected in dialog. Skipping...");
                NextDialog(); // 遇到空白时自动跳过
            }

            // 设置选项按钮，如果有选项
            if (story.currentChoices.Count > 0)
            {
                Debug.Log("Choices are available.");
                SetChoices();
            }
            else
            {
                Debug.Log("No choices available.");
            }
        }

        Debug.Log(dialogHistory[currentDialogIndex]);
    }

    private void SetChoices()
    {
        // 隐藏所有按钮
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        // 显示可用的选项按钮
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text;
        }
    }

    public void MakeChoice(int index)
    {
        story.ChooseChoiceIndex(index); // 使用选择的选项
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false); // 隐藏选项按钮
        }
        NextDialog(); // 显示下一个对话
    }

    public void back()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
