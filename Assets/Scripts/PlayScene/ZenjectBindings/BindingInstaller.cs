using PlayScene.CardCells;
using PlayScene.Data;
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
        private CardBundleListData _cardBundles;
        
        public override void InstallBindings()
        {
            // controllers
            Bind(_controllers.GetComponent<GameplayController>());
            Bind(_controllers.GetComponent<CardCellFactory>());
            Bind(_controllers.GetComponent<CardCellGrid>());
            // data
            Bind(_cardBundles);
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}