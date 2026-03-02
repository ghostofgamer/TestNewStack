using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Wallet>().AsSingle().WithArguments(100);
        }
    }
}
