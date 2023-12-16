using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Action _onDestroyAction;

    public void OnDestroyActionAdd(Action action)
    {
        _onDestroyAction += action;
    }

    void OnDestroy()
    {   
        if (gameObject.tag != "Level")
        {
            _onDestroyAction?.Invoke();
        }
    }
}
