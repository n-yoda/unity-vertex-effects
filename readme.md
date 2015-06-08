# Unity UIの文字に綺麗な輪郭線と影を付ける
### Text Outline and Shadow Effects for Unity UI (uGUI)

## Keywords
Unity uGUI 文字 Text 縁取り 輪郭 Outline 影 Shadow

## 概要
* UI.Outlineの改良版です。Qiitaのアドベントカレンダーの記事にするつもりでしたが、間に合わなかったのでソースだけ公開します。括弧内は通常の描画に対して頂点数が何倍に増えたかを表します。
* 以下の画像は
  1. Outlineは、uGUIに含まれているもので、太いOutlineを綺麗に描画出来ません。
  2. Outline8は、Outlineを改良したもので、上下左右の他に、斜め方向にもずらして描画しています。NGUIのUILabelをこのように拡張する人が多いためか、NGUIには最近導入されました。
  3. BoxOutlineは、格子状にずらしながら描画することでどんな太さの境界線も綺麗に描画出来ます、が頂点数がとても増えます。
  4. CircleOutlineは、半径1の円上にN個、半径2の円上にN+K個、半径3の円上にN+2K個…、のようにずらして描画します。「線の太さ」を正しく表現することが出来ます。
* BoxOutlineに関しては、シーン読み込み時にShaderとRenderTextureで効率よく描画しておく方法が考えられます。縦方向にNピクセル膨張したあと横方向にNピクセル膨張させると、結果的に上のBoxOutlineと同じ効果が得られるためです。



![Outline](Assets/VertexEffectsExamples/ScreenShots/Outline.png)

* Alphaを小さくするとドロップシャドウのような効果も得られます。

![DropShadow](Assets/VertexEffectsExamples/ScreenShots/DropShadow.png)

## ModifiedShadow.cs
Unity UI の Shadow.cs の Capacity の計算がおかしい気がするので、Shadow の代わりにこれを継承しています。

