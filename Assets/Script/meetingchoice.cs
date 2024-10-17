using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class meetingchoice : MonoBehaviour
{
    // 每個角色對話框的Text和三個Button，以及一個"下一步"按鈕
    public Text systemAnalystText;
    public Button systemAnalystButton1;
    public Button systemAnalystButton2;
    public Button systemAnalystButton3;
    public Button systemAnalystNextButton; // 系統分析師的下一步按鈕

    public Text engineerText;
    public Button engineerButton1;
    public Button engineerButton2;
    public Button engineerButton3;
    public Button engineerNextButton; // 工程師的下一步按鈕

    public Text designerText;
    public Button designerButton1;
    public Button designerButton2;
    public Button designerButton3;
    public Button designerNextButton; // 設計師的下一步按鈕

    public TextAsset _inkAssets; // Inky 劇本
    private List<string> dialogHistory = new List<string>(); // 儲存對話紀錄
    private int currentDialogIndex = -1; // 目前的劇情index
    Story story = null;

    void Start()
    {
        HideAllDialogues(); // 初始化時隱藏所有角色的對話框
        StartDialog(_inkAssets);
        if (story.canContinue)
        {
            string nextLine = story.Continue();
            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                dialogHistory.Add(nextLine); // 將對話添加到歷史
                currentDialogIndex = dialogHistory.Count - 1; // 更新當前對話索引
                ShowDialogue(nextLine); // 顯示對話內容並更新UI
            }
            else
            {
                Debug.LogWarning("Empty line detected in dialog. Skipping...");
                NextDialog(); // 遇到空白時自動跳過
            }
        }

        // 綁定“下一步”按鈕的事件
        systemAnalystNextButton.onClick.AddListener(NextDialog);
        engineerNextButton.onClick.AddListener(NextDialog);
        designerNextButton.onClick.AddListener(NextDialog);
    }

    public bool StartDialog(TextAsset inkAssets)
    {
        if (story != null) return false;
        story = new Story(inkAssets.text); // 初始化Story
        return true;
    }

    public void NextDialog()
    {
        if (story == null) return;

        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            Debug.Log("Dialog End");
            story = null;
            return;
        }

        if (currentDialogIndex < dialogHistory.Count - 1)
        {
            currentDialogIndex++;
            ShowDialogue(dialogHistory[currentDialogIndex]);
        }
        else if (story.canContinue)
        {
            string nextLine = story.Continue();
            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                dialogHistory.Add(nextLine);
                currentDialogIndex++;
                ShowDialogue(nextLine);
            }
            else
            {
                Debug.LogWarning("Empty line detected in dialog. Skipping...");
                NextDialog(); // 遇到空白時自動跳過
            }

            // 設定選項按鈕（如果有選項）
            if (story.currentChoices.Count > 0)
            {
                SetChoices();
            }
        }
    }

    private void SetChoices()
    {
        // 每次顯示選項之前，隱藏所有按鈕
        systemAnalystButton1.gameObject.SetActive(false);
        systemAnalystButton2.gameObject.SetActive(false);
        systemAnalystButton3.gameObject.SetActive(false);

        engineerButton1.gameObject.SetActive(false);
        engineerButton2.gameObject.SetActive(false);
        engineerButton3.gameObject.SetActive(false);

        designerButton1.gameObject.SetActive(false);
        designerButton2.gameObject.SetActive(false);
        designerButton3.gameObject.SetActive(false);

        // 根據選項數量，顯示並設置對應按鈕
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            var choiceText = story.currentChoices[i].text;

            // 捕捉當前索引到局部變數，避免閉包問題
            int choiceIndex = i;

            // 根據角色顯示對應的按鈕
            switch (GetCurrentSpeaker())
            {
                case "系統分析師":
                    if (i == 0)
                    {
                        systemAnalystButton1.GetComponentInChildren<Text>().text = choiceText;
                        systemAnalystButton1.gameObject.SetActive(true);
                        systemAnalystButton1.onClick.RemoveAllListeners();
                        systemAnalystButton1.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 1)
                    {
                        systemAnalystButton2.GetComponentInChildren<Text>().text = choiceText;
                        systemAnalystButton2.gameObject.SetActive(true);
                        systemAnalystButton2.onClick.RemoveAllListeners();
                        systemAnalystButton2.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 2)
                    {
                        systemAnalystButton3.GetComponentInChildren<Text>().text = choiceText;
                        systemAnalystButton3.gameObject.SetActive(true);
                        systemAnalystButton3.onClick.RemoveAllListeners();
                        systemAnalystButton3.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    break;

                case "工程師":
                    if (i == 0)
                    {
                        engineerButton1.GetComponentInChildren<Text>().text = choiceText;
                        engineerButton1.gameObject.SetActive(true);
                        engineerButton1.onClick.RemoveAllListeners();
                        engineerButton1.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 1)
                    {
                        engineerButton2.GetComponentInChildren<Text>().text = choiceText;
                        engineerButton2.gameObject.SetActive(true);
                        engineerButton2.onClick.RemoveAllListeners();
                        engineerButton2.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 2)
                    {
                        engineerButton3.GetComponentInChildren<Text>().text = choiceText;
                        engineerButton3.gameObject.SetActive(true);
                        engineerButton3.onClick.RemoveAllListeners();
                        engineerButton3.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    break;

                case "設計師":
                    if (i == 0)
                    {
                        designerButton1.GetComponentInChildren<Text>().text = choiceText;
                        designerButton1.gameObject.SetActive(true);
                        designerButton1.onClick.RemoveAllListeners();
                        designerButton1.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 1)
                    {
                        designerButton2.GetComponentInChildren<Text>().text = choiceText;
                        designerButton2.gameObject.SetActive(true);
                        designerButton2.onClick.RemoveAllListeners();
                        designerButton2.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    else if (i == 2)
                    {
                        designerButton3.GetComponentInChildren<Text>().text = choiceText;
                        designerButton3.gameObject.SetActive(true);
                        designerButton3.onClick.RemoveAllListeners();
                        designerButton3.onClick.AddListener(() => MakeChoice(choiceIndex));
                    }
                    break;
            }
        }
    }



    public void MakeChoice(int index)
    {
        // 檢查選項索引是否在範圍內
        if (index >= 0 && index < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(index); // 選擇對應的選項
            HideAllDialogues(); // 隱藏所有角色的對話框
            NextDialog(); // 顯示下一段對話
        }
        else
        {
            Debug.LogError("選項索引超出範圍：" + index);
        }
    }


    public void back()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // 顯示當前角色的對話框和內容，並顯示對應的下一步按鈕
    private void ShowDialogue(string nextLine)
    {
        HideAllDialogues(); // 每次顯示前先隱藏所有角色的對話框

        // 調試代碼：輸出當前標註列表
        if (story.currentTags.Count > 0)
        {
            Debug.Log("標註: " + string.Join(", ", story.currentTags));
        }
        else
        {
            Debug.LogWarning("未找到標註");
        }

        string currentSpeaker = GetCurrentSpeaker(); // 取得當前講話角色1
        switch (currentSpeaker)
        {
            case "系統分析師":
                systemAnalystText.text = nextLine;
                systemAnalystText.gameObject.SetActive(true);
                systemAnalystNextButton.gameObject.SetActive(true); // 顯示系統分析師的下一步按鈕
                break;
            case "工程師":
                engineerText.text = nextLine;
                engineerText.gameObject.SetActive(true);
                engineerNextButton.gameObject.SetActive(true); // 顯示工程師的下一步按鈕
                break;
            case "設計師":
                designerText.text = nextLine;
                designerText.gameObject.SetActive(true);
                designerNextButton.gameObject.SetActive(true); // 顯示設計師的下一步按鈕
                break;
            default:
                Debug.LogWarning("未知的角色標註: " + currentSpeaker);
                break;
        }
    }


    // 根據當前對話段落的標註來取得講話的角色
    private string GetCurrentSpeaker()
    {
        if (story.currentTags.Count > 0)
        {
            return story.currentTags[0]; // 假設角色標註為第一個tag
        }
        return string.Empty;
    }

    // 隱藏所有角色的對話框和按鈕
    private void HideAllDialogues()
    {
        systemAnalystText.gameObject.SetActive(false);
        systemAnalystButton1.gameObject.SetActive(false);
        systemAnalystButton2.gameObject.SetActive(false);
        systemAnalystButton3.gameObject.SetActive(false);
        systemAnalystNextButton.gameObject.SetActive(false); // 隱藏系統分析師的下一步按鈕

        engineerText.gameObject.SetActive(false);
        engineerButton1.gameObject.SetActive(false);
        engineerButton2.gameObject.SetActive(false);
        engineerButton3.gameObject.SetActive(false);
        engineerNextButton.gameObject.SetActive(false); // 隱藏工程師的下一步按鈕

        designerText.gameObject.SetActive(false);
        designerButton1.gameObject.SetActive(false);
        designerButton2.gameObject.SetActive(false);
        designerButton3.gameObject.SetActive(false);
        designerNextButton.gameObject.SetActive(false); // 隱藏設計師的下一步按鈕
    }
}
