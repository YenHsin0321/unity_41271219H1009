using UnityEngine;

namespace Melody
{
    ///<summary>
    ///NPC���
    /// </summary>
    [CreateAssetMenu(menuName = "Melody/NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("NPC�n���R���y�y")]
        public string[] sentences;
    }
}