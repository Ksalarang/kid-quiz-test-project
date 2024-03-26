using PlayScene.Data.Cards;
using UnityEngine;

namespace PlayScene.CardCells
{
    public class CardCell : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private CardData _cardData;

        public void setCard(CardData cardData)
        {
            _cardData = cardData;
            _spriteRenderer.sprite = cardData.Sprite;
        }
    }
}