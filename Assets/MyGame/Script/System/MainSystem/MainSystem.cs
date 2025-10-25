using System;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private WeaponSystem _weaponSystem;
    [SerializeField] private PlayerSystem _playerSystem;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private UiSystem _uiSystem;
    [Header("Other")]
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private Camera _camera;
    public static event Action OnUpdate;

    private void Awake()
    {
        _inputControl.Initialization(_weaponSystem, _inventorySystem);
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void Initialization()
    {
        _weaponSystem.Initialization(_player.PlayerData, _camera);
        _playerSystem.Initialization(_player, _camera);
        _inventorySystem.Initialization(_player.PlayerData.Inventory);
        _uiSystem.Initialization(_player.PlayerData);
    }
}