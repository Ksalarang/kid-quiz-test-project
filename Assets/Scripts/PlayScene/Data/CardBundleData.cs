using UnityEngine;

namespace PlayScene.Data
{
    [CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data", order = 0)]
    public class CardBundleData : ScriptableObject
    {
        [SerializeField]
        private CardData[] _cardDataList;

        public CardData[] CardDataList => _cardDataList;
    }
}