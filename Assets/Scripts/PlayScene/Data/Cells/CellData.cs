using UnityEngine;

namespace PlayScene.Data.Cells
{
    [CreateAssetMenu(fileName = "New CellData", menuName = "Cell Data", order = 0)]
    public class CellData : ScriptableObject
    {
        [SerializeField]
        private Color[] _backgroundColors;

        public Color[] BackgroundColors => _backgroundColors;
    }
}