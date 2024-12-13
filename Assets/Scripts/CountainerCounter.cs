using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // ��Ҳ�Я��������Ʒ
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }
}
