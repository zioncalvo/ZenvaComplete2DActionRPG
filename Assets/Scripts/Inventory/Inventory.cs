using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemData[] starterItems;
    [SerializeField] private int inventorySize;
    private ItemSlot[] itemSlots;

    public InventoryUI UI;

    public static Inventory Instance;

    void Awake()
    {
        Debug.Log("Fork");
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        itemSlots = new ItemSlot[inventorySize];

        for(int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = new ItemSlot();
        }

        for(int i = 0; i < starterItems.Length; i++)
        {
            AddItem(starterItems[i]);
        }
    }

    public void AddItem(ItemData item)
    {
        ItemSlot slot = FindAvailableItemSlot(item);
        Debug.Log("Current Slot Quantity: " + slot.Quantity);
        if(slot != null)
        {
            slot.Quantity++;
            UI.UpdateUI(itemSlots);
            return;
        }

        slot = GetEmptySlot();

        if(slot != null)
        {
            slot.Item = item;
            slot.Quantity = 1;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return;
        }

        UI.UpdateUI(itemSlots);

    }

    public void RemoveItem(ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item)
            {
                RemoveItem(itemSlots[i]);
                return;
            }
        }
    }

    public void RemoveItem(ItemSlot slot)
    {
        if(slot == null)
        {
            Debug.LogError("Cannot remove item from inventory");
            return;
        }

        slot.Quantity--;

        if(slot.Quantity <= 0)
        {
            slot.Item = null;
            slot.Quantity = 0;
        }

        UI.UpdateUI(itemSlots);

    }

    //this function is finding if there is an already existing slot that has an item that is like "stackable" so that it doesnt take up another slot
    ItemSlot FindAvailableItemSlot(ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item && itemSlots[i].Quantity < item.MaxStackSize)
                return itemSlots[i];
        }

        return null;

    }

    ItemSlot GetEmptySlot()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
                return itemSlots[i];
        }

        return null;

    }

    public void UseItem(ItemSlot slot)
    {
        if(slot.Item is MeleeWeaponItemData || slot.Item is RangedWeaponItemData)
        {
            Player.Instance.EquipCtrl.Equip(slot.Item);
        }
        else if(slot.Item is FoodItemData)
        {
            FoodItemData food = slot.Item as FoodItemData;
            Player.Instance.Heal(food.HealthToGive);

            RemoveItem(slot);
        }
    }

    public bool HasItem(ItemData item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item && itemSlots[i].Quantity > 0)
                return true;
        }

        return false;

    }
}
