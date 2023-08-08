using UnityEngine;
using Zenject;

namespace SPSDigital.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private UISystem uISystem;

        public override void InstallBindings()
        {
            Container.Bind<IUISystem>().FromInstance(uISystem);
        }
    }
}