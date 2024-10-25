using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action OnSpacePressed;
    public event Action OnCPressed;
    public event Action OnRPressed;
    public event Action OnSpeedUpPressed;
    public event Action<float> OnHorizontalKeyUp;
    public event Action<float> HorizontalInput;

    private const string HorizontalAxisName = "Horizontal";

    void Update()
    {
        var moveInput = Input.GetAxisRaw(HorizontalAxisName);

        if (moveInput is > 0 or < 0)
        {
            HorizontalInput?.Invoke(moveInput);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }

        // For color change
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnCPressed?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRPressed?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnSpeedUpPressed?.Invoke();
        }
        
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnHorizontalKeyUp?.Invoke(1f);
        }
        
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnHorizontalKeyUp?.Invoke(-1f);
        }
    }
}

