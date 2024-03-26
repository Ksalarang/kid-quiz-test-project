using UnityEngine;

namespace PlayScene.Data
{
    [CreateAssetMenu(fileName = "New CardBundleListData", menuName = "Card Bundle List", order = 0)]
    public class CardBundleListData : ScriptableObject
    {
        [SerializeField]
        private CardBundleData[] _cardBundleList;

        public CardBundleData[] CardBundleList => _cardBundleList;
    }
}