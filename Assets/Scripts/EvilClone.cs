using UnityEngine;

namespace DefaultNamespace
{
    public class EvilClone : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;       
        [SerializeField] private float jumpForce = 10f;      
        [SerializeField] private Transform groundCheck;       
        [SerializeField] private float groundCheckRadius = 0.2f;  
        [SerializeField] private LayerMask groundLayer;
    }
}