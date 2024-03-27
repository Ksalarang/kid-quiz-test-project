using PlayScene.CardCells;
using PlayScene.Data.Cells;
using PlayScene.Data.Levels;
using PlayScene.Gameplay;
using PlayScene.Particles;
using PlayScene.UI;
using UnityEngine;
using Zenject;

namespace PlayScene.ZenjectBindings
{
    public class BindingInstaller : MonoInstaller
    {
        [Header("Controllers")]
        [SerializeField]
        private GameObject _controllers;
        
        [Header("Data")]
        [SerializeField]
        private LevelBundleData _levelBundleData;

        [SerializeField]
        private CellData _cellData;

        [Header("UI")]
        [SerializeField]
        private TaskLabel _taskLabel;

        [SerializeField]
        private RestartPanel _restartPanel;

        [SerializeField]
        private LoadingPanel _loadingPanel;

        public override void InstallBindings()
        {
            // controllers
            Bind(_controllers.GetComponent<GameplayController>());
            Bind(_controllers.GetComponent<CardCellFactory>());
            Bind(_controllers.GetComponent<CardCellGrid>());
            Bind(_controllers.GetComponent<CardCellAnimator>());
            Bind(_controllers.GetComponent<StarParticleController>());
            // data
            Bind(_levelBundleData);
            Bind(_cellData);
            // UI
            Bind(_taskLabel);
            Bind(_restartPanel);
            Bind(_loadingPanel);
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}