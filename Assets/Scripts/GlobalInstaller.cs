using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<AudioService>().AsSingle();
    }
}