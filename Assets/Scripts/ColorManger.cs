using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ColorManger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [Inject] protected InputHandler InputHandler;
    void Start()
    {
        InputHandler.OnCPressed += SetRandomColor;
    }
    
    public void SetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        
        var randomColor = new Color(r, g, b);
        SpriteRenderer.material.color = randomColor;
    }

    private void OnDestroy()
    {
        InputHandler.OnCPressed -= SetRandomColor;
    }
}
