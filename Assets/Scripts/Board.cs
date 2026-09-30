using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int Size = 8;

    [SerializeField]
    private Cell cellPrefab;

    [SerializeField]
    private Transform CellsTransform;

    [SerializeField]
    private GameManager gameManager;
    private Cell[,] cells;
    private int[,] dataState = new int[Size, Size]; // 0: empty, 1: Show, 2: Hover, 3: 1->Highlight, 4: 2->Highlight

    // Khởi tạo mảng các ô (cells) 8x8 trên bàn cờ và ẩn đi ban đầu
    void Awake()
    {
        // background, khởi tạo các cell và ẩn đi
        cells = new Cell[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Cell cell = Instantiate(cellPrefab, CellsTransform.transform);
                cell.transform.position = new Vector3(i + 0.5f, j + 0.5f, 0);
                cells[i, j] = cell;
                cells[i, j].Hide();
                dataState[i, j] = 0;
            }
        }
    }

    // Căn chỉnh camera và vùng hiển thị bao gồm bàn cờ và khay chứa khối gạch
    void Start()
    {
        var blockCellWidth = (float)Size / (Block.Size * 3 + 3 + 1);

        var offset = new Vector2(
            0.25f + 0.5f,
            0.25f + blockCellWidth * Block.Size + Blocks.distanceBoardY
        );

        var gameCamera = Camera.main.GetComponent<GameCamera>();
        // vùng camera nhìn thấy từ đáy của Block tới đỉnh bảng chơi, margin=0.75
        gameCamera.View(
            new Rect(-offset.x, -offset.y, Size + offset.x * 2.0f, Size + offset.y + 0.25f),
            new(Size, Size)
        );
    }

    // Hiển thị trạng thái mờ (hover) cho các ô đang được khối gạch ướm thử
    public void Hover()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (dataState[i, j] == 2)
                {
                    this.cells[i, j].Hover();
                }
            }
        }
    }

    // Xóa trạng thái hover của các ô trên bàn cờ và ẩn chúng đi
    public void ClearHover()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (dataState[i, j] == 2)
                {
                    dataState[i, j] = 0;
                    this.cells[i, j].Hide();
                }
            }
        }
    }

    // Trả về ma trận trạng thái dữ liệu của toàn bộ bàn cờ
    public int[,] GetDataState()
    {
        return dataState;
    }

    // Đánh dấu trạng thái hover (2) trên bàn cờ tương ứng với hình dạng khối đang kéo
    public void SetHover(int[,] polyomio, Vector3 positionOnBoard)
    {
        int rowsPolyomio = polyomio.GetLength(0);
        int colsPolyomio = polyomio.GetLength(1);
        for (int r = 0; r < rowsPolyomio; r++)
        {
            for (int c = 0; c < colsPolyomio; ++c)
            {
                if (polyomio[r, c] == 1)
                {
                    int boardX = (int)positionOnBoard.x + c;
                    int boardY = (int)positionOnBoard.y + r;
                    dataState[boardX, boardY] = 2;
                }
            }
        }
    }

    // Đặt cố định khối gạch lên bàn cờ và chuyển trạng thái ô sang hiển thị bình thường (1)
    public void SetPlace(int[,] polyomio, Vector3 positionOnBoard)
    {
        int rowsPolyomio = polyomio.GetLength(0);
        int colsPolyomio = polyomio.GetLength(1);
        for (int r = 0; r < rowsPolyomio; r++)
        {
            for (int c = 0; c < colsPolyomio; ++c)
            {
                if (polyomio[r, c] == 1)
                {
                    int boardX = (int)positionOnBoard.x + c;
                    int boardY = (int)positionOnBoard.y + r;
                    dataState[boardX, boardY] = 1;
                    this.cells[boardX, boardY].Normal();
                }
            }
        }
    }

    // Kiểm tra và trả về danh sách các cột đã được lấp đầy hoàn toàn
    public List<int> CheckFullFillCols()
    {
        List<int> res = new List<int>();
        for (int c = 0; c < Size; c++)
        {
            int count = 0;
            for (int r = 0; r < Size; r++)
            {
                if (dataState[r, c] == 1 || dataState[r, c] == 2)
                {
                    count++;
                }
            }
            if (count == Size)
            {
                res.Add(c);
            }
        }
        return res;
    }

    // Kiểm tra và trả về danh sách các hàng đã được lấp đầy hoàn toàn
    public List<int> CheckFullFillRows()
    {
        List<int> res = new List<int>();
        for (int r = 0; r < Size; r++)
        {
            int count = 0;
            for (int c = 0; c < Size; c++)
            {
                if (dataState[r, c] == 1 || dataState[r, c] == 2)
                {
                    count++;
                }
            }
            if (count == Size)
            {
                res.Add(r);
            }
        }
        return res;
    }

    // Đánh dấu nổi bật (highlight) các hàng/cột sẽ được lấp đầy nếu đặt khối vào vị trí hiện tại
    public void PredictFullFill()
    {
        List<int> fullFillCols = CheckFullFillCols();
        List<int> fullFillRows = CheckFullFillRows();

        foreach (int c in fullFillCols)
        {
            for (int r = 0; r < Size; r++)
            {
                cells[r, c].Highlight();
                if (dataState[r, c] == 1)
                    dataState[r, c] = 3;
                if (dataState[r, c] == 2)
                    dataState[r, c] = 4;
            }
        }
        foreach (int r in fullFillRows)
        {
            for (int c = 0; c < Size; c++)
            {
                cells[r, c].Highlight();
                if (dataState[r, c] == 1)
                    dataState[r, c] = 3;
                if (dataState[r, c] == 2)
                    dataState[r, c] = 4;
            }
        }
    }

    // Tắt trạng thái highlight của các ô, hoàn trả về trạng thái hiển thị bình thường hoặc hover
    public void UnHighlight()
    {
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                if (dataState[r, c] == 3)
                {
                    dataState[r, c] = 1;
                    cells[r, c].Normal();
                }
                if (dataState[r, c] == 4)
                {
                    dataState[r, c] = 2;
                    cells[r, c].Hover();
                }
            }
        }
    }

    // Xóa các hàng và cột đã hoàn thành, kích hoạt hiệu ứng nổ ô và tính điểm combo cho lượt chơi
    public int ClearFullFill()
    {
        List<int> fullFillCols = CheckFullFillCols();
        List<int> fullFillRows = CheckFullFillRows();
        int linesCount = fullFillCols.Count + fullFillRows.Count;
        if (linesCount == 0) return 0;

        // Đánh dấu các ô cần xóa để tránh xóa trùng lặp hoặc kích hoạt hiệu ứng 2 lần tại giao điểm hàng và cột
        bool[,] toClear = new bool[Size, Size];
        foreach (int c in fullFillCols)
        {
            for (int r = 0; r < Size; r++)
            {
                toClear[r, c] = true;
            }
        }
        foreach (int r in fullFillRows)
        {
            for (int c = 0; c < Size; c++)
            {
                toClear[r, c] = true;
            }
        }

        // Xóa ô và kích hoạt hiệu ứng vỡ hạt cho từng ô được đánh dấu
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                if (toClear[r, c])
                {
                    dataState[r, c] = 0;
                    cells[r, c].Hide();
                    if (BlockBreakEffect.Instance != null)
                    {
                        BlockBreakEffect.Instance.Play(cells[r, c].transform);
                    }
                }
            }
        }

        // Tính điểm: mỗi dòng 100 điểm, thưởng thêm combo cấp số cộng khi xóa nhiều dòng cùng lúc
        // 1 dòng: 100, 2 dòng: 300, 3 dòng: 600, 4 dòng: 1000...
        int lineScore = (linesCount * (linesCount + 1) / 2) * 100;
        if (gameManager != null)
        {
            gameManager.AddScore(lineScore);
        }

        return linesCount;
    }

    // Chuyển tiếp yêu cầu cộng điểm đặt khối tới GameManager
    public void AddPlacementScore(int cellCount)
    {
        if (gameManager != null)
        {
            gameManager.AddPlacementScore(cellCount);
        }
    }
}

