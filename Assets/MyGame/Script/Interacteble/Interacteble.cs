using UnityEngine;

/// <summary>
/// ������� abstract ����� ��� ���������� ������������� ��������� � ��������
/// </summary>
public abstract class Interacteble : MonoBehaviour, IInteracteble
{
    [Header("Settings")]
    public bool UseEvents;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }
    [SerializeField] private string _description;

    /// <summary>
    /// ������� ��������������� ������� ������������
    /// </summary>
    public virtual void BaseInteract()
    {
        if (UseEvents)
        {
            GetComponent<InteractebleEvents>().OnInteract?.Invoke();
        }
        Interact();
    }

    /// <summary>
    /// ������������ ���������������, ������� ������������ ��������� ������ �������������� ��� ���� ����������
    /// </summary>
    protected virtual void Interact()
    {
    }
}

/// <summary>
/// ���� interface ������� ������������� ���������� ��� ������������� ��������� � ��������
/// </summary>
public interface IInteracteble
{
    public abstract void BaseInteract();
    public string Description { get; set; }
}