# TJAPlayer3-Develop-BSQ
TJAPlayer3-Develop-ReWriteのフォーク, .tjaファイルのシミュレーターです。

- 現在のバージョン： v0.5.0

- スキンの最新版： v0.5.0

## 予告情報 （重要）

このプログラムはまだスキンを用意しておりません。

著作権に従うためにデフォルトスキンを作る予定です。

それまでに本家風のスキンが希望なら僕のDiscordでご連絡ください：

- 申しコミ#5734 （日本語・英語・仏語可）

(デフォルトスキンの作成に手伝える人も大歓迎です。)

Discord以外で配布しておりません。

`スキンの最新版`は後でリリースをする予定です。

## Q＆A

- 道場の選曲画面に曲ごとの表示される難易度が全部鬼の☆10になっています

```
.tjaファイルの#NEXTSONG行に「,[難易度],[COURSE]」を追加してください。

例：

旧： #NEXTSONG [TITLE],[SUBTITLE],[GENRE],[WAVE],[SCOREINIT],[SCOREDIFF]

新： #NEXTSONG [TITLE],[SUBTITLE],[GENRE],[WAVE],[SCOREINIT],[SCOREDIFF],[LEVEL],[COURSE]
```

- スタートの画面を通過できません

```
Pキーを長押ししてください。
```

- バグを発見しました、どうすればいいですか？

```
バグを発見したらGithubのIssueをご提出ください。
```

- 「太鼓タワー」のメニュに入れません

```
「太鼓タワー」のメニュはまだ実装されていません。
タワーの譜面をやる場合は「演奏ゲーム」選曲画面からお選びください。
```

## 更新記録

<details>
	<summary>v0.5.0</summary>
	
	- タワーを実装 (背景+結果画面の基盤)
	
	- タワー譜面で「TOWERTYPE」の設定を追加 （タワー譜面に複数なスキンを用いてプレイを可能にする機能）
	
	- 道場にAccuracy（精度）のEXAMを追加
	
	- box.defで「#BOXCOLOR」, 「#BOXTYPE」, 「#BGCOLOR」, 「#BGTYPE」, 「#BOXCHARA」の設定を追加
	
</details>

<details>
	<summary>v0.4.3</summary>
	
	- タワーを実装 (Gameplay)
	
</details>

<details>
	<summary>v0.4.2</summary>
	
	- 演奏選曲画面に複数のバグとクラッシュを修正
	
	- COURSE:Towerの.tjaファイルのクラッシュを修正、太鼓タワーメニュ・LIFE管理・結果画面がまだ実装されていません。

</details>

<details>
	<summary>v0.4.1</summary>
	
	- 演奏選曲画面に複数のバグとクラッシュ場面を修正
	
</details>

<details>
	<summary>v0.4.0</summary>
	
	- EXAM5,6,7の実装 (下記の映像をご覧ください)
	
	- EXAM数にギャップのあるクラッシュ場面を修正
	
	- Danに関してコードの構造を改善（コード蓄積の修正）
  
</details>

<details>
	<summary>v0.3.4.2</summary>
	
	- 道場選曲画面にプチキャラを追加
	
</details>

<details>
	<summary>v0.3.4.1</summary>
	
	- Mobアニメーション速度の変化バグを修正
	
</details>

<details>
	<summary>v0.3.4</summary>
	
	- 道場の結果を保存を可能にする機能を実装
	
	- 道場選曲画面に合格プレートを表示
	
</details>

<details>
	<summary>v0.3.3</summary>
	
	- 道場の魂ゲージの表示を修正
	
	- 道場の結果画面の基盤を実装（まだ実装中）
	
</details>

<details>
	<summary>v0.3.2</summary>
	
	- 演奏セーブの重ね書きバグを修正
	
</details>

<details>
	<summary>v0.3.1</summary>
	
	- P2にスコアランクを表示されないバグを修正
	
</details>

<details>
	<summary>v0.3.0</summary>
	
	- メニュにプチキャラを表示
	
	- Nameplate.jsonファイルにプレイヤー別々のプチキャラを選べる可能にする機能を実装
	
</details>

<details>
	<summary>v0.2.0</summary>
	
	- 様々な演奏選曲画面のバグを修正
	
	- メインメニュに様々なバグを修正、コード蓄積を修正
	
</details>

<details>
	<summary>v0.1.0</summary>
	
	- 演奏結果画面のアニメーションを実装
	
</details>

## 短期で実装する予定機能
```
☐ 道場結果画面を実装し切る
☐ コイン（ドンメダル）を貯蓄可能にする機能を実装＋ドンメダル商店
☑ タワーを実装し切る (COURSE: 5)
☐ タワー結果画面を実装し切る
☐ 複数な背景と踊り子セットを選べる機能を実装
☐ ２P結果画面を実装
☐ プログラムの性能を改善、メモリリークを修正
☐ 道場で４曲以上連続でやれる機能を実装
```
## クレジット
> * [Aioiloght/TJAPlayer3](https://github.com/aioilight/TJAPlayer3)
> * [TwoPointZero/TJAPlayer3](https://github.com/twopointzero/TJAPlayer3)(@twopointzero)
> * [KabanFriends/TJAPlayer3](https://github.com/KabanFriends/TJAPlayer3/tree/features)(@KabanFriends)
> * [Mr-Ojii/TJAPlayer3-f](https://github.com/Mr-Ojii/TJAPlayer3-f)(@Mr-Ojii)
> * [Akasoko/TJAPlayer3](https://github.com/Akasoko-Master/TJAPlayer3)(@AkasokoR)
> * [FROM/DTXMaina](https://github.com/DTXMania)(@DTXMania)
> * [Kairera0467/TJAP2fPC](https://github.com/kairera0467/TJAP2fPC)(@Kairera0467)
