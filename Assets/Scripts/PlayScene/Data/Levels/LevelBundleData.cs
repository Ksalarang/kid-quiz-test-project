using UnityEngine;

namespace PlayScene.Data.Levels
{
    [CreateAssetMenu(fileName = "New LevelBundleData", menuName = "Level Bundle Data", order = 0)]
    public class LevelBundleData : ScriptableObject
    {
        [SerializeField]
        private int _gridWidth;
        
        [SerializeField]
        private LevelData[] _levelDataList;

        public int GridWidth => _gridWidth;
        
        public LevelData[] LevelDataList => _levelDataList;
    }
}