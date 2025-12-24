using UnityEngine;

public class TestTarget : MonoBehaviour, IHealtheble
{
    public float Health => _health;
    [SerializeField] private float _startHealth = 50f;
    [SerializeField] private GameObject _dropItem;
    [SerializeField] private Transform _dropPoint;
    private float _health;


    private void Start()
    {
        _health = _startHealth;
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Abs(damage);
        _health -= damage;
        if (_health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _health = 0;
        LootDrop();
        Destroy(gameObject,1f);
    }

    private void LootDrop()
    {
        GameObject tempDrop = Instantiate(_dropItem, _dropPoint.position, Quaternion.identity);
    }
}

public interface IHealtheble
{
    public void TakeDamage(float damage);
    public float Health {  get; }
}