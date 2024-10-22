using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyProject.Dialogs
{
    public class test : MonoBehaviour
    {
        public Text dialogText;  // 顯示對話的 Text
        public Button[] buttons;  // 主對話框的選項按鈕
        public TextAsset[] inkAssets;  // 多個 Inky 劇本的陣列（每個 NPC 可以有多個劇本）

        // 原先的 Prefab
        public GameObject characterDialogPrefab;  // "人物對話ui(強化版)" prefab
        public GameObject vendorIntroPrefab;  // "廠商背景介紹 (1)" prefab

        // 新增三個Prefab和三個Text
        public GameObject greenVitalPrefab;  // "GreenVital Foods" 的Prefab
        public GameObject elegancePrefab;    // "Elegance Accessories" 的Prefab
        public GameObject ecoEssentialsPrefab;  // "EcoEssentials" 的Prefab

        public Text greenVitalText;  // "GreenVital Foods" 對應的Text
        public Text eleganceText;    // "Elegance Accessories" 對應的Text
        public Text ecoEssentialsText;  // "EcoEssentials" 對應的Text

        // 為每個對話框新增一組按鈕
        public Button[] greenVitalButtons;   // GreenVital Foods 的選項按鈕
        public Button[] eleganceButtons;     // Elegance Accessories 的選項按鈕
        public Button[] ecoEssentialsButtons;  // EcoEssentials 的選項按鈕

        private Story story = null;
        private List<string> dialogHistory = new List<string>();  // 對話歷史紀錄
        private int currentDialogIndex = -1;  // 當前對話索引
        private int currentInkAssetIndex = 0;  // 當前劇本索引

        // 新增一個事件來通知對話更新
        public delegate void OnDialogUpdateHandler(Story story);
        public event OnDialogUpdateHandler OnDialogUpdate;

        void Start()
        {
            // 初始化隱藏所有按鈕
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

            // 初始化隱藏所有 Prefab 和 Text
            HideAllPrefabsAndTexts();

            if (inkAssets.Length > 0)
            {
                // 開始第一個劇本
                StartDialog(inkAssets[currentInkAssetIndex]);
            }
        }

        public bool StartDialog(TextAsset inkAsset)
        {
            if (story != null)
            {
                story = null;  // 重置故事對象
            }

            story = new Story(inkAsset.text);  // 初始化新的 Story
            dialogHistory.Clear();  // 清空對話歷史
            currentDialogIndex = -1;  // 重置對話索引
            NextDialog();  // 顯示對話
            return true;
        }

        public void NextDialog()
        {
            if (story == null) return;

            // 如果對話已經結束，並且沒有可選擇的選項，嘗試切換到下一個劇本
            if (!story.canContinue && story.currentChoices.Count == 0)
            {
                Debug.Log("劇本結束。");

                // 切換到下一個劇本（如果有）
                currentInkAssetIndex++;
                if (currentInkAssetIndex < inkAssets.Length)
                {
                    Debug.Log("切換到下一個劇本。");
                    StartDialog(inkAssets[currentInkAssetIndex]);  // 加載下一個劇本
                    return;
                }

                Debug.Log("所有劇本已完成。");
                return;
            }

            // 如果還有對話可以繼續
            if (story.canContinue)
            {
                string nextLine = story.Continue();
                if (!string.IsNullOrWhiteSpace(nextLine))
                {
                    dialogHistory.Add(nextLine);  // 將對話添加到歷史
                    currentDialogIndex = dialogHistory.Count - 1;
                    dialogText.text = nextLine;  // 顯示新的對話
                }

                // **偵測標籤並控制 prefab 和 text**
                if (story.currentTags.Contains("GreenVital Foods代表"))
                {
                    ShowDialog(greenVitalPrefab, greenVitalText, nextLine, greenVitalButtons);
                }
                else if (story.currentTags.Contains("Elegance Accessories代表"))
                {
                    ShowDialog(elegancePrefab, eleganceText, nextLine, eleganceButtons);
                }
                else if (story.currentTags.Contains("EcoEssentials代表"))
                {
                    ShowDialog(ecoEssentialsPrefab, ecoEssentialsText, nextLine, ecoEssentialsButtons);
                }
                else if (story.currentTags.Contains("系統分析師1"))
                {
                    // 顯示/隱藏原有的 "人物對話ui(強化版)" 和 "廠商背景介紹 (1)" prefab
                    if (characterDialogPrefab != null)
                    {
                        characterDialogPrefab.SetActive(false);
                    }

                    if (vendorIntroPrefab != null)
                    {
                        vendorIntroPrefab.SetActive(true);
                    }
                }

                // 發送通知，讓其他系統知道對話更新了
                OnDialogUpdate?.Invoke(story);

                // 顯示選項按鈕（如果有）
                if (story.currentChoices.Count > 0)
                {
                    SetChoices();
                }
                else
                {
                    // 隱藏所有選項按鈕
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void ShowDialog(GameObject prefab, Text text, string nextLine, Button[] buttons)
        {
            HideAllPrefabsAndTexts();  // 隱藏所有Prefab和Text
            prefab.SetActive(true);  // 顯示當前Prefab
            text.gameObject.SetActive(true);  // 顯示當前Text
            text.text = nextLine;  // 更新Text的內容

            // 顯示選項按鈕
            SetChoices(buttons);
        }

        private void SetChoices(Button[] buttons = null)
        {
            Button[] currentButtons = buttons != null ? buttons : this.buttons;

            // 顯示選項按鈕，並設置選項文本
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                currentButtons[i].gameObject.SetActive(true);
                currentButtons[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text;

                // 使用局部變數捕捉選項索引，避免閉包問題
                int choiceIndex = i;
                currentButtons[i].onClick.RemoveAllListeners();
                currentButtons[i].onClick.AddListener(() => MakeChoice(choiceIndex));
            }

            // 隱藏多餘的按鈕
            for (int i = story.currentChoices.Count; i < currentButtons.Length; i++)
            {
                currentButtons[i].gameObject.SetActive(false);
            }
        }

        public void MakeChoice(int index)
        {
            story.ChooseChoiceIndex(index);  // 選擇對應選項
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);  // 隱藏選項按鈕
            }
            NextDialog();
        }

        private void HideAllPrefabsAndTexts()
        {
            // 隱藏所有Prefab
            greenVitalPrefab.SetActive(false);
            elegancePrefab.SetActive(false);
            ecoEssentialsPrefab.SetActive(false);

            // 隱藏原有的 Prefab
            //if (characterDialogPrefab != null)
                //characterDialogPrefab.SetActive(false);

            if (vendorIntroPrefab != null)
                vendorIntroPrefab.SetActive(false);

            // 隱藏所有Text
            greenVitalText.gameObject.SetActive(false);
            eleganceText.gameObject.SetActive(false);
            ecoEssentialsText.gameObject.SetActive(false);
        }

        public void back()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
