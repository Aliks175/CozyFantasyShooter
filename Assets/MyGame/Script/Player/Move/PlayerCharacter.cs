using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public IPlayerDatable PlayerData => _playerData;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerAim _playerAim;
    [SerializeField] private PlayerInteracteble _playerInteracteble;
    private IPlayerDatable _playerData;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerData = GetComponent<IPlayerDatable>();
    }

    public void Initialization(Camera camera)
    {
        _playerMover.Initialization(_playerRigidbody);
        _playerLook.Initialization(_playerRigidbody);
        _playerAim.Initialization(camera);
        _playerInteracteble.Initialization(camera);
        _playerData.Initialization(this, camera);
    }
}