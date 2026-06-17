using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class Board : MonoBehaviour
{
    public static int Size = 8;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform CellsTransform;
    [SerializeField] private GameManager gameManager;
    private Cell[,] cells;
    private int[,] dataState = new int[Size, Size];//0: empty, 1: Show, 2: Hover, 3: 1->Highlight, 4: 2->Highlight
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //background, khởi tạo các cell và ẩn đi
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
    void Start()
    {
        var blockCellWidth = (float)Size / (Block.Size * 3 + 3 + 1);

        var offset = new Vector2(0.25f + 0.5f, 0.25f + blockCellWidth * Block.Size + Blocks.distanceBoardY);

        var gameCamera = Camera.main.GetComponent<GameCamera>();
        //vùng camera nhìn thấy từ đáy của Block tới đỉnh bảng chơi, margin=0.75
        gameCamera.View(
            new Rect(
                -offset.x,
                -offset.y,
                Size + offset.x * 2.0f,
                Size + offset.y + 0.25f
            ),
            new(Size, Size)
        );
    }

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
    public int[,] GetDataState()
    {
        return dataState;
    }
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

    public void PredictFullFill()
    {
        List<int> fullFillCols = CheckFullFillCols();
        List<int> fullFillRows = CheckFullFillRows();

        foreach (int c in fullFillCols)
        {
            for (int r = 0; r < Size; r++)
            {
                cells[r, c].Highlight();
                if (dataState[r, c] == 1) dataState[r, c] = 3;
                if (dataState[r, c] == 2) dataState[r, c] = 4;
            }
        }
        foreach (int r in fullFillRows)
        {
            for (int c = 0; c < Size; c++)
            {
                cells[r, c].Highlight();
                if (dataState[r, c] == 1) dataState[r, c] = 3;
                if (dataState[r, c] == 2) dataState[r, c] = 4;
            }
        }
    }
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

    public void ClearFullFill()
    {
        List<int> fullFillCols = CheckFullFillCols();
        List<int> fullFillRows = CheckFullFillRows();
        foreach (int c in fullFillCols)
        {
            for (int r = 0; r < Size; r++)
            {
                dataState[r, c] = 0;
                cells[r, c].Hide();
                gameManager.AddScore(8);
            }
        }
        foreach (int r in fullFillRows)
        {
            for (int c = 0; c < Size; c++)
            {
                dataState[r, c] = 0;
                cells[r, c].Hide();
                gameManager.AddScore(8);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
