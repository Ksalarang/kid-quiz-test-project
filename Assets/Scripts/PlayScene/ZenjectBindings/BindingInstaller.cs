using Zenject;

namespace PlayScene.ZenjectBindings
{
    public class BindingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
        }

        private void Bind<T>(T instance)
        {
            Container.BindInstance(instance);
        }
    }
}
