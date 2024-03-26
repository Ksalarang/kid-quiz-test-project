using Zenject;

namespace ZenjectBindings
{
    public class BindingInstaller : MonoInstaller
    {
        

        public override void InstallBindings() {
            
        }
    
        void bind<T>(T instance) {
            Container.BindInstance(instance);
        }
    }
}
