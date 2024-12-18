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
        [SerializeField, Header("NPC 資料")]
        private DataNPC dataNPC;

        [SerializeField, Header("動畫參數")]
        private string[] parameters =
        {
            "觸發閃躲", "觸發跳躍", "觸發奔跑"
        };

        [SerializeField, Header("移動速度")]
        private float moveSpeed = 5f; // 控制移動速度

        [SerializeField, Header("旋轉速度")]
        private float rotationSpeed = 720f; // 控制旋轉速度

        private Animator ani;

        public DataNPC data => dataNPC;

        // 喚醒事件 : 撥放遊戲會執行一次
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovement();
        }

        /// <summary>
        /// 播放動畫
        /// </summary>
        /// <param name="index">動畫參數索引</param>
        public void PlayAinmation(int index)
        {
            ani.SetTrigger(parameters[index]);
        }

        /// <summary>
        /// 處理角色移動
        /// </summary>
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal"); // A/D 或 左/右 鍵輸入
            float vertical = Input.GetAxis("Vertical");   // W/S 或 上/下 鍵輸入

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // 計算目標角度並平滑旋轉
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // 移動角色
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

                // 啟動走路動畫
                if (ani != null)
                {
                    ani.SetBool("IsWalking", true);
                }
            }
            else
            {
                // 停止走路動畫
                if (ani != null)
                {
                    ani.SetBool("IsWalking", false);
                }
            }
        }
    }
}

