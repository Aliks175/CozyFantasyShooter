using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponThrow : MonoBehaviour
{
    [SerializeField] private float _forceThrow;
    [SerializeField] private float _directionY = 15;
    [SerializeField] private Transform _firePoint;
    private PlayerInventory _playerInventory;
    private Camera _camera;
    private Weapon _equipWeapon;
    private List<Weapon> _throwWeaponsSlot;

    public event Action<Weapon> OnChangeWeapon;

    public void Initialization(Camera camera, PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
        _camera = camera;
        _throwWeaponsSlot = new List<Weapon>();
    }

    public void Throw()
    {
        Debug.Log("Check Throw");
        SlotItem slotItem = CheckItem();
        if (slotItem != null)
        {
            Debug.Log("Fire");
            Rigidbody rb = TestFire();
            Vector3 tempDirection = Quaternion.AngleAxis(-_directionY, _camera.transform.right) * _camera.transform.forward;
            rb.AddForce(tempDirection * _forceThrow, ForceMode.Impulse);
            slotItem.ChangeCountItem();
            CheckLostWeapon();
        }

    }

    public Weapon GetEquipWeapon()
    {
        if (_equipWeapon != null)
        {

            Debug.Log($"_equipWeapon - {_equipWeapon.TypeWeapon}");
            return _equipWeapon;
        }
        return null;
    }

    private SlotItem CheckItem()
    {
        SlotItem slotItem = null;
        if (_equipWeapon == null)
        {
            Debug.Log("Granade Not Found");
            return slotItem;
        }

        if (_equipWeapon.CountItem <= 0)
        {
            return slotItem;
        }

        slotItem = _playerInventory.FindSlotItem(_equipWeapon.Id);

        if (slotItem == null) return null;

        if (!slotItem.CheckChangeCountItem(-1))
        {
            slotItem = null;
        }

        return slotItem;
    }

    private void CheckLostWeapon()
    {
        if (_equipWeapon != null)
        {
            if (_equipWeapon.CountItem <= 0)
            {
                if (_throwWeaponsSlot.Contains(_equipWeapon))
                {
                    _throwWeaponsSlot.Remove(_equipWeapon);
                    _equipWeapon = null;
                }
            }
        }
        ChooseWeapon();
    }

    private void ChooseWeapon()
    {
        if (_equipWeapon != null) return;

        foreach (var item in _throwWeaponsSlot)
        {
            if (item.CountItem > 0)
            {
                _equipWeapon = item;
                OnChangeWeapon?.Invoke(_equipWeapon);
                return;
            }
        }
    }

    private Rigidbody TestFire()
    {
        GameObject tempWeapon = Instantiate(_equipWeapon.Model, _firePoint.position, Quaternion.identity);

        return tempWeapon.GetComponent<Rigidbody>();
    }



    public void SetWeapon(Weapon weapon)
    {
        if (weapon.TypeWeapon == TypeWeapon.Grenade)
        {
            Debug.Log($"SetWeapon ===< {weapon.TypeWeapon}");
            _throwWeaponsSlot.Add(weapon);

            ChooseWeapon();
        }
    }
}