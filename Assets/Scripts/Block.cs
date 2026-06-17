using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Block : MonoBehaviour
{
    public static int Size = 5;
    public Cell[,] cells = new Cell[Size, Size];
    public int index;
    public int[,] polyomio;
    public int rowsPolyomio;
    public int colsPolyomio;
    public SortingGroup sortingGroup;
    public Audio Audio;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Board board;
    [SerializeField] private Blocks blocks;
    public Vector3 positionOrigin;
    public Vector3 scaleOrigin;
    public Vector3 positionMouseDown;
    public Vector3 prePositionOnBoard;
    public Vector3 positionOnBoard;
    public Vector2 centerPosition;

    void Awake()
    {
        sortingGroup = gameObject.GetComponent<SortingGroup>();
        Audio = FindAnyObjectByType<Audio>();
    }
    void OnMouseDown()
    {
        positionMouseDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(1, 1, 1);
        Audio.PlayClickAudio();
        transform.localPosition = positionOrigin + new Vector3(0, 4, 0);
        this.positionOnBoard = Vector3Int.RoundToInt(transform.position - (Vector3)this.centerPosition);
        if (this.positionOnBoard != prePositionOnBoard)
        {
            this.prePositionOnBoard = this.positionOnBoard;

        }
    }
    void OnMouseDrag()
    {
        Vector3 mousePositionDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 delta = mousePositionDrag - positionMouseDown;
        transform.localPosition = positionOrigin + new Vector3(0, 4, 0) + new Vector3(delta.x, delta.y, 0);
        this.positionOnBoard = Vector3Int.RoundToInt(transform.position - (Vector3)this.centerPosition);
        if (this.positionOnBoard != prePositionOnBoard)
        {
            this.prePositionOnBoard = this.positionOnBoard;
            board.UnHighlight();
            board.ClearHover();
            if (IsValidOnBoardPoint((int)this.positionOnBoard.x, (int)this.positionOnBoard.y))
            {
                board.SetHover(this.polyomio, positionOnBoard);
                board.PredictFullFill();
            }
            board.Hover();
            sortingGroup.sortingOrder = 21;
        }

    }
    void OnMouseUp()
    {
        transform.localScale = scaleOrigin;
        transform.localPosition = positionOrigin;
        board.ClearHover();
        if (IsValidOnBoardPoint((int)this.positionOnBoard.x, (int)this.positionOnBoard.y))
        {
            board.SetPlace(this.polyomio, this.positionOnBoard);
            board.UnHighlight();
            board.ClearFullFill();
            blocks.CountBlocksCurrently--;
            gameObject.SetActive(false);
            Audio.PlayPlaceAudio();
        }
        sortingGroup.sortingOrder = 20;
    }
    public void InitialCells()
    {
        positionOrigin = transform.localPosition;
        scaleOrigin = transform.localScale;

        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                cells[r, c] = Instantiate(cellPrefab, transform);

            }
        }
    }
    public void GenerateBlocks(int index)
    {
        HideAllCells();
        this.index = index;
        this.polyomio = Polyomios.GetPolyomioReverse(index);
        this.rowsPolyomio = polyomio.GetLength(0);
        this.colsPolyomio = polyomio.GetLength(1);
        centerPosition = new Vector2((float)colsPolyomio / 2, (float)rowsPolyomio / 2);
        for (int r = 0; r < rowsPolyomio; r++)
        {
            for (int c = 0; c < colsPolyomio; ++c)
            {
                if (polyomio[r, c] == 1)
                {
                    cells[r, c].Normal();
                }
                else
                {
                    cells[r, c].Hide();
                }
                cells[r, c].transform.localPosition = new Vector3(c - centerPosition.x + 0.5f, r - centerPosition.y + 0.5f, 0);
            }
        }
    }
    public void HideAllCells()
    {
        for (int r = 0; r < Size; r++)
        {
            for (int c = 0; c < Size; c++)
            {
                cells[r, c].Hide();
            }
        }
    }

    public bool IsValidOnBoardPoint(int positionOnBoardX, int positionOnBoardY)
    {
        for (int r = 0; r < this.rowsPolyomio; r++)
        {
            for (int c = 0; c < this.colsPolyomio; ++c)
            {
                if (this.polyomio[r, c] == 1)
                {
                    int boardX = positionOnBoardX + c;
                    int boardY = positionOnBoardY + r;
                    if (boardX < 0 || boardX >= Board.Size || boardY < 0 || boardY >= Board.Size)
                    {
                        return false;
                    }
                    else if (board.GetDataState()[boardX, boardY] == 1)
                    {
                        return false;
                    }

                }
            }
        }
        return true;
    }

    public bool IsCanPlace()
    {
        for (int r = 0; r < Board.Size; r++)
        {
            for (int c = 0; c < Board.Size; c++)
            {
                if (IsValidOnBoardPoint(r, c))
                {
                    return true;
                }
            }
        }
        return false;
    }

}
