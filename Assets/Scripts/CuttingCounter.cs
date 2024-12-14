using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // ��̨û�г�����Ʒ
            if (player.HasKitchenObject())
            {
                // ���Я��������Ʒ

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // ���Я�����и�ĳ�����Ʒ
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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

    public override void InteractAlternate(Player player)
    {

        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // �˰����г�����Ʒ ���� ���Ա��и�
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenPbjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenPbjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;

    }
}
