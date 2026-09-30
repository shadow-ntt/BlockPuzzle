using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class GameViewFrame : UIBehaviour
    {
        private RectTransform rectTransform;
        private bool dirty = false;
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = gameObject.GetComponent<RectTransform>();
                }

                return rectTransform;
            }
        }

        private GameCamera gameCamera;

        private GameCamera GameCamera
        {
            get
            {
                if (gameCamera == null)
                {
                    gameCamera = Camera.main.GetComponent<GameCamera>();
                }

                return gameCamera;
            }
        }

        // Kích hoạt khi component được bật, gửi thông số khung nhìn sang camera
        protected override void OnEnable()
        {
            base.OnEnable();
            SetDirty();
        }

        // Kiểm tra và cập nhật lại camera ở cuối mỗi frame nếu có đánh dấu thay đổi kích thước
        private void LateUpdate()
        {
            if (dirty) SetDirty();
            dirty = false;
        }

        // Lắng nghe sự kiện kích thước RectTransform thay đổi (khi xoay màn hình hoặc đổi tỉ lệ)
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            dirty = true;
        }

        // Tính toán toạ độ chuẩn hóa của khung nhìn theo màn hình và cập nhật tới GameCamera
        private void SetDirty()
        {
            if (IsActive() == false)
            {
                return;
            }

            // Lấy 4 góc của RectTransform trong không gian thế giới
            var worldCorners = new Vector3[4];
            RectTransform.GetWorldCorners(worldCorners);

            var min = new Vector2(
                worldCorners[0].x / Screen.width,
                worldCorners[0].y / Screen.height);

            var max = new Vector2(
                worldCorners[2].x / Screen.width,
                worldCorners[2].y / Screen.height);

            GameCamera.ViewFrame(
                new Rect(min, max - min)
            );
        }
    }
}