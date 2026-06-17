using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public Sprite normalSprite;
    [SerializeField] public Sprite highlightSprite;
    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = highlightSprite;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Highlight()
    {
        spriteRenderer.sprite = highlightSprite;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        gameObject.SetActive(true);
    }
    public void Hover()
    {
        spriteRenderer.sprite = normalSprite;
        spriteRenderer.color = new Color(1, 1, 1, 0.15f);
        gameObject.SetActive(true);
    }
    public void Normal()
    {
        spriteRenderer.sprite = normalSprite;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        gameObject.SetActive(true);
    }
}
