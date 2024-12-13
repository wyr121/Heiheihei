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
            // ��̨û�г�����Ʒ
            if (player.HasKitchenObject())
            {
                // ���Я��������Ʒ
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // ��Ҳ�Я��������Ʒ
            }
        }
        else
        {
            // ��̨���г�����Ʒ
            if (player.HasKitchenObject())
            {
                // ���Я��������Ʒ

            }
            else
            {
                // ��Ҳ�Я��������Ʒ
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }


}
