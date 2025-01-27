using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask _slingShotAreaMask;
    public bool IsWithinSlingshotArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Physics2D.OverlapPoint(worldPosition, _slingShotAreaMask))
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
}
