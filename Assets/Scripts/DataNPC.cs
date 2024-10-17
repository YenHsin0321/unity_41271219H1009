using UnityEngine;

namespace Melody
{
    ///<summary>
    ///NPC資料
    /// </summary>
    [CreateAssetMenu(menuName = "Melody/NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("NPC要分析的語句")]
        public string[] sentences;
    }
}