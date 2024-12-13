using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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


}
