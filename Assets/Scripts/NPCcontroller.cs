using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Melody
{
    /// <summary>
    /// NPC控制器
    /// </summary>
    public class NPCcontroller : MonoBehaviour
    {
        //序列化欄位：將私人變數顯示在 unity 屬性面板
        [SerializeField, Header("NPC 資料")]
        private DataNPC dataNPC;

        //Unity 的動畫控制系統
        private Animator ani;

        //喚醒事件 : 撥放遊戲會執行一次
        private void Awake()
        {
            //獲得 NPC 身上的動畫控制器
            ani = GetComponent<Animator>();
        }

    }
}
   


