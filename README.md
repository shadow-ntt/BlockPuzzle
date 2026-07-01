version-1
//
về file GameFrame và GameCamera để màn hình lúc nào cũng chứa được hết board
GameFrame là 1 canvas có Rect Tranform-> Anchor Presets->Stretch-> top: 400
-> mọi thiết bị top: 400px
rectFrameRatio =height của gameframe/screen
chiều cao mainCamera.orthographicSize= chiều cao của board/rectFraneRatio
tất nhiên là còn trường hợp của width dài hơn height nữa nên nó sẽ là: var height = Mathf.Max(size.x / mainCamera.aspect, size.y)
