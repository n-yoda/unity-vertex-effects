# unity-vertex-effects

UI.Outlineの改良版です。Qiitaのアドベントカレンダーの記事にするつもりでしたが、間に合わなかったのでソースだけ公開します。括弧内は通常の描画に対して頂点数が何倍に増えたかを表します。

![Outline](Assets/VertexEffectsExamples/ScreenShots/Outline.png)

Alphaを小さくするとドロップシャドウのような効果も得られます。

![DropShadow](Assets/VertexEffectsExamples/ScreenShots/DropShadow.png)

BoxOutlineに関しては、シーン読み込み時にShaderとRenderTextureで効率よく描画しておく方法が考えられます。縦方向にNピクセル膨張したあと横方向にNピクセル膨張させると、結果的に上のBoxOutlineと同じ効果が得られるためです。
