using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [Header("Player-Parameters")]
    [SerializeField] private float _speedMove = 5.0f;
    [SerializeField] private float _jumpHeight = 3.0f;
    [SerializeField] private float _turnSpeed = 3.0f;
    [Header("Physic")]
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private bool _isGrounded;
    private float _speed;
    private float _speedCoefficient;
    private Rigidbody _playerRigidbody;
    //private CharacterController _controller;
    [SerializeField] private Vector3 _groundPoint;
    [SerializeField] private float _radiusGroundPoint = 0.9f;
    [SerializeField] private LayerMask _groundLayer;

    private Vector3 _moveDirection;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Initialization(Rigidbody playerRigidbody)
    {
        _speed = _speedMove;
        _speedCoefficient = 1f;
        _playerRigidbody = playerRigidbody;
        MainSystem.OnUpdate += OnUpdate;
    }

    public void ProcessMove(Vector2 pos)
    {
        _moveDirection.x = pos.x;
        _moveDirection.z = pos.y;

        //_moveDirection = transform.TransformDirection(_moveDirection);
        //_playerVelocity.y += _gravity * Time.deltaTime;// отключим гравитацию на Rigidbody она встроена 

        _speed = _isGrounded ? _speedMove : _speedMove / 4;
        //if (!_isGrounded)
        //{
        //    _speed = _speedMove / 4;
        //}
        //else if (_isGrounded && _playerVelocity.y < 0)
        //{
        //    _playerVelocity.y = -2;
        //    _speed = _speedMove;
        //}
        //_final = _moveDirection * _speed * _speedCoefficient;// * Time.fixedDeltaTime;// + _playerVelocity;
        //Vector3 movement = CameraDirection(_moveDirection) * _speed * Time.deltaTime;
        //_playerRigidbody.MovePosition(_playerRigidbody.position + _moveDirection);
        //_controller.Move(_final * Time.deltaTime);
        MoveThePlayer();
    }

    public Vector3 CameraDirection(Vector3 movementDirection)
    {
        //Camera camera = Camera.main;
        //var cameraForward = _playerRigidbody.rotation;
        //Vector3 vector3 = Quaternion.(cameraForward);
        //var cameraRight = _playerRigidbody.transform.right;

        var cameraForward = _playerRigidbody.transform.forward;
        var cameraRight = _playerRigidbody.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }


    private void MoveThePlayer()// происходит какая то магия CameraDirection мне пока не понятен 
    {
        Vector3 movement = CameraDirection(_moveDirection) * _speed * Time.deltaTime;
        _playerRigidbody.MovePosition(_playerRigidbody.position + movement);
    }

    public void ChangeSpeedCoefficient(float speedCoefficient)
    {
        _speedCoefficient = Mathf.Abs(speedCoefficient);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerRigidbody.AddForce(_jumpHeight * Vector3.up, ForceMode.Impulse);
        }
    }

    private void OnUpdate()
    {// своя проверка касаемся ли мы земли через физик рэйкаст ресуем сферу так чтобы пол сферы выглядывало из нижней части gameobject проверяем на физ слой граунд 
     //_isGrounded = _controller.isGrounded;

        //   Collider[] colliders = Physics.OverlapSphere(transform.position + _groundPoint, _radiusGroundPoint, _groundLayer);
        //_isGrounded = colliders != null;

        _isGrounded = Physics.OverlapSphere(_playerRigidbody.position + _groundPoint, _radiusGroundPoint, _groundLayer).Length > 0;
        if (_isGrounded)
        {

        }
        else
        {
            Debug.Log("НЕТ земли");
        }
        //TurnThePlayer();
    }

   

    private void OnDrawGizmos()
    {
        //Debug.DrawLine()
        Gizmos.color = Color.yellow;
        Vector3 temp = transform.position + _groundPoint;
        Gizmos.DrawSphere(temp, _radiusGroundPoint);

    }
}