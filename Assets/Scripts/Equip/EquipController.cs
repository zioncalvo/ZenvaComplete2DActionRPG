using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EquipController : MonoBehaviour
{

    private EquipItem curEquipItem;
    private GameObject curEquipObject;

    private bool useInput;


    [Header("Components")]
    [SerializeField] private Transform equipObjectOrigin;
    [SerializeField] private MouseUtilities mouseUtilities;



    void Update()
    {
        Vector2 mouseDir = mouseUtilities.GetMouseDirection(transform.position);

        transform.up = mouseDir;

        if(HasItemEquiped())
        {
            //this line of code is used to see if the mouse is over a UI element such as a button,image, etc. (IsPointerOverGameObject())
            if(useInput && EventSystem.current.IsPointerOverGameObject() == false)
            {
                curEquipItem.OnUse();
            }
        }
    }

    public void Equip(ItemData item)
    {
        if(HasItemEquiped())
            Unequip();

        curEquipObject = Instantiate(item.EquipPrefab, equipObjectOrigin);
        curEquipItem = curEquipObject.GetComponent<EquipItem>();
    }

    public void Unequip()
    {
        if(curEquipItem != null)
            Destroy(curEquipObject);
        
        curEquipItem = null;
    }

    public bool HasItemEquiped()
    {
        return curEquipItem != null;
    }

    public void OnUseInput(InputAction.CallbackContext context)
    {

        //this code gets when the key is pressed(or being held down) and released
        if(context.phase == InputActionPhase.Performed)
            useInput = true;
        if(context.phase == InputActionPhase.Canceled)
            useInput = false;
    }

}
