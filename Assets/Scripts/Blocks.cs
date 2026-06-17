using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField] private Block[] blockPrefab;
    [SerializeField] private Board board;
    public int CountBlocksCurrently = 0;
    public float blockCellWidthScale;
    public static float distanceBoardY = 3.25f;
    public bool IsGameOver()
    {
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            if (blockPrefab[i].IsCanPlace() && blockPrefab[i].gameObject.activeSelf)
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

    public void GenerateBlocks()
    {
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            blockPrefab[i].GenerateBlocks(Random.Range(0, Polyomios.Length()));
            blockPrefab[i].gameObject.SetActive(true);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Kích thước toàn bộ khối-> set vị trí
        float blockWidth = (float)Board.Size / blockPrefab.Length;
        //kích thước của block-> scale, trái phải 1 cell, mỗi block cách nhau 1 cell
        float blockCellWidthScale = (float)Board.Size / (Block.Size * blockPrefab.Length + blockPrefab.Length + 1);

        Debug.Log("blockCellWidthScale: " + blockCellWidthScale);
        //get số lượng block hiện tại
        this.CountBlocksCurrently = blockPrefab.Length;
        for (int i = 0; i < blockPrefab.Length; ++i)
        {
            blockPrefab[i].gameObject.transform.localPosition = new Vector3(i * blockWidth + blockWidth / 2, -distanceBoardY, 0);
            blockPrefab[i].transform.localScale = new Vector3(blockCellWidthScale, blockCellWidthScale, blockCellWidthScale);
            blockPrefab[i].InitialCells();
            blockPrefab[i].GenerateBlocks(Random.Range(0, Polyomios.Length()));
        }
    }
    void Update()
    {
        if (this.CountBlocksCurrently <= 0)
        {
            GenerateBlocks();
            this.CountBlocksCurrently = blockPrefab.Length;
        }

    }
}
