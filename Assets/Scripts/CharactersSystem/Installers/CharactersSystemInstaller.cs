using UnityEngine;
using Zenject;

namespace SPSDigital.Characters
{
    public class CharactersInstaller : MonoInstaller
    {
        [SerializeField]
        private CharactersSystem charactersSystem;

        public override void InstallBindings()
        {
            Container.Bind<ICharactersSystem>().FromInstance(charactersSystem);
        }
    }
}