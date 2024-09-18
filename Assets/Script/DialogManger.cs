using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DialogManger : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        StartDialog(_inkAssets);
        if (story.canContinue) diaglogText.text = story.Continue();
    }
    public Text diaglogText;
    public Button[] buttons;
    public TextAsset _inkAssets;
    Story story = null;
    public bool StartDialog(TextAsset inkAssets)
    {
        if (story != null) return false;
        story = new Story(inkAssets.text); //new Story 裡面放json檔的文字，讓 Story 初始化
        return true;
    }

    public void NextDialog()
    {
        if (story == null) return;

        // 如果故事不能繼續並且沒有選項，則表示對話已經結束
        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            Debug.Log("Dialog End");
            story = null;
            return;
        }

        // 設定選項按鈕，如果有選項
        if (story.currentChoices.Count > 0)
        {
            SetChoices();
        }

        // 檢查是否還有繼續的故事並且返回值不為空
        if (story.canContinue)
        {
            string nextLine = story.Continue();
            if (!string.IsNullOrWhiteSpace(nextLine))
            {
                diaglogText.text = nextLine; // 確保對話框不顯示空白
            }
            else
            {
                Debug.LogWarning("Empty line detected in dialog. Skipping...");
                NextDialog(); // 遇到空白時自動跳過
            }
        }
    }


    private void SetChoices()
    { //依照選項數量，設置按鈕 文字 及 Active
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text; //把story文本內的選項貼到按鈕的text
        }
    }

    public void MakeChoice(int index)
    {
        story.ChooseChoiceIndex(index); //使用 ChooseChoiceIndex 選擇當前選項
        for (int i = 0; i < buttons.Length; i++)
        { //選擇完，將按鈕隱藏
            buttons[i].gameObject.SetActive(false);
        }
        NextDialog();
    }
    public void back()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
