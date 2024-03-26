using System;
using UnityEngine;

namespace PlayScene.Data.Levels
{
    [Serializable]
    public class LevelData
    {
        [SerializeField]
        private int _cellAmount;

        [SerializeField]
        private CardBundleData _cardBundleData;
    }
}