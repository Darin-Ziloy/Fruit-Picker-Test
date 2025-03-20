using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private MobileInput mobileInput;

    public override void InstallBindings()
    {
        Container.Bind<IMobileInput>()
            .To<MobileInput>()
            .FromInstance(mobileInput)
            .AsSingle();
    }
}