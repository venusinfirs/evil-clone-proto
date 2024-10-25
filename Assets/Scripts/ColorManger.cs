using DefaultNamespace;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ColorManger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [Inject] private InputHandler _inputHandler;
    [Inject] private ReproduceActionService _reproduceService; 
    private string _tag;

    private void Start()
    {
        _inputHandler.OnCPressed += SetRandomColor;
        _reproduceService.Stupify += SetStupifyColor;
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

    private void SetStupifyColor()
    {
        if (_tag != GameplayValues.EvilCloneTag)
        {
            return;
        }
        
        SpriteRenderer.material.color = Color.gray;
    }
    
    private void OnDestroy()
    {
        _inputHandler.OnCPressed -= SetRandomColor;
    }
}
