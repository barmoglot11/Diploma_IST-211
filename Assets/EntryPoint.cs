using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoInstaller
{
    public GameObject inventoryService;


    public override void InstallBindings()
    {
       Container.Bind<InventoryService>().FromInstance(inventoryService.GetComponent<InventoryService>());
    }

    
}

