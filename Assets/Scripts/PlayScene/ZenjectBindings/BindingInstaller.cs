using PlayScene.CardCells;
using UnityEngine;
using Zenject;

// ReSharper disable All
namespace PlayScene.ZenjectBindings
{
    public class BindingInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _controllers;
        
        public override void InstallBindings()
        {
            // controllers
            Bind(_controllers.GetComponent<GameplayController>());
            Bind(_controllers.GetComponent<CardCellFactory>());
            Bind(_controllers.GetComponent<CardCellGrid>());
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}