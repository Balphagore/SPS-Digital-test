using UnityEngine;
using Zenject;

using SPSDigital.Player;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerSystem playerSystem;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerSystem>().FromInstance(playerSystem);
    }
}