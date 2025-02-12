using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField]
    private Inventory inventory;
    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(inventory).AsSingle().NonLazy();
    }
}
