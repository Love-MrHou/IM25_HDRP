using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class RepresentativeDialog : MonoBehaviour
{
    public Text dialogText;  // 對應的文本顯示
    public Button[] buttons;  // 五個選項按鈕

    private Story currentStory;

    // 設定當前對話框的文本和選項
    public void UpdateDialog(Story story)
    {
        currentStory = story;
        string nextLine = story.Continue();
        dialogText.text = nextLine;

        SetChoices();
    }

    private void SetChoices()
    {
        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = currentStory.currentChoices[i].text;

            int choiceIndex = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => MakeChoice(choiceIndex));
        }

        // 隱藏多餘的按鈕
        for (int i = currentStory.currentChoices.Count; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        UpdateDialog(currentStory);  // 更新對話進度
    }
}
