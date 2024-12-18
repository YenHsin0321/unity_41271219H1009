using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Melody
{
    /// <summary>
    /// NPC���
    /// </summary>
    public class NPCcontroller : MonoBehaviour
    {
        [SerializeField, Header("NPC ���")]
        private DataNPC dataNPC;

        [SerializeField, Header("�ʵe�Ѽ�")]
        private string[] parameters =
        {
            "Ĳ�o�{��", "Ĳ�o���D", "Ĳ�o�b�]"
        };

        [SerializeField, Header("���ʳt��")]
        private float moveSpeed = 5f; // ����ʳt��

        [SerializeField, Header("����t��")]
        private float rotationSpeed = 720f; // �������t��

        private Animator ani;

        public DataNPC data => dataNPC;

        // ����ƥ� : ����C���|����@��
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleMovement();
        }

        /// <summary>
        /// ����ʵe
        /// </summary>
        /// <param name="index">�ʵe�ѼƯ���</param>
        public void PlayAinmation(int index)
        {
            ani.SetTrigger(parameters[index]);
        }

        /// <summary>
        /// �B�z���Ⲿ��
        /// </summary>
        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal"); // A/D �� ��/�k ���J
            float vertical = Input.GetAxis("Vertical");   // W/S �� �W/�U ���J

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                // �p��ؼШ��רå��Ʊ���
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // ���ʨ���
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

                // �Ұʨ����ʵe
                if (ani != null)
                {
                    ani.SetBool("IsWalking", true);
                }
            }
            else
            {
                // ������ʵe
                if (ani != null)
                {
                    ani.SetBool("IsWalking", false);
                }
            }
        }
    }
}

