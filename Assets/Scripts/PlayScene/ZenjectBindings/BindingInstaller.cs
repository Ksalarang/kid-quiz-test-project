using PlayScene.CardCells;
using PlayScene.Data;
using PlayScene.Data.Cards;
using PlayScene.Data.Levels;
using PlayScene.Gameplay;
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
        
        public override void InstallBindings()
        {
            // controllers
            Bind(_controllers.GetComponent<GameplayController>());
            Bind(_controllers.GetComponent<CardCellFactory>());
            Bind(_controllers.GetComponent<CardCellGrid>());
            // data
            Bind(_levelBundleData);
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}