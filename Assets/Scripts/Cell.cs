using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    public Sprite normalSprite;

    [SerializeField]
    public Sprite highlightSprite;
    private SpriteRenderer spriteRenderer;

    // Khởi tạo SpriteRenderer và thiết lập trạng thái hiển thị ban đầu
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = highlightSprite;
        gameObject.SetActive(true);
    }

    // Ẩn ô trên bàn cờ (tắt GameObject)
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Chuyển sprite ô sang dạng nổi bật (highlight) để hiển thị các dòng sắp hoàn thành
    public void Highlight()
    {
        spriteRenderer.sprite = highlightSprite;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        gameObject.SetActive(true);
    }

    // Chuyển ô sang dạng bán trong suốt (hover) khi người chơi kéo khối gạch ướm thử vị trí
    public void Hover()
    {
        spriteRenderer.sprite = normalSprite;
        spriteRenderer.color = new Color(1, 1, 1, 0.15f);
        gameObject.SetActive(true);
    }

    // Đặt ô về trạng thái bình thường (rõ nét) khi khối gạch đã được cố định trên bàn cờ
    public void Normal()
    {
        spriteRenderer.sprite = normalSprite;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        gameObject.SetActive(true);
    }
}

