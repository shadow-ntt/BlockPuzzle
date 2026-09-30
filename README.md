# Block Puzzle - Unity 2D Game

Dự án game giải đố xếp hình khối 2D (Block Puzzle) được phát triển bằng **Unity 6 (URP)**, lấy cảm hứng từ các tựa game nổi tiếng như *1010!*, *Blockudoku* và *Woodoku*.

---

## 🎮 Giới thiệu & Lối chơi

Người chơi sẽ kéo các khối gạch (Polyomios) từ khay chứa ở phía dưới và đặt vào bảng cờ kích thước **8x8**. Mục tiêu là lấp đầy các hàng ngang hoặc cột dọc để xóa chúng, giải phóng không gian trên bàn cờ và ghi điểm càng cao càng tốt. Trò chơi kết thúc khi không còn khối nào trong khay có thể đặt vừa vào bàn cờ.

---

## ✨ Tính năng nổi bật

### 1. Bảng cờ & Cơ chế kéo thả mượt mà
- Bảng cờ lưới **8x8** được tối ưu hóa hiển thị.
- Tương tác kéo thả chuột/cảm ứng mượt mà: nhấc khối lên phía trên điểm chạm để ngón tay không che tầm nhìn, tự động bắt điểm lưới (grid snap).
- **Xem trước thông minh (Preview & Prediction)**:
  - Khi kéo khối qua bàn cờ, các ô tương ứng sẽ hiển thị trạng thái mờ (*Hover*).
  - Tự động nhận diện và làm nổi bật (*Highlight*) các hàng hoặc cột sẽ được lấp đầy nếu thả khối tại vị trí đó.

### 2. Thư viện khối Polyomios phong phú
- Định nghĩa đa dạng các hình khối từ cơ bản đến nâng cao:
  - **Điểm & Đường thẳng**: Chấm 1x1, thanh thẳng 2, 3, 4, 5 ô (ngang & dọc).
  - **Khối vuông & chữ nhật**: Vuông 2x2, 3x3, chữ nhật 2x3, 3x2.
  - **Khối kích thước lớn**: Vuông 4x4, góc vuông siêu to (Super L 4x4).
  - **Khối góc & chữ L**: L nhỏ 2x2, L vừa 3x3, Tetris L và J.
  - **Khối chữ T**: T nhỏ 2x3, 3x2 và T lớn 3x3.
  - **Khối chữ Z / S**: Khối ziczac các hướng.
  - **Khối chữ U / C**: Khối móc câu đặc biệt.

### 3. Hệ thống tính điểm cải tiến (Scoring System)
Hệ thống tính điểm được thiết kế tạo cảm giác thỏa mãn và khuyến khích người chơi tính toán các combo:
- **Điểm đặt khối (Placement Score)**: Người chơi nhận **1 điểm** cho mỗi ô vuông của khối khi đặt thành công xuống bàn cờ (ví dụ khối 3 ô = 3 điểm, khối 9 ô = 9 điểm).
- **Điểm xóa dòng (Line Clear Score)**: Nhận **100 điểm** cho mỗi hàng hoặc cột được lấp đầy hoàn toàn.
- **Thưởng Combo đa dòng (Multi-line Bonus)**: Khi lấp đầy và xóa nhiều hàng/cột trong cùng một lượt thả khối, người chơi sẽ nhận thưởng cấp số cộng theo công thức:
  $$\text{Điểm Combo} = \frac{\text{Số dòng} \times (\text{Số dòng} + 1)}{2} \times 100$$
  - **1 dòng**: $100$ điểm
  - **2 dòng**: $300$ điểm *(Thưởng thêm 100 điểm combo)*
  - **3 dòng**: $600$ điểm *(Thưởng thêm 300 điểm combo)*
  - **4 dòng**: $1000$ điểm *(Thưởng thêm 600 điểm combo)*
  - **5 dòng**: $1500$ điểm...
- **Kỷ lục điểm cao (High Score)**: Tự động so sánh và lưu trữ kỷ lục cao nhất vào thiết bị thông qua `PlayerPrefs`.

### 4. Hiệu ứng hình ảnh & Âm thanh
- **Hệ thống hạt vỡ khối (Block Break Particle System)**: Áp dụng kỹ thuật **Object Pooling** (`Queue<ParticleSystem>`) với kích thước pool định sẵn, hạn chế tối đa việc khởi tạo/hủy đối tượng trong runtime giúp game chạy mượt mà 60fps.
- **Âm thanh tương tác**:
  - Âm thanh chạm/nhấc khối (`PlayClickAudio`).
  - Âm thanh đặt khối xuống bàn cờ (`PlayPlaceAudio`).
  - Âm thanh ăn điểm phá dòng (`PlayScore`).
  - Âm thanh kết thúc trò chơi (`PlayGameOver`).
- **Giao diện Cài đặt (Settings UI) & Audio Mixer**:
  - Điều chỉnh âm lượng thông qua thanh trượt Slider.
  - Sử dụng hàm chuyển đổi toán học phi tuyến tính giữa tỉ lệ thanh trượt (0 đến 1) và đơn vị Decibels (-80dB đến +3dB) để tăng độ mượt khi nghe.
  - Tự động lưu giá trị âm lượng vào `PlayerPrefs`.

### 5. Camera & Khung nhìn co giãn linh hoạt (Responsive Viewport)
Hệ thống camera kết hợp giữa `GameCamera` và `GameViewFrame`:
- `GameViewFrame` là một RectTransform trên Canvas (Anchor Presets: Stretch, Top: 400px).
- Tỉ lệ khung nhìn:
  $$\text{rectFrameRatio} = \frac{\text{height của GameViewFrame}}{\text{height màn hình}}$$
- Chiều cao camera orthographicSize:
  $$\text{height} = \max\left(\frac{\text{size.x}}{\text{camera.aspect}}, \text{size.y}\right)$$
  $$\text{orthographicSize} = \frac{\text{height}}{2}$$
- Đảm bảo toàn bộ bàn cờ 8x8 và khay chứa 3 khối gạch luôn hiển thị trọn vẹn, căn giữa hoàn hảo trên mọi kích thước màn hình (màn hình dọc, ngang, điện thoại, tablet) mà không bị mất góc hay tràn viền.

---

## 📁 Cấu trúc Mã nguồn (`Assets/Scripts/`)

Tất cả các hàm trong toàn bộ dự án đều được chú thích chi tiết bằng comment `//`:

| File Script | Chức năng chính |
| :--- | :--- |
| **`GameManager.cs`** | Quản lý vòng đời trò chơi, tính toán điểm số, lưu kỷ lục cao nhất qua `PlayerPrefs`, kiểm tra điều kiện Game Over và tải lại màn chơi. |
| **`Board.cs`** | Quản lý ma trận bàn cờ 8x8 (`dataState`), xử lý các trạng thái ô (trống, đặt khối, hover, highlight), kiểm tra hàng/cột hoàn thành, xóa ô và kích hoạt hiệu ứng nổ. |
| **`Block.cs`** | Điều khiển tương tác chuột/cảm ứng (`OnMouseDown`, `OnMouseDrag`, `OnMouseUp`), tính toán toạ độ lưới, kiểm tra vị trí đặt hợp lệ và hiển thị xem trước. |
| **`Blocks.cs`** | Quản lý khay chứa 3 khối gạch bên dưới bàn cờ, tự động sinh 3 khối mới khi người chơi dùng hết, kiểm tra xem người chơi còn nước đi hợp lệ hay không (`IsGameOver`). |
| **`Polyomios.cs`** | Kho lưu trữ định nghĩa ma trận các mẫu hình dạng khối gạch Polyomio và hàm lật khối theo hệ toạ độ Unity. |
| **`Cell.cs`** | Quản lý từng ô vuông đơn lẻ (đổi Sprite giữa Normal/Highlight, chỉnh độ trong suốt khi Hover, hiển thị/ẩn). |
| **`BlockBreakEffect.cs`** | Quản lý Object Pool cho hệ thống hạt Particle System khi phá vỡ các ô gạch. |
| **`Audio.cs`** | Quản lý và phát các hiệu ứng âm thanh (Click, Place, Score, GameOver). |
| **`MixerGroupSlider.cs`** | Điều khiển âm lượng AudioMixer bằng Slider thông qua công thức ánh xạ Decibel phi tuyến tính. |
| **`SettingUI.cs`** | Xử lý hiển thị và ẩn bảng cài đặt (Settings popup). |
| **`GameCamera.cs`** | Tự động tính toán kích thước khung nhìn Camera (`orthographicSize`), co giãn hình nền và căn vị trí bảng điểm. |
| **`GameViewFrame.cs`** | Theo dõi sự thay đổi kích thước giao diện (`OnRectTransformDimensionsChange`) và truyền thông số khung hình sang `GameCamera`. |

---

## 🛠️ Yêu cầu Hệ thống & Hướng dẫn Chạy

### Yêu cầu:
- **Unity Version**: Unity 6 (`6000.5.7f1`) trở lên.
- **Render Pipeline**: Universal Render Pipeline (URP 2D).
- **Hệ điều hành hỗ trợ**: Windows, macOS, Android, iOS, WebGL.

### Cách chạy dự án:
1. Mở **Unity Hub** và chọn **Add** > **Add project from disk**.
2. Chọn thư mục dự án `PuzzleLast`.
3. Mở dự án với phiên bản Unity phù hợp.
4. Trong cửa sổ **Project**, mở Scene tại: `Assets/Scenes/GameScence.unity`.
5. Nhấn nút **Play** (▶) trên thanh công cụ của Unity Editor để trải nghiệm game.
