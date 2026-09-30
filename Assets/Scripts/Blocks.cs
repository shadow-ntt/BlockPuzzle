using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blockPrefab;
    [SerializeField] private Board board;
    public int CountBlocksCurrently = 0;
    public float blockCellWidthScale;
    public static float distanceBoardY = 3.25f;

    // Kiểm tra xem trò chơi đã kết thúc chưa (khi không còn khối nào đang hiển thị có thể đặt lên bàn cờ)
    public bool IsGameOver()
    {
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            if (blockPrefab[i].gameObject.activeSelf && blockPrefab[i].IsCanPlace())
            {
                return false;
            }
        }
        if (CountBlocksCurrently <= 0)
        {
            return false;
        }
        return true;
    }

    // Sinh ngẫu nhiên hình dạng mới cho tất cả các khối trong khay và kích hoạt hiển thị
    public void GenerateBlocks()
    {
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            blockPrefab[i].GenerateBlocks(Random.Range(0, Polyomios.Length()));
            blockPrefab[i].gameObject.SetActive(true);
        }
    }

    // Khởi tạo kích thước tỉ lệ, vị trí đặt khay chứa và tạo 3 khối gạch đầu tiên
    void Start()
    {
        // Kích thước toàn bộ khối -> set vị trí
        float blockWidth = (float)Board.Size / blockPrefab.Length;
        // Kích thước của block -> scale, trái phải 1 cell, mỗi block cách nhau 1 cell
        float blockCellWidthScale = (float)Board.Size / (Block.Size * blockPrefab.Length + blockPrefab.Length + 1);

        // Lấy số lượng block hiện tại
        this.CountBlocksCurrently = blockPrefab.Length;
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            blockPrefab[i].gameObject.transform.localPosition = new Vector3(i * blockWidth + blockWidth / 2, -distanceBoardY, 0);
            blockPrefab[i].transform.localScale = new Vector3(blockCellWidthScale, blockCellWidthScale, blockCellWidthScale);
            blockPrefab[i].InitialCells();
            blockPrefab[i].GenerateBlocks(Random.Range(0, Polyomios.Length()));
        }
    }

    // Theo dõi mỗi frame: nếu đã dùng hết toàn bộ khối thì tự động sinh lượt khối mới
    void Update()
    {
        if (this.CountBlocksCurrently <= 0)
        {
            GenerateBlocks();
            this.CountBlocksCurrently = blockPrefab.Length;
        }
    }
}

