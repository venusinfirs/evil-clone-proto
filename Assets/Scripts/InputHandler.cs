using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action OnSpacePressed;
    public event Action OnCPressed;
    public event Action OnRPressed;
    public event Action OnSpeedUpPressed;
    public event Action<float> HorizontalInput;

    private const string HorizontalAxisName = "Horizontal";

    void Update()
    {
        var moveInput = Input.GetAxis(HorizontalAxisName);

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
    }
}

