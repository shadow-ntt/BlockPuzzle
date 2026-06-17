using UnityEngine;

public class Polyomios : MonoBehaviour
{
    private static int[][,] polyomios = new int[][,]
    {
        new int[,] {
        {1}},
        new int[,] {
        {1,1}},
        new int[,] {
        {1,1,1},
        {0,1,0},},
        new int[,] {
        {1,1},
        {1,0},},
        new int[,] {
        {1,1,1},},
        new int[,] {
        {1,1,1,1},},
        new int[,] {
        {1,1},
        {1,1},},
        new int[,] {
        {1,1,1},},
        new int[,] {
        {1,1,1,1},
        {0,0,0,1},
        {0,0,0,1},
        {0,0,0,1}},
        new int[,] {
        {1,1,1},
        {0,1,0},
        {0,1,0}},
       new int[,] {
        {1,1,1},
        {1,0,0},
        {1,0,0}},
        new int[,] {
        {1,1,1,1},
        {1,1,1,1},},
         new int[,] {
          {1,1,1,1,1},
          {1,1,1,1,1},
          {0,0,0,1,1},
          {0,0,0,1,1},
          {0,0,0,1,1}},
    new int[,] {
        {1,1,1,1,1},
        {1,1,1,1,1},},
    };

    public static int[,] GetPolyomios(int index)
    {
        return polyomios[index];
    }
    public static int[,] GetPolyomioReverse(int index)
    {
        int rows = polyomios[index].GetLength(0);
        int cols = polyomios[index].GetLength(1);
        int[,] reversePolyomio = new int[rows, cols];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                reversePolyomio[r, c] = polyomios[index][rows - 1 - r, c];
            }
        }
        return reversePolyomio;
    }
    public static int Length()
    {
        return polyomios.Length;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
