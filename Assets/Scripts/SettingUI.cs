using UnityEngine;

public class SettingUI : MonoBehaviour
{
    // Ẩn giao diện bảng cài đặt (Settings)
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Hiển thị giao diện bảng cài đặt (Settings)
    public void Show()
    {
        gameObject.SetActive(true);
    }
}

