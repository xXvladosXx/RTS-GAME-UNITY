﻿using Codebase.Runtime.CameraSystem;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Test;
using Zenject;

namespace Codebase.Runtime.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelLoader>().AsSingle();
        }
    }
}