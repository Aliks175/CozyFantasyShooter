using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private ChooseWeaponView _chooseWeaponView;
    [SerializeField] private ShootingWeapon _shootingWeapon;
    [SerializeField] private ControlViewMark _controlViewMark;
    //[SerializeField] private WeaponThrow _weaponThrow;
    private IPlayerDatable _playerData;
    
    private void OnDisable()
    {
        _playerData.ChooseWeapon.OnChangeWeapon -= _chooseWeaponView.ChangeWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon -= _shootingWeapon.SetWeapon;
        _chooseWeaponView.OnWeaponEquip -= _shootingWeapon.SetWeaponEffect;
        _playerData.ChooseWeapon.OnChangeWeapon -= Contect => _shootingWeapon.StopFire();
        //_playerData.ChooseWeapon.OnSetThrowWeapon -= _weaponThrow.SetWeapon;
    }

    public void Initialization(IPlayerDatable playerDatable,Camera camera)
    {

        _playerData = playerDatable;
        _chooseWeaponView.Initialization();
        _shootingWeapon.Initialization(camera, _controlViewMark);
        _controlViewMark.Initialization();
        //_weaponThrow.Initialization();
        SetUp();
    }

    private void SetUp()
    {
        _playerData.ChooseWeapon.OnChangeWeapon += _chooseWeaponView.ChangeWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon += _shootingWeapon.SetWeapon;
        _playerData.ChooseWeapon.OnChangeWeapon += Contect => _shootingWeapon.StopFire();
        _chooseWeaponView.OnWeaponEquip += _shootingWeapon.SetWeaponEffect;
        //_playerData.ChooseWeapon.OnSetThrowWeapon += _weaponThrow.SetWeapon;
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        _shootingWeapon.Shoot(value);
    }
    
    //public void Throw()
    //{
    //    _weaponThrow.Throw();
    //}
}