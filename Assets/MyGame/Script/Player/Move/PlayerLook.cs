using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private GameObject _headSlot;
    [SerializeField] private float _ySensitivity = 30f;
    [SerializeField] private float _xSensitivity = 30f;
    private Rigidbody _playerRigidbody;
    private float _xRotation = 0;
    private bool _isPlay;

    public void Initialization(Rigidbody playerRigidbody)
    {
        _playerRigidbody = playerRigidbody;
        Cursor.lockState = CursorLockMode.Locked;
        _isPlay = true;
    }

    public void ProcessLook(Vector2 vector2)
    {
        if (!_isPlay) return;
        float mouseX = vector2.x;
        float mouseY = vector2.y;
        _xRotation -= (mouseY * Time.deltaTime) * _ySensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _headSlot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(0, (mouseX * Time.deltaTime) * _xSensitivity, 0);
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * deltaRotation);
    }

    public void ControlPlay(bool play)
    {
        _isPlay = play;
    }
}