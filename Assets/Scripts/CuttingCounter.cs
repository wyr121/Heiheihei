using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // 桌台没有厨房物品
            if (player.HasKitchenObject())
            {
                // 玩家携带厨房物品
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // 玩家不携带厨房物品
            }
        }
        else
        {
            // 桌台上有厨房物品
            if (player.HasKitchenObject())
            {
                // 玩家携带厨房物品

            }
            else
            {
                // 玩家不携带厨房物品
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // 菜板上有厨房物品

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO,this);
        }
    }
}
