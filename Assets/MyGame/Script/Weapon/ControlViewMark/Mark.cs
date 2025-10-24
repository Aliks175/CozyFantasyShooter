using System;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [HideInInspector]public bool IsFree;
    [SerializeField] private MeshRenderer _meshRenderer;
    private float _time;
    private float _timer;
    public event Action<Mark> OnEnd;
    public event Action<Mark> OnDestroyMark;

    private void OnDestroy()
    {
        OnDestroyMark?.Invoke(this);
    }

    public void OnUpdate()
    {
        if (!IsFree)
        {
            if (Time.time >= _timer)
            {
                End();
            }
        }
    }

    public void Initialization(float Time)
    {
        _time = Time;
        SetUp();
    }

    public void SetMark()
    {
        _timer = _time + Time.time;
        _meshRenderer.enabled = true;
        IsFree = false;
    }

    private void SetUp()
    {
        _meshRenderer.enabled = false;
        IsFree = true;
    }

    public void End()
    {
        SetUp();
        OnEnd?.Invoke(this);
    }
}
