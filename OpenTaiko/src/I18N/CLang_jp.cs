﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FDK;

namespace TJAPlayer3
{
    internal class CLang_jp : ILang
    {
        string ILang.GetString(int idx)
        {
            if (!dictionnary.ContainsKey(idx))
                return "[!] 辞書の指数を見つけられませんでした";

            return dictionnary[idx];
        }


        private static readonly Dictionary<int, string> dictionnary = new Dictionary<int, string>
        {
            [0] = "プレイ中やメニューの\n言語を変更。",
            [1] = "システム言語",
            [2] = "<< 戻る",
            [3] = "左側のメニューに戻ります。",
            [4] = "曲データ再読み込み",
            [5] = "曲データの一覧情報を取得し直します。",
            [6] = "プレイ人数",
            [7] = "プレイ人数切り替え：\n2にすると演奏画面が2人プレイ専用のレイアウトになり、\n2P専用譜面を読み込むようになります。",
            [8] = "完璧モード",
            [9] = "Riskyモードの設定:\n1以上の値にすると、その回数分の\n可/不可でFAILEDとなります。\n0にすると無効になり、\nDamageLevelに従ったゲージ増減と\nなります。\nStageFailedの設定と併用できます。",
            [10] = "再生速度",
            [11] = "曲の演奏速度を、速くしたり遅くした\n" +
                "りすることができます。\n" +
                "（※一部のサウンドカードでは正しく\n" +
                "　再生できない可能性があります。）\n" +
                "\n" +
                "TimeStretchがONのときに、演奏\n" +
                "速度をx0.850以下にすると、音の\n" +
                "ズレが大きくなります。",
			[12] = "AIレベル",
			[13] = "AIの制度を定める設定です。\n" +
				"０ならAIはオフされます。\n" +
				"１以上なら2PはAIとして自動でやります。\n" +
				"AUTO 2PはオンならAI使用できません。",
			[14] = "グローバルオフセット",
			[15] = "全譜面のOFFSETメタデータ\nを変化設定です。\n" +
				"-999から999msまで設ける可能です。\n" +
				"入力遅れを減るためマイナス値をご設定ください。",
			[16] = "選曲画面レイアウト",
            [17] = "選曲画面のレイアウトの変更ができます。\n" +
                "０＝＞通常の設計（上下斜）\n" +
                "１＝＞垂直\n" +
                "２＝＞下上斜\n" +
                "３＝＞右向け半丸\n" +
                "４＝＞左向け半丸",
            [18] = "演奏速度の変更方式:\n" + 
				"ONにすると、演奏速度の変更を、\n" +
				"周波数変更ではなく\n" +
				"タイムストレッチで行います。" +
				"\n" +
				"これをONにすると、サウンド処理に\n" +
				"より多くのCPU性能を使用します。\n" +
				"また、演奏速度をx0.850以下にすると、\n" +
				"チップのズレが大きくなります。",
            [19] = "画面モード設定：\nON で全画面モード、OFF でウィンド\nウモードになります。",
            [20] = "STAGE FAILED 有効：\nON にすると、ゲージがなくなった時\nに STAGE FAILED となり演奏が中断\nされます。OFF の場合は、ゲージが\nなくなっても最後まで演奏できます。",
            [21] = "子BOXをRANDOMの対象とする：\nON にすると、RANDOM SELECT 時\nに子BOXも選択対象とします。",
            [22] = "垂直帰線同期：\n画面の描画をディスプレイの垂直帰\n線中に行なう場合には ON を指定し\nます。ON にすると、ガタつきのない\n滑らかな画面描画が実現されます。",
            [23] = "AVIの使用：\n動画(AVI)を再生可能にする場合に\nON にします。AVI の再生には、それ\nなりのマシンパワーが必要とされます。",
            [24] = "BGAの使用：\n画像(BGA)を表示可能にする場合に\nON にします。BGA の再生には、それ\nなりのマシンパワーが必要とされます。",
            [25] = "プレビュー音演奏までの時間：\n曲にカーソルが合わされてからプレ\nビュー音が鳴り始めるまでの時間を\n指定します。\n0 ～ 10000 [ms] が指定可能です。",
            [26] = "プレビュー画像表示までの時間：\n曲にカーソルが合わされてからプレ\nビュー画像が表示されるまでの時間\nを指定します。\n0 ～ 10000 [ms] が指定可能です。",
            [27] = "演奏情報の表示：\n演奏中、BGA領域の下部に演奏情報\n（FPS、BPM、演奏時間など）を表示し\nます。\nまた、小節線の横に小節番号が表示\nされるようになります。",
            [28] = "背景画像の半透明割合：\n背景画像をDTXManiaのフレーム画像\nと合成する際の、背景画像の透明度\nを指定します。\n0 が完全透明で、255 が完全不透明\nとなります。",
            [29] = "BGMの再生：\nこれをOFFにすると、BGM を再生しな\nくなります。",
            [30] = "演奏記録の保存：\nON で演奏記録を ～.score.ini ファイ\nルに保存します。\n",
            [31] = "BS1770GAIN によるラウドネスメータの測量を適用します。\n利用するにはBS1770GAINが必要です。",
            [32] = "BS1770GAIN によるラウドネスメータの目標値を指定します。",
            [33] = ".tjaファイルのSONGVOLヘッダを音源の音量に適用します。設定による音量調整を使用する場合はこの設定をOFFにしてください。",
            [34] = $"効果音の音量を調節します。\n{CSound.MinimumGroupLevel} ～ {CSound.MaximumGroupLevel} % の値が指定可能です。\n",
			[35] = $"各画面で流れるボイス、コンボボイスの音量を調節します。\n{CSound.MinimumGroupLevel} ～ {CSound.MaximumGroupLevel} % の値が指定可能です。\n",
			[36] = $"ゲーム中の音源の音量を調節します。\n{CSound.MinimumGroupLevel} ～ {CSound.MaximumGroupLevel} % の値が指定可能です。\n",
			[37] = "キーボードで音量調整をするときの増加量、減少量を指定します。\n1 ～ 20 の値が指定可能です。\n",
			[38] = "音源再生前の空白時間 (ms)。\n",
			[39] = "リザルト画像自動保存機能：\nONにすると、ハイスコア/ハイスキル時に\n自動でリザルト画像を曲データと同じ\nフォルダに保存します。",
			[40] = "Discordに再生中の譜面情報を送信する",
			[41] = "バッファ入力モード：\nON にすると、FPS を超える入力解像\n度を実現します。\nOFF にすると、入力解像度は FPS に\n等しくなります。",
			[42] = "Traceログ出力：\nDTXManiaLog.txt にログを出力します。\n変更した場合は、DTXMania の再起動\n後に有効となります。",
			[43] = "サウンドの出力方式:\n" +
					"WASAPI, ASIO, DSound(DirectSound)\n" +
					"の中からサウンド出力方式を選択\n" +
					"します。\n" +
					"WASAPIはVista以降でのみ使用可能\n" +
					"です。ASIOは対応機器でのみ使用\n" +
					"可能です。\n" +
					"WASAPIかASIOを指定することで、\n" +
					"遅延の少ない演奏を楽しむことが\n" +
					"できます。\n" +
					"\n" +
					"※ 設定はCONFIGURATION画面の\n" +
					"　終了時に有効になります。",
			[44] = "WASAPI使用時のバッファサイズ:\n" +
					"0～99999ms を指定可能です。\n" +
					"0を指定すると、OSがバッファの\n" +
					"サイズを自動設定します。\n" +
					"値を小さくするほど発音ラグが\n" +
					"減少しますが、音割れや異常動作を\n" +
					"引き起こす場合があります。\n" +
					"※ 設定はCONFIGURATION画面の\n" +
					"　終了時に有効になります。",
            [45] = "ASIOデバイス:\n" +
					"ASIO使用時のサウンドデバイスを\n" +
					"選択します。\n" +
					"\n" +
					"※ 設定はCONFIGURATION画面の\n" +
					"　終了時に有効になります。",
			[46] = "OSタイマーを使用するかどうか:\n" +
					"演奏タイマーとして、DTXMania独自の\n" +
					"タイマーを使うか、OS標準のタイマー\n" +
					"を使うかを選択します。\n" +
					"OS標準タイマーを使うとスクロールが\n" +
					"滑らかになりますが、演奏で音ズレが\n" +
					"発生することがあります。(そのため\n" +
					"AdjustWavesの効果が適用されます。)\n" +
					"\n" +
					"この指定はWASAPI/ASIO使用時のみ有効\n" +
					"です。\n",
			[47] = "キャラクター画像を表示するかどうか\n",
			[48] = "ダンサー画像を表示するかどうか\n",
			[49] = "モブ画像を表示するかどうか\n",
			[50] = "ランナー画像を表示するかどうか\n",
			[51] = "フッター画像を表示するかどうか\n",
			[52] = "事前画像描画機能を使うかどうか。\n",
			[53] = "ぷちキャラ画像を表示するかどうか\n",
			[54] = "スキン切替：\n" +
					"スキンを切り替えます。\n",
			[55] = "システムのキー入力に関する項目を設\n定します。",
			[56] = "AUTO 1P",
			[57] = "すべての音符を自動で演奏します。\n",
			[58] = "AUTO 2P",
			[59] = "すべての音符を自動で演奏します。\n",
			[60] = "連打速度",
			[61] = "AUTO中で黄色連打の速度を変化します。\n" +
					"０で設けったら叩きません。\n" +
					"多くとも各フレーム１打。",
			[62] = "スクロール速度",
			[63] = "演奏時のドラム譜面のスクロールの\n" +
					"速度を指定します。\n" +
					"x0.1 ～ x200.0 を指定可能です。",
			[64] = "完璧モード",
			[65] = "Riskyモードの設定:\n" +
					"1以上の値にすると、その回数分の\n" +
					"不可で演奏が強制終了します。\n" +
					"0にすると無効になり、\n" +
					"ノルマゲージのみになります。\n" +
					"\n",
			[66] = "でたらめ",
			[67] = "いわゆるランダム。\n  RANDOM: ちょっと変わる\n  MIRROR: あべこべ \n  SUPER: そこそこヤバい\n  HYPER: 結構ヤバい\nなお、実装は適当な模様",
			[68] = "ドロン",
			[69] = "DORON:ドロン\n"+
					"STEALTH:ステルス",
			[70] = "情報なし",
			[71] = "有効にすると曲情報などが見えなくなります。\n",
			[72] = "厳密",
			[73] = "有効にすると「良」以外の判定が全て不可になります。\n",
			[74] = "タイト",
			[75] = "ドラムチップのないところでパッドを\n" +
					"叩くとミスになります。",
			[76] = "最低表示コンボ",
			[77] = "表示可能な最小コンボ数（ドラム）：\n" +
					"画面に表示されるコンボの最小の数\n" +
					"を指定します。\n" +
					"1 ～ 99999 の値が指定可能です。",
			[78] = "入力微調整",
			[79] = "ドラムの入力タイミングの微調整を\n" +
					"行います。\n" +
					"-99 ～ 99ms まで指定可能です。\n" +
					"入力ラグを軽減するためには、負の\n" +
					"値を指定してください。\n",
			[80] = "デフォルト難易度",
			[81] = "デフォルトで選択される難易度\n",
			[82] = "スコア計算方法",
			[83] = "スコア計算方法\n" +
					"TYPE-A: 旧配点\n" +
					"TYPE-B: 旧筐体配点\n" +
					"TYPE-C: 新配点\n",
			[84] = "真打モードを有効にする。",
			[85] = "譜面分岐ガイド",
			[86] = "譜面分岐の参考になる数値などを表示します。\n" +
					"オートプレイだと表示されません。",
			[87] = "譜面分岐時アニメーション",
			[88] = "譜面分岐時のアニメーション\n" +
					"TYPE-A: 太鼓7～太鼓14\n" +
					"TYPE-B: 太鼓15～\n" +
					" \n",
			[89] = "ゲームモード",
			[90] = "ゲームモード\n" +
					"(1人プレイ専用)\n" +
					"TYPE-A: 完走!叩ききりまショー!\n" +
					"TYPE-B: 完走!叩ききりまショー!(激辛)\n" +
					" \n",
			[91] = "大音符判定",
			[92] = "大音符の両手判定を有効にします。",
			[93] = "判定数表示",
			[94] = "左下に判定数を表示します。\n" +
					"(1人プレイ専用)",
			[95] = "入力設定",
			[96] = "ドラムのキー入力に関する項目を設\n"+
					"定します。",
			[97] = "キャプチャ",
			[98] = "キャプチャキー設定：\n画面キャプチャのキーの割り当てを設\n定します。",
			[99] = "LeftRed",


			[9996] = "描画の方式:\n" +
					"OpenGL, DirectX11, Vulkan, Metal\n" +
					"の中からグラフィックスAPIを選択します。\n" +
					"OpenGLは遅いですが互換性が高く安定してます\n" +
					//"DirectX9は古いデバイスで高速です、\n" +
					"DirectX11はWindowsでしか動作しませんが、\n" +
					"早くて安定しています\n" +
					"VulkanはWindowsとLinux環境で最速です\n" +
					"MetalはMacのみですが最速です\n" +
					"\n" +
					"この変更は再起動後に反映されます\n",
			[9997] = "グラフィックスAPI",

			[9998] = "Bass使用時のバッファサイズ:\n" +
					"0～99999ms を指定可能です。\n" +
					"0を指定すると、OSがバッファの\n" +
					"サイズを自動設定します。\n" +
					"値を小さくするほど発音ラグが\n" +
					"減少しますが、音割れや異常動作を\n" +
					"引き起こす場合があります。\n" +
					"※ 設定はCONFIGURATION画面の\n" +
					"　終了時に有効になります。",
			[9999] = "Bassバッファサイズ",
			[10000] = "左側の面へのキーの割り当てを設\n定します。",
			[10001] = "RightRed",
			[10002] = "右側の面へのキーの割り当て\nを設定します。",
			[10003] = "LeftBlue",
			[10004] = "左側のふちへのキーの\n割り当てを設定します。",
			[10005] = "RightBlue",
			[10006] = "右側のふちへのキーの\n割り当てを設定します。",
			[10007] = "LeftRed2P",
			[10008] = "左側の面へのキーの割り当てを設\n定します。",
			[10009] = "RightRed2P",
			[10010] = "右側の面へのキーの割り当て\nを設定します。",
			[10011] = "LeftBlue2P",
			[10012] = "左側のふちへのキーの\n割り当てを設定します。",
			[10013] = "RightBlue2P",
			[10014] = "右側のふちへのキーの\n割り当てを設定します。",
			[10018] = "タイムストレッチ",
			[10019] = "全画面モード",
			[10020] = "StageFailed",
			[10021] = "RandSubBox",
			[10022] = "垂直帰線同期",
			[10023] = "AVI",
			[10024] = "BGA",
			[10025] = "PreSoundWait",
			[10026] = "PreImageWait",
			[10027] = "デバッグ情報",
			[10028] = "BG透明度",
			[10029] = "音源使用",
			[10030] = "スコア保存",
			[10031] = "Apply Loudness Metadata",
			[10032] = "Target Loudness",
			[10033] = "SONGVOL使用",
			[10034] = "効果音音量",
			[10035] = "ボイス音量",
			[10036] = "音源音量",
			[10037] = "キーボード音量調整",
			[10038] = "MusicPreTimeMs",
			[10039] = "リザルト画像自動保存",
			[10040] = "Discord送信",
			[10041] = "バッファ入力モード",
			[10042] = "Traceログ出力",
			[10043] = "サウンド出力方式",
			[10044] = "WASAPIバッファサイズ",
			[10045] = "ASIOデバイス",
			[10046] = "OSタイマー使用",
			[10047] = "キャラクター表示",
			[10048] = "踊り子表示",
			[10049] = "モブ表示",
			[10050] = "ランナー表示",
			[10051] = "フッター表示",
			[10052] = "FastRender",
			[10053] = "プチキャラ表示",
			[10054] = "スキン (全体)",
			[10055] = "キー設定",
			[10056] = "段・タワー譜面省略",
			[10057] = "演奏選曲画面にタワー及び\n段の譜面を省略設定です。\nONに設けると別々の画面\nにしか表示されていません。\n" +
					"※ 設定は譜面の再読み込み時\n" +
				"  に有効になります。",
			[10058] = "プレビュー音量",
			[10059] = $"プレビュー時の音源の音量を調節します。\n{CSound.MinimumGroupLevel} ～ {CSound.MaximumGroupLevel} % の値が指定可能です。\n",
			[10060] = "Clap",
			[10061] = "コンガの拍手へのキーの割り当てを設\n定します。",
			[10062] = "Clap2P",
			[10063] = "コンガの拍手へのキーの割り当てを設\n定します。",

			[10064] = "Decide",
			[10065] = "選択のキーの割り当てを設\n定します。",
			[10066] = "Cancel",
			[10067] = "キャンセルのキーの割り当てを設\n定します。",
			[10068] = "LeftChange",
			[10069] = "左のキーの割り当てを設\n定します。",
			[10070] = "RightChange",
			[10071] = "右のキーの割り当てを設\n定します。",

			[10084] = "真打モード",
			[10085] = "システム設定",
			[10086] = "ゲームプレイ設定",
			[10087] = "出る",
			[10091] = "システムに関係する項目を設定します。",
            [10092] = "ドラムの演奏に関する項目を設定します。",
            [10093] = "設定を保存し、コンフィグ画面を終了します。",

			[10094] = "LeftRed3P",
			[10095] = "左側の面へのキーの割り当てを設\n定します。",
			[10096] = "RightRed3P",
			[10097] = "右側の面へのキーの割り当て\nを設定します。",
			[10098] = "LeftBlue3P",
			[10099] = "左側のふちへのキーの\n割り当てを設定します。",
			[10100] = "RightBlue3P",
			[10101] = "右側のふちへのキーの\n割り当てを設定します。",

			[10102] = "LeftRed4P",
			[10103] = "左側の面へのキーの割り当てを設\n定します。",
			[10104] = "RightRed4P",
			[10105] = "右側の面へのキーの割り当て\nを設定します。",
			[10106] = "LeftBlue4P",
			[10107] = "左側のふちへのキーの\n割り当てを設定します。",
			[10108] = "RightBlue4P",
			[10109] = "右側のふちへのキーの\n割り当てを設定します。",

			[10110] = "LeftRed5P",
			[10111] = "左側の面へのキーの割り当てを設\n定します。",
			[10112] = "RightRed5P",
			[10113] = "右側の面へのキーの割り当て\nを設定します。",
			[10114] = "LeftBlue5P",
			[10115] = "左側のふちへのキーの\n割り当てを設定します。",
			[10116] = "RightBlue5P",
			[10117] = "右側のふちへのキーの\n割り当てを設定します。",

			[10118] = "Clap3P",
			[10119] = "コンガの拍手へのキーの割り当てを設\n定します。",
			[10120] = "Clap4P",
			[10121] = "コンガの拍手へのキーの割り当てを設\n定します。",
			[10122] = "Clap5P",
			[10123] = "コンガの拍手へのキーの割り当てを設\n定します。",

			[10124] = "裏表移行アニメーション",
			[10125] = "裏表移行アニメーションを有効する",

			[100] = "演奏ゲーム",
            [101] = "段位道場",
            [102] = "太鼓タワー",
            [103] = "メダル商店",
            [104] = "太鼓大冒険",
            [105] = "太鼓部屋",
            [106] = "設定",
            [107] = "ゲーム終了",
			[108] = "オンライン喫茶店",
			[109] = "Open百科事典",
			[110] = "AIバトル演奏",
			[111] = "プレイヤースタッツ",
			[112] = "譜面エディター",
			[113] = "Open工具箱",

			[150] = "すきな曲や、むずかしさを\nえらんであそべるよ！",
            [151] = "３曲連続して演奏しながら、\nお題の合格を目指そう！\n合格すると腕前が認定されるよ！",
            [152] = "有限のライフを持って、\nタワーの頂上を登って\n挑戦できるよ！",
            [153] = "稼いだメダルを費やして、\n新しい曲やプチキャラを買えるよ！",
            [154] = "様々な障害に挑んで、\n新しい場所を発見出来るよ！",
            [155] = "キャラクタ、プチキャラ、\nネームプレートの情報を変えるよ！",
            [156] = "ゲームスタイルや\n一般的な設定を設けられるよ！",
            [157] = "セッションを終了できるよ。\nまた遊んでね！",
			[158] = "インターネットで新しい譜面\nまたはコンテンツを\nダウンロードできるよ！",
			[159] = "OpenTaikoの機能または新しい\nコンテンツを追加方法\nについて勉強できるよ！",
			[160] = "「AIどん」と勝つか負けるか\nギリギリのバトルをたのしもう！",
			[161] = "自分の進歩を認められるよ！",
			[162] = "好きな曲を使って\n創作.tja譜面を作られるよ！",
			[163] = "様々な役に立つツールを\n使って楽に新しい\nコンテンツを追加できるよ！",

			[200] = "とじる",
            [201] = "最近あそんだ曲",
            [202] = "最近あそんだ曲を集めたよ！",
			[203] = "おまかせ",

			[300] = "コイン取得！",
			[301] = "キャラクター取得！",
			[302] = "プチキャラ取得！",
			[303] = "タイトル取得！",
			[304] = "注目",
			[305] = "確認",
			[306] = "コイン",
			[307] = "累計",

			[400] = "タイトルへ戻る",
			[401] = "戻る",
			[402] = "コンテンツダウンロード",
			[403] = "CDN選択",
			[404] = "曲・譜面ダウンロード",
			[405] = "キャラクターダウンロード",
			[406] = "プチキャラダウンロード",
			[407] = "オンラインマルチプレイヤー",

			[500] = "タイミング",
			[501] = "緩い",
			[502] = "易しい",
			[503] = "通常",
			[504] = "厳しい",
			[505] = "過酷",
			[510] = "スコア乗数：",
			[511] = "コイン乗数：",
			[512] = "ゲームタイプ",
			[513] = "太鼓",
			[514] = "コンガ",
			[515] = "余分なモード",
			[516] = "雪崩",
			[517] = "掃海",

			[1000] = "到達階数",
            [1001] = "階",
            [1002] = "点",
            [1003] = "スコア",

            [1010] = "魂ゲージ",
            [1011] = "良の数",
            [1012] = "可の数",
            [1013] = "不可の数",
            [1014] = "スコア",
            [1015] = "連打数",
            [1016] = "叩けた数",
            [1017] = "コンボ数",
            [1018] = "精度",
			[1019] = "ADLIBの数",
			[1020] = "叩けた爆弾",

			[1030] = "戻る",
            [1031] = "プチキャラ",
            [1032] = "キャラクター",
            [1033] = "段位称号",
            [1034] = "名札称号",

			[1040] = "かんたん",
			[1041] = "ふつう",
			[1042] = "むずかしい",
			[1043] = "おに（表）",
			[1044] = "おに（裏）",
			[1045] = "おに（裏表）",

			[90000] = "[エラー] 無効な条件",
			[90001] = "上記の商品はコイン商店に限りです、部屋に購入ができません。",
			[90002] = "コインの値段 : ",
			[90003] = "商品購入済み !",
			[90004] = "コインが足りません !",
			[90005] = "下記の条件 : ",

			[900] = "もどる",
			[901] = "やりなおす",
			[902] = "演奏中止",

			[910] = "AIドン",
			[911] = "デウス・エクス・マキナ",

			[9000] = "しない",
            [9001] = "する",
            [9002] = "なし",
            [9003] = "きまぐれ",
            [9004] = "でたらめ",
            [9006] = "特訓モード",
            [9007] = "使用不可",
            [9008] = "はやさ",
            [9009] = "ドロン",
            [9010] = "あべこべ",
            [9011] = "ランダム",
            [9012] = "ゲームモード",
            [9013] = "オート",
            [9014] = "ボイス",
            [9015] = "音色",
			[9016] = "ステルス",
			[9017] = "無難",
			[9018] = "厳密",

			[9100] = "検索 （難易度）",
			[9101] = "難易度",
			[9102] = "レベル",

			[9200] = "戻る",
            [9201] = "絶対パス",
            [9202] = "曲名",
            [9203] = "作者名",
            [9204] = "表示レベル",
        };
    }
}
