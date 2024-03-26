using PlayScene.CardCells;
using PlayScene.Data.Levels;
using PlayScene.Gameplay;
using PlayScene.UI;
using UnityEngine;
using Zenject;

// ReSharper disable All
namespace PlayScene.ZenjectBindings
{
    public class BindingInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _controllers;
        
        [SerializeField]
        private LevelBundleData _levelBundleData;

        [SerializeField]
        private TaskLabel _taskLabel;
        
        public override void InstallBindings()
        {
            // controllers
            Bind(_controllers.GetComponent<GameplayController>());
            Bind(_controllers.GetComponent<CardCellFactory>());
            Bind(_controllers.GetComponent<CardCellGrid>());
            // data
            Bind(_levelBundleData);
            // UI
            Bind(_taskLabel);
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}