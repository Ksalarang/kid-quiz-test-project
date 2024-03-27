using System.Collections.Generic;
using PlayScene.Data.Cells;
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

        [Inject]
        private CellData _cellData;
        
        // ReSharper disable Unity.PerformanceAnalysis
        private CardCell Get()
        {
            var cell = _diContainer.InstantiatePrefabForComponent<CardCell>(_cardCellPrefab, _cellContainer);

            var index = Random.Range(0, _cellData.BackgroundColors.Length);
            var color = _cellData.BackgroundColors[index];
            cell.SetBackgroundColor(color);
            
            return cell;
        }

        public List<CardCell> GetCells(int count)
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