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
        //�ǦC�����G�N�p�H�ܼ���ܦb unity �ݩʭ��O
        [SerializeField, Header("NPC ���")]
        private DataNPC dataNPC;

        //Unity ���ʵe����t��
        private Animator ani;

        //����ƥ� : ����C���|����@��
        private void Awake()
        {
            //��o NPC ���W���ʵe���
            ani = GetComponent<Animator>();
        }

    }
}
   


