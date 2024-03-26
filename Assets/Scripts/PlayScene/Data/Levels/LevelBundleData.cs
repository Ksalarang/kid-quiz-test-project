using UnityEngine;

namespace PlayScene.Data.Levels
{
    [CreateAssetMenu(fileName = "New LevelBundleData", menuName = "Level Bundle Data", order = 0)]
    public class LevelBundleData : ScriptableObject
    {
        [SerializeField]
        private LevelData[] _levelDataList;

        public LevelData[] LevelDataList => _levelDataList;
    }
}