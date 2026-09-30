using UnityEngine;

public class Polyomios : MonoBehaviour
{
    public static readonly int[][,] polyomios = new int[][,]
    {
        // ================= 1. DOTS & LINES (ĐIỂM & ĐƯỜNG THẲNG) =================
        // Dot 1x1
        new int[,]
        {
            { 1 },
        },
        // Line 2 (Ngang / Dọc)
        new int[,]
        {
            { 1, 1 },
        },
        new int[,]
        {
            { 1 },
            { 1 },
        },
        // Line 3 (Ngang / Dọc)
        new int[,]
        {
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 1 },
            { 1 },
            { 1 },
        },
        // Line 4 (Ngang / Dọc)
        new int[,]
        {
            { 1, 1, 1, 1 },
        },
        // Line 4 Dọc
        new int[,]
        {
            { 1 },
            { 1 },
            { 1 },
            { 1 },
        },
        // Line 5 (Ngang / Dọc)
        new int[,]
        {
            { 1, 1, 1, 1, 1 },
        },
        new int[,]
        {
            { 1 },
            { 1 },
            { 1 },
            { 1 },
            { 1 },
        },
        // ================= 2. SQUARES (KHỐI VUÔNG & KHỐI ĐẶC) =================
        // Square 2x2
        new int[,]
        {
            { 1, 1 },
            { 1, 1 },
        },
        // Square 3x3
        new int[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 },
        },
        // Big Rectangle 2x3 & 3x2
        new int[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 1, 1 },
            { 1, 1 },
            { 1, 1 },
        },
        // ================= 3. KHỐI CỠ LỚN 4x4 (BIG BLOCKS) =================
        // Square 4x4 (Siêu to - rất ít xuất hiện nhưng cực kỳ thử thách)
        new int[,]
        {
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
            { 1, 1, 1, 1 },
        },
        // Super L 4x4 (Góc vuông siêu to)
        new int[,]
        {
            { 1, 1, 1, 1 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
        },
        new int[,]
        {
            { 1, 1, 1, 1 },
            { 0, 0, 0, 1 },
            { 0, 0, 0, 1 },
            { 0, 0, 0, 1 },
        },
        new int[,]
        {
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 1, 1, 1 },
        },
        new int[,]
        {
            { 0, 0, 0, 1 },
            { 0, 0, 0, 1 },
            { 0, 0, 0, 1 },
            { 1, 1, 1, 1 },
        },
        // ================= 4. SMALL L-SHAPES (GÓC NHỎ 2x2) =================
        new int[,]
        {
            { 1, 1 },
            { 1, 0 },
        },
        new int[,]
        {
            { 1, 1 },
            { 0, 1 },
        },
        new int[,]
        {
            { 1, 0 },
            { 1, 1 },
        },
        new int[,]
        {
            { 0, 1 },
            { 1, 1 },
        },
        // ================= 5. BIG L-SHAPES (GÓC VỪA 3x3) =================
        new int[,]
        {
            { 1, 1, 1 },
            { 1, 0, 0 },
            { 1, 0, 0 },
        },
        new int[,]
        {
            { 1, 1, 1 },
            { 0, 0, 1 },
            { 0, 0, 1 },
        },
        new int[,]
        {
            { 1, 0, 0 },
            { 1, 0, 0 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 0, 0, 1 },
            { 0, 0, 1 },
            { 1, 1, 1 },
        },
        // ================= 6. T-SHAPES (KHỐI CHỮ T 2x3 & 3x2) =================
        new int[,]
        {
            { 1, 1, 1 },
            { 0, 1, 0 },
        },
        new int[,]
        {
            { 0, 1, 0 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 },
        },
        new int[,]
        {
            { 0, 1 },
            { 1, 1 },
            { 0, 1 },
        },
        // Big T 3x3 (Khối T cỡ lớn)
        new int[,]
        {
            { 1, 1, 1 },
            { 0, 1, 0 },
            { 0, 1, 0 },
        },
        new int[,]
        {
            { 0, 1, 0 },
            { 0, 1, 0 },
            { 1, 1, 1 },
        },
        // ================= 7. TETRIS L / J SHAPES (2x3 & 3x2) =================
        new int[,]
        {
            { 1, 1, 1 },
            { 1, 0, 0 },
        },
        new int[,]
        {
            { 1, 1, 1 },
            { 0, 0, 1 },
        },
        new int[,]
        {
            { 1, 0, 0 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 0, 0, 1 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 },
        },
        new int[,]
        {
            { 1, 1 },
            { 0, 1 },
            { 0, 1 },
        },
        new int[,]
        {
            { 1, 0 },
            { 1, 0 },
            { 1, 1 },
        },
        new int[,]
        {
            { 0, 1 },
            { 0, 1 },
            { 1, 1 },
        },
        // ================= 8. Z & S SHAPES (2x3 & 3x2) =================
        new int[,]
        {
            { 1, 1, 0 },
            { 0, 1, 1 },
        },
        new int[,]
        {
            { 0, 1, 1 },
            { 1, 1, 0 },
        },
        new int[,]
        {
            { 0, 1 },
            { 1, 1 },
            { 1, 0 },
        },
        new int[,]
        {
            { 1, 0 },
            { 1, 1 },
            { 0, 1 },
        },
        // ================= 9. SPECIAL SHAPES (U-SHAPE & C-SHAPE) =================
        // U-Shape 2x3
        new int[,]
        {
            { 1, 0, 1 },
            { 1, 1, 1 },
        },
        new int[,]
        {
            { 1, 1, 1 },
            { 1, 0, 1 },
        },
        // C-Shape / U-Shape Dọc (3x2)
        new int[,]
        {
            { 1, 1 },
            { 1, 0 },
            { 1, 1 },
        },
        new int[,]
        {
            { 1, 1 },
            { 0, 1 },
            { 1, 1 },
        },
    };

    // Lấy ma trận 2D mô tả hình dạng khối gạch Polyomio gốc theo chỉ số index
    public static int[,] GetPolyomios(int index)
    {
        return polyomios[index];
    }

    // Lấy ma trận khối gạch và đảo ngược chiều dọc theo trục Y để khớp với hệ toạ độ 2D của Unity
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

    // Trả về tổng số lượng mẫu khối gạch Polyomio có trong danh sách
    public static int Length()
    {
        return polyomios.Length;
    }
}
