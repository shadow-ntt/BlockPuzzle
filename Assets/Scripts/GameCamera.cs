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

        private void Awake()
        {
            Assert.IsNotNull(backgroundTransform);
            Assert.IsNotNull(scoresRectTransform);

            mainCamera = gameObject.GetComponent<Camera>();
        }

        public void ViewFrame(Rect rect)
        {
            this.viewFrameRect = rect;

            Apply();
        }

        public void View(Rect rect, Vector2Int boardSize)
        {
            this.viewRect = rect;
            this.boardSize = boardSize;

            Apply();
        }

        public void Apply()
        {
            if (mainCamera == null)
                return;

            //var center = viewFrameRect.center;
            var size = viewRect.size / viewFrameRect.size;
            var height = Mathf.Max(size.x / mainCamera.aspect, size.y);
            var orthographicSize = height * 0.5f;
            Debug.Log("size: " + size + "height: " + height + " viewFrameRect: " + viewFrameRect + " viewRect " + viewRect);
            mainCamera.orthographicSize = orthographicSize;

            // transform.position = new Vector3(
            //     viewRect.center.x,
            //     viewRect.center.y - (center.y - 0.5f) * height,
            //     transform.position.z
            // );

            //xử lý background theo camera
            backgroundTransform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                0.0f
            );
            var scaleFactor =
                Mathf.Max(
                    height * mainCamera.aspect / 1080.0f,// chiều rộng camera, xử lý khi người dùng xoay ngang màn hình
                    height / 1920.0f
                ) * 100.0f;

            backgroundTransform.localScale =
                new Vector3(scaleFactor, scaleFactor, scaleFactor);
            //vị trí của điểm đạt được
            //do canvas set position đặc biệt nên phải set theo kiểu lấy vị trí của nó trong màn hình sau đó set vị trí theo rectangle
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