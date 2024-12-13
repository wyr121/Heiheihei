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
        // 玩家不携带厨房物品
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectSOTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }
}
