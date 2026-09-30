using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public sealed class GameCamera : MonoBehaviour
    {
        [SerializeField] private Transform backgroundTransform;
        [SerializeField] private RectTransform scoresRectTransform;

        private Camera mainCamera;

        private Rect viewFrameRect;
        private Rect viewRect;

        private Vector2Int boardSize;

        // Khởi tạo các tham chiếu camera, background và điểm số UI
        private void Awake()
        {
            Assert.IsNotNull(backgroundTransform);
            Assert.IsNotNull(scoresRectTransform);

            mainCamera = gameObject.GetComponent<Camera>();
        }

        // Nhận thông tin khung nhìn UI từ GameViewFrame và cập nhật lại camera
        public void ViewFrame(Rect rect)
        {
            this.viewFrameRect = rect;
            Apply();
        }

        // Thiết lập vùng giới hạn bàn cờ cùng kích thước bàn cờ, sau đó áp dụng cấu hình
        public void View(Rect rect, Vector2Int boardSize)
        {
            this.viewRect = rect;
            this.boardSize = boardSize;
            Apply();
        }

        // Tính toán kích thước orthographicSize cho camera, co giãn background và căn vị trí bảng điểm
        public void Apply()
        {
            if (mainCamera == null)
                return;

            var size = viewRect.size / viewFrameRect.size;
            var height = Mathf.Max(size.x / mainCamera.aspect, size.y);
            var orthographicSize = height * 0.5f;
            mainCamera.orthographicSize = orthographicSize;

            // Xử lý background theo camera
            backgroundTransform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0.0f
            );
            var scaleFactor =
                Mathf.Max(
                    height * mainCamera.aspect / 1080.0f, // Chiều rộng camera, xử lý khi xoay ngang màn hình
                    height / 1920.0f
                ) * 100.0f;

            backgroundTransform.localScale =
                new Vector3(scaleFactor, scaleFactor, scaleFactor);

            // Vị trí của điểm đạt được: chuyển đổi toạ độ đỉnh bàn cờ sang Screen Point rồi sang Local Point trong Canvas
            var screenPoint =
                mainCamera.WorldToScreenPoint(
                    new Vector3(
                        boardSize.x * 0.5f,
                        boardSize.y + 0.75f,
                        0.0f
                    )
                );

            if (
                float.IsNaN(screenPoint.x) == false &&
                float.IsNaN(screenPoint.y) == false &&
                float.IsNaN(screenPoint.z) == false &&
                float.IsInfinity(screenPoint.x) == false &&
                float.IsInfinity(screenPoint.y) == false &&
                float.IsInfinity(screenPoint.z) == false
            )
            {
                Vector2 localPoint;

                if (
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        scoresRectTransform.parent.GetComponent<RectTransform>(),
                        screenPoint,
                        null,
                        out localPoint
                    )
                )
                {
                    scoresRectTransform.localPosition = localPoint;
                }
            }
        }
    }
}