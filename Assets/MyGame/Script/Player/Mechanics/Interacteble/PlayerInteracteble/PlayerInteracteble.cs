using UnityEngine;

[RequireComponent(typeof(PlayerUi))]
public class PlayerInteracteble : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _distance;
    private Camera _camera;
    private PlayerUi _playerUi;
    private bool _isInteract = false;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Initialization(Camera camera)
    {
        _camera = camera;
        _playerUi = GetComponent<PlayerUi>();
        MainSystem.OnUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        _playerUi.UpdateText(string.Empty);
        Ray ray = new(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _distance, _mask))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteracteble interacteble))
            {
                _playerUi.UpdateText(interacteble.Description);
                if (_isInteract)
                {
                    _isInteract = !_isInteract;
                    interacteble.BaseInteract();
                }
            }
        }
    }

    public void OnInteracteble(bool isInteract)
    {
        _isInteract = isInteract;
    }
}