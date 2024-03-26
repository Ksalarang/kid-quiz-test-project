using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PlayScene.CardCells
{
    public class CardCellFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cardCellPrefab;
        
        [SerializeField]
        private Transform _cellContainer;

        [Inject]
        private DiContainer _diContainer;

        // ReSharper disable Unity.PerformanceAnalysis
        public CardCell Get()
        {
            return _diContainer.InstantiatePrefabForComponent<CardCell>(_cardCellPrefab, _cellContainer);
        }

        public List<CardCell> GetList(int count)
        {
            var cells = new List<CardCell>();
            
            for (var i = 0; i < count; i++)
            {
                cells.Add(Get());
            }
            
            return cells;
        }

        public void DestroyCells(List<CardCell> cells)
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
        }
    }
}