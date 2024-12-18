using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace KID
{
    /// <summary>
    /// 根據玩家食物喜好推薦食物的模型管理器
    /// </summary>
    public class FoodRecommendationManager : MonoBehaviour
    {
        [Header("API 設定")]
        [SerializeField] private string url = "https://g.ubitus.ai/v1/chat/completions"; // API 連結
        [SerializeField] private string key = "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"; // 請填入正確的 API Key

        [Header("角色設定")]
        private string role = "你是一位美食專家，根據玩家的口味推薦最合適的食物。";

        [Header("UI 元件")]
        [SerializeField] private TMP_Text textResult;      // 用於顯示角色回應
        [SerializeField] private TMP_InputField inputField; // 玩家輸入欄位

        private List<object> messages = new List<object>(); // 儲存對話歷史

        private void Awake()
        {
            // 當玩家結束輸入時執行 HandlePlayerInput 方法
            inputField.onEndEdit.AddListener(HandlePlayerInput);

            // 初始化角色招呼語
            string greeting = "嗨！告訴我你的食物喜好，我會推薦你美味的選擇！";
            AddMessage("assistant", greeting);
            DisplayResponse(greeting);
        }

        /// <summary>
        /// 處理玩家輸入
        /// </summary>
        /// <param name="input">玩家的輸入內容</param>
        private void HandlePlayerInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            // 添加玩家的輸入到對話歷史
            AddMessage("user", input);

            // 清空輸入欄位
            inputField.text = "";

            // 啟動協同程序，獲得模型回應
            StartCoroutine(GetFoodRecommendation());
        }

        /// <summary>
        /// 從 API 獲取推薦結果
        /// </summary>
        private IEnumerator GetFoodRecommendation()
        {
            // 準備要發送的資料
            var data = new
            {
                model = "llama-3.1-70b",
                messages = messages.ToArray(),
                max_tokens = 300,
                temperature = 0.7,
                top_p = 0.9,
                stream = false
            };

            string json = JsonConvert.SerializeObject(data);
            byte[] postData = Encoding.UTF8.GetBytes(json);

            // 建立 HTTP POST 請求
            UnityWebRequest request = new UnityWebRequest(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer d4pHv5n2G3q2vkVMPen3vFMfUJic4huKiQbvMmGLWUVIr/ptUuGnsCx/zVdYmVtdrGPO9//2h8Fbp6HkSQ0/oA==");


            yield return request.SendWebRequest();

            // 處理回應
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"API 錯誤: {request.error}");
                DisplayResponse("抱歉，發生了錯誤，請稍後再試。");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                JObject jsonResponse = JObject.Parse(responseText);
                string content = jsonResponse["choices"][0]["message"]["content"].ToString();

                // 添加模型回應到對話歷史
                AddMessage("assistant", content);

                // 顯示回應
                DisplayResponse(content);
            }
        }

        /// <summary>
        /// 顯示角色回應
        /// </summary>
        /// <param name="response">模型回傳的文字</param>
        private void DisplayResponse(string response)
        {
            textResult.text = response; // 顯示角色的推薦結果
        }

        /// <summary>
        /// 添加訊息到對話歷史
        /// </summary>
        /// <param name="role">角色類型：user 或 assistant</param>
        /// <param name="content">對話內容</param>
        private void AddMessage(string role, string content)
        {
            messages.Add(new { role = role, content = content });
        }
    }
}
