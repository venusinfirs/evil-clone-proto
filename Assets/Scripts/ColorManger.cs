using DefaultNamespace;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ColorManger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [Inject] private InputHandler _inputHandler;
    private string _tag;

    private void Start()
    {
        _inputHandler.OnCPressed += SetRandomColor;
        _tag = gameObject.tag;
    }

    private void SetRandomColor()
    {
        if (_tag == GameplayValues.EvilCloneTag)
        {
            return;
        }

        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        var randomColor = new Color(r, g, b);
        SpriteRenderer.material.color = randomColor;
    }
    
    private void OnDestroy()
    {
        _inputHandler.OnCPressed -= SetRandomColor;
    }
}
