﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using FDK;

namespace TJAPlayer3
{
	internal class CAct演奏Drumsゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public double db現在のゲージ値
//		{
//			get
//			{
//				return this.dbゲージ値;
//			}
//			set
//			{
//				this.dbゲージ値 = value;
//				if( this.dbゲージ値 > 1.0 )
//				{
//					this.dbゲージ値 = 1.0;
//				}
//			}
//		}

		
		// コンストラクタ
        /// <summary>
        /// ゲージの描画クラス。ドラム側。
        /// 
        /// 課題
        /// _ゲージの実装。
        /// _Danger時にゲージの色が変わる演出の実装。
        /// _Danger、MAX時のアニメーション実装。
        /// </summary>
		public CAct演奏Drumsゲージ()
		{
			base.b活性化してない = true;
		}

        public override void Start(int nLane, E判定 judge, int player)
        {
            for (int j = 0; j < 32; j++)
            {
                if( player == 0 )
                {
                    if( !this.st花火状態[ j ].b使用中 )
                    {
                        this.st花火状態[j].ct進行 = new CCounter(0, 10, 20, TJAPlayer3.Timer);
                        this.st花火状態[j].nPlayer = player;

                        switch (nLane)
                        {
                            case 0x11:
                            case 0x12:
                            case 0x15:
                                this.st花火状態[j].isBig = false;
                                break;
                            case 0x13:
                            case 0x14:
                            case 0x16:
                            case 0x17:
                                this.st花火状態[j].isBig = true;
                                break;
                        }
                        this.st花火状態[j].nLane = nLane;

                        this.st花火状態[j].b使用中 = true;
                        break;
                    }
                }
                if( player == 1 )
                {
                    if( !this.st花火状態2P[ j ].b使用中 )
                    {
                        this.st花火状態2P[ j ].ct進行 = new CCounter(0, 10, 20, TJAPlayer3.Timer);
                        this.st花火状態2P[ j ].nPlayer = player;

                        switch (nLane)
                        {
                            case 0x11:
                            case 0x12:
                            case 0x15:
                                this.st花火状態2P[ j ].isBig = false;
                                break;
                            case 0x13:
                            case 0x14:
                            case 0x16:
                            case 0x17:
                                this.st花火状態2P[ j ].isBig = true;
                                break;
                        }
                        this.st花火状態2P[j].nLane = nLane;
                        this.st花火状態2P[ j ].b使用中 = true;
                        break;
                    }
                }
            }
        }

		// CActivity 実装

		public override void On活性化()
		{
            this.ct炎 = new CCounter( 0, 6, 50, TJAPlayer3.Timer );

            for (int i = 0; i < 32; i++ )
            {
                this.st花火状態[i].ct進行 = new CCounter();
                this.st花火状態2P[i].ct進行 = new CCounter();
            }
			base.On活性化();
		}
		public override void On非活性化()
		{
            for (int i = 0; i < 32; i++ )
            {
                this.st花火状態[i].ct進行 = null;
                this.st花火状態2P[i].ct進行 = null;
            }
            this.ct炎 = null;
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
				
                if(TJAPlayer3.Skin.Game_Gauge_Rainbow_Timer <= 1)
                {
                    throw new DivideByZeroException("SkinConfigの設定\"Game_Gauge_Rainbow_Timer\"を1以下にすることは出来ません。");
                }
                this.ct虹アニメ = new CCounter( 0, TJAPlayer3.Skin.Game_Gauge_Rainbow_Ptn -1, TJAPlayer3.Skin.Game_Gauge_Rainbow_Timer, TJAPlayer3.Timer );
                this.ct虹透明度 = new CCounter(0, TJAPlayer3.Skin.Game_Gauge_Rainbow_Timer-1, 1, TJAPlayer3.Timer);
                this.ctGaugeFlash = new CCounter(0, 532, 1, TJAPlayer3.Timer);
                //this.tx音符 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_taiko_notes.png"));
                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                this.ct虹アニメ = null;

                base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if ( !base.b活性化してない )
			{
                //CDTXMania.act文字コンソール.tPrint( 20, 150, C文字コンソール.Eフォント種別.白, this.db現在のゲージ値.Taiko.ToString() );

                #region [ 初めての進行描画 ]
				if ( base.b初めての進行描画 )
				{
					base.b初めての進行描画 = false;
                }
                #endregion

                this.ctGaugeFlash.t進行Loop();

                int nWidth = (TJAPlayer3.Skin.Game_Gauge_Rect[2] / 50);
                int nRectX2P = (int)(this.db現在のゲージ値[1] / 2) * nWidth;
                int nRectX = (int)( this.db現在のゲージ値[ 0 ] / 2 ) * nWidth;
                int 虹ベース = ct虹アニメ.n現在の値 + 1;
                if (虹ベース == ct虹アニメ.n終了値+1) 虹ベース = 0;
                /*

                新虹ゲージの仕様  2018/08/10 ろみゅ～？
                 
                 フェードで動く虹ゲージが、ある程度強化できたので放出。
                 透明度255の虹ベースを描画し、その上から透明度可変式の虹ゲージを描画する。
                 ゲージのパターン枚数は、読み込み枚数によって決定する。
                 ゲージ描画の切り替え速度は、タイマーの値をSkinConfigで指定して行う(初期値50,1にするとエラーを吐く模様)。進行速度は1ms、高フレームレートでの滑らかさを重視。
                 虹ゲージの透明度調整値は、「255/パターン数」で算出する。
                 こんな簡単なことを考えるのに30分(60f/s換算で108000f)を費やす。
                 
                */

                // No gauge if tower
                if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Tower || TJAPlayer3.ConfigIni.bTokkunMode)
                    return 0;

                float scale = 1.0f;
                if (TJAPlayer3.ConfigIni.bAIBattleMode)
                {
                    scale = 0.8f;
                }

                #region [Gauge base]

                if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan)
                {
                    if (TJAPlayer3.P1IsBlue())
                    {
                        TJAPlayer3.Tx.Gauge_Dan[4]?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[0], TJAPlayer3.Skin.Game_Gauge_Y[0], 
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                    }
                    else
                    {
                        TJAPlayer3.Tx.Gauge_Dan[0]?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[0], TJAPlayer3.Skin.Game_Gauge_Y[0],
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                    }

                    if (TJAPlayer3.Tx.Gauge_Dan[2] != null)
                    {
                        for (int i = 0; i < TJAPlayer3.DTX.Dan_C.Length; i++)
                        {
                            if (TJAPlayer3.DTX.Dan_C[i] != null)
                            {
                                if (TJAPlayer3.DTX.Dan_C[i].GetExamType() == Exam.Type.Gauge)
                                {
                                    TJAPlayer3.Tx.Gauge_Dan[2].t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[0] + (TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth), TJAPlayer3.Skin.Game_Gauge_Y[0], 
                                        new Rectangle((TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth), 0, TJAPlayer3.Skin.Game_Gauge_Rect[2] - (TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth), TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (TJAPlayer3.stage演奏ドラム画面.bDoublePlay && !TJAPlayer3.ConfigIni.bAIBattleMode)
                    {
                        TJAPlayer3.Tx.Gauge_Base[1]?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1],
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                    }
                    if (TJAPlayer3.P1IsBlue())
                    {
                        TJAPlayer3.Tx.Gauge_Base[2]?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[0], TJAPlayer3.Skin.Game_Gauge_Y[0],
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                    }
                    else
                    {
                        int x;
                        int y;
                        if (TJAPlayer3.ConfigIni.bAIBattleMode)
                        {
                            x = TJAPlayer3.Skin.Game_Gauge_X_AI;
                            y = TJAPlayer3.Skin.Game_Gauge_Y_AI;
                        }
                        else
                        {
                            x = TJAPlayer3.Skin.Game_Gauge_X[0];
                            y = TJAPlayer3.Skin.Game_Gauge_Y[0];
                        }

                        if (TJAPlayer3.Tx.Gauge_Base[0] != null)
                        {
                            TJAPlayer3.Tx.Gauge_Base[0].vc拡大縮小倍率.X = scale;
                            TJAPlayer3.Tx.Gauge_Base[0].vc拡大縮小倍率.Y = scale;

                            TJAPlayer3.Tx.Gauge_Base[0].t2D描画(TJAPlayer3.app.Device, x, y,
                                new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                        }
                    }
                }

                #endregion

                #region [ Gauge 1P ]

                if( TJAPlayer3.Tx.Gauge[0] != null )
                {
                    int x;
                    int y;
                    if (TJAPlayer3.ConfigIni.bAIBattleMode)
                    {
                        x = TJAPlayer3.Skin.Game_Gauge_X_AI;
                        y = TJAPlayer3.Skin.Game_Gauge_Y_AI;
                    }
                    else
                    {
                        x = TJAPlayer3.Skin.Game_Gauge_X[0];
                        y = TJAPlayer3.Skin.Game_Gauge_Y[0];
                    }

                    if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan)
                    {
                        if (TJAPlayer3.P1IsBlue())
                            TJAPlayer3.Tx.Gauge_Dan[5]?.t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(0, 0, nRectX, TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                        else
                            TJAPlayer3.Tx.Gauge_Dan[1]?.t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(0, 0, nRectX, TJAPlayer3.Skin.Game_Gauge_Rect[3]));

                        for (int i = 0; i < TJAPlayer3.DTX.Dan_C.Length; i++)
                        {
                            if (TJAPlayer3.DTX.Dan_C[i] != null && TJAPlayer3.DTX.Dan_C[i].GetExamType() == Exam.Type.Gauge && db現在のゲージ値[0] >= TJAPlayer3.DTX.Dan_C[i].GetValue(false))
                            {
                                TJAPlayer3.Tx.Gauge_Dan[3].Opacity = 255;
                                TJAPlayer3.Tx.Gauge_Dan[3]?.t2D描画(TJAPlayer3.app.Device, x + (TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth), y, new Rectangle(0, 0, nRectX - (TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth), TJAPlayer3.Skin.Game_Gauge_Rect[3]));

                                int Opacity = 0;
                                if (this.ctGaugeFlash.n現在の値 <= 365) Opacity = 0;
                                else if (this.ctGaugeFlash.n現在の値 <= 448) Opacity = (int)((this.ctGaugeFlash.n現在の値 - 365) / 83f * 255f);
                                else if (this.ctGaugeFlash.n現在の値 <= 531) Opacity = 255 - (int)((this.ctGaugeFlash.n現在の値 - 448) / 83f * 255f);
                                TJAPlayer3.Tx.Gauge_Dan[3].Opacity = Opacity;
                                TJAPlayer3.Tx.Gauge_Dan[3]?.t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(0, 0, TJAPlayer3.DTX.Dan_C[i].GetValue(false) / 2 * nWidth, TJAPlayer3.Skin.Game_Gauge_Rect[3]));

                                break;
                            }
                        }

                    }
                    else
                    {
                        if (TJAPlayer3.P1IsBlue())
                            TJAPlayer3.Tx.Gauge[2]?.t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(0, 0, nRectX, TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                        else
                        {
                            if (TJAPlayer3.Tx.Gauge[0] != null)
                            {
                                TJAPlayer3.Tx.Gauge[0].vc拡大縮小倍率.X = scale;
                                TJAPlayer3.Tx.Gauge[0].vc拡大縮小倍率.Y = scale;

                                TJAPlayer3.Tx.Gauge[0].t2D描画(TJAPlayer3.app.Device, x, y, new Rectangle(0, 0, nRectX, TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                            }
                        }
                    }

                    if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan && db現在のゲージ値[0] >= 80.0 && db現在のゲージ値[0] < 100.0)
                    {
                        int Opacity = 0;
                        if (this.ctGaugeFlash.n現在の値 <= 365) Opacity = 0;
                        else if (this.ctGaugeFlash.n現在の値 <= 448) Opacity = (int)((this.ctGaugeFlash.n現在の値 - 365) / 83f * 255f);
                        else if (this.ctGaugeFlash.n現在の値 <= 531) Opacity = 255 - (int)((this.ctGaugeFlash.n現在の値 - 448) / 83f * 255f);

                        if (TJAPlayer3.Tx.Gauge_Flash != null)
                        {
                            TJAPlayer3.Tx.Gauge_Flash.vc拡大縮小倍率.X = scale;
                            TJAPlayer3.Tx.Gauge_Flash.vc拡大縮小倍率.Y = scale;

                            TJAPlayer3.Tx.Gauge_Flash.Opacity = Opacity;
                            TJAPlayer3.Tx.Gauge_Flash.t2D描画(TJAPlayer3.app.Device, x, y,
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                        }
                        
                    }


                    if (TJAPlayer3.Tx.Gauge_Line[0] != null )
                    {
                        #region [Rainbow]

                        if ( this.db現在のゲージ値[ 0 ] >= 100.0 )
                        {
                            this.ct虹アニメ.t進行Loop();
			                this.ct虹透明度.t進行Loop();
                            if(TJAPlayer3.Tx.Gauge_Rainbow[ this.ct虹アニメ.n現在の値 ] != null )
                            {
                                TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].vc拡大縮小倍率.X = scale;
                                TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].vc拡大縮小倍率.Y = scale;

                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].vc拡大縮小倍率.X = scale;
                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].vc拡大縮小倍率.Y = scale;

                                TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].Opacity = 255;
                                TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].t2D描画(TJAPlayer3.app.Device, x, y + (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? (TJAPlayer3.Skin.Game_Gauge_Rect[3] / 2) : 0),
                                    new RectangleF(0,
                                    TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? 22 : 0,
                                    TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].szテクスチャサイズ.Width,
                                    TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].szテクスチャサイズ.Height - (TJAPlayer3.Skin.Game_Gauge_Rect[3] / 2) : TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値].szテクスチャサイズ.Height));
                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].Opacity = (ct虹透明度.n現在の値 * 255 / (int)ct虹透明度.n終了値)/1;
                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].t2D描画(TJAPlayer3.app.Device, x, y + (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? 22 : 0), 
                                    new RectangleF(0, 
                                    TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? 22 : 0, 
                                    TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].szテクスチャサイズ.Width,
                                    TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan ? TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].szテクスチャサイズ.Height - (TJAPlayer3.Skin.Game_Gauge_Rect[3] / 2) : TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].szテクスチャサイズ.Height));
                            }
                        }

                        #endregion


                        TJAPlayer3.Tx.Gauge_Line[0].vc拡大縮小倍率.X = scale;
                        TJAPlayer3.Tx.Gauge_Line[0].vc拡大縮小倍率.Y = scale;

                        TJAPlayer3.Tx.Gauge_Line[0].t2D描画( TJAPlayer3.app.Device, x, y);
                    }

                    #region[ 「Clear」icon ]
                    if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan)
                    {
                        int text_x;
                        int text_y;
                        if (TJAPlayer3.ConfigIni.bAIBattleMode)
                        {
                            text_x = TJAPlayer3.Skin.Game_Gauge_ClearText_X_AI;
                            text_y = TJAPlayer3.Skin.Game_Gauge_ClearText_Y_AI;
                        }
                        else
                        {
                            text_x = TJAPlayer3.Skin.Game_Gauge_ClearText_X[0];
                            text_y = TJAPlayer3.Skin.Game_Gauge_ClearText_Y[0];
                        }

                        if (this.db現在のゲージ値[0] >= 80.0)
                        {
                            TJAPlayer3.Tx.Gauge[0].t2D描画(TJAPlayer3.app.Device, text_x, text_y, 
                                new Rectangle(TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[0], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[2], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[3]));
                        }
                        else
                        {
                            TJAPlayer3.Tx.Gauge[0].t2D描画(TJAPlayer3.app.Device, text_x, text_y,
                                new Rectangle(TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[0], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[2], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[3]));
                        }
                    }
                    #endregion

                }

                #endregion

                #region [ Gauge 2P ]

                if( TJAPlayer3.stage演奏ドラム画面.bDoublePlay && !TJAPlayer3.ConfigIni.bAIBattleMode && TJAPlayer3.Tx.Gauge[1] != null )
                {
                    TJAPlayer3.Tx.Gauge[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1], new Rectangle( 0, 0, nRectX2P, TJAPlayer3.Skin.Game_Gauge_Rect[3]) );
                    if (db現在のゲージ値[1] >= 80.0 && db現在のゲージ値[1] < 100.0)
                    {
                        int Opacity = 0;
                        if (this.ctGaugeFlash.n現在の値 <= 365) Opacity = 0;
                        else if (this.ctGaugeFlash.n現在の値 <= 448) Opacity = (int)((this.ctGaugeFlash.n現在の値 - 365) / 83f * 255f);
                        else if (this.ctGaugeFlash.n現在の値 <= 531) Opacity = 255 - (int)((this.ctGaugeFlash.n現在の値 - 448) / 83f * 255f);
                        TJAPlayer3.Tx.Gauge_Flash.Opacity = Opacity;
                        TJAPlayer3.Tx.Gauge_Flash.t2D上下反転描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1],
                            new Rectangle(TJAPlayer3.Skin.Game_Gauge_Rect[0], TJAPlayer3.Skin.Game_Gauge_Rect[1], TJAPlayer3.Skin.Game_Gauge_Rect[2], TJAPlayer3.Skin.Game_Gauge_Rect[3]));
                    }
                    if (TJAPlayer3.Tx.Gauge[1] != null )
                    {
                        if (this.db現在のゲージ値[1] >= 100.0)
                        {
                            this.ct虹アニメ.t進行Loop();
			                this.ct虹透明度.t進行Loop();
                            if (TJAPlayer3.Tx.Gauge_Rainbow[this.ct虹アニメ.n現在の値] != null)
                            {
                                TJAPlayer3.Tx.Gauge_Rainbow[ct虹アニメ.n現在の値].Opacity = 255;
                                TJAPlayer3.Tx.Gauge_Rainbow[ct虹アニメ.n現在の値].t2D上下反転描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1]);
                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].Opacity = (int)(ct虹透明度.n現在の値 * 255 / ct虹透明度.n終了値) / 1;
                                TJAPlayer3.Tx.Gauge_Rainbow[虹ベース].t2D上下反転描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1]);
                            }
                        }
                        TJAPlayer3.Tx.Gauge_Line[1]?.t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_X[1], TJAPlayer3.Skin.Game_Gauge_Y[1]);
                    }
                    #region[ 「クリア」文字 ]
                    if( this.db現在のゲージ値[ 1 ] >= 80.0 )
                    {
                        TJAPlayer3.Tx.Gauge[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_ClearText_X[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Y[1],
                                new Rectangle(TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[0], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[2], TJAPlayer3.Skin.Game_Gauge_ClearText_Rect[3]));
                    }
                    else
                    {
                        TJAPlayer3.Tx.Gauge[1].t2D描画( TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_Gauge_ClearText_X[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Y[1],
                                new Rectangle(TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[0], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[1], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[2], TJAPlayer3.Skin.Game_Gauge_ClearText_Clear_Rect[3]));
                    }
                    #endregion
                }
                #endregion

                // Soul fire here
                if(TJAPlayer3.Tx.Gauge_Soul_Fire != null )
                {
                    //仮置き
                    int soulfire_width = TJAPlayer3.Tx.Gauge_Soul_Fire.szテクスチャサイズ.Width / 8;
                    int soulfire_height = TJAPlayer3.Tx.Gauge_Soul_Fire.szテクスチャサイズ.Height;
                    for ( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
                    {
                        if (TJAPlayer3.ConfigIni.bAIBattleMode && i == 1) break;

                        int x;
                        int y;
                        if (TJAPlayer3.ConfigIni.bAIBattleMode)
                        {
                            x = TJAPlayer3.Skin.Gauge_Soul_Fire_X_AI;
                            y = TJAPlayer3.Skin.Gauge_Soul_Fire_Y_AI;
                        }
                        else
                        {
                            x = TJAPlayer3.Skin.Gauge_Soul_Fire_X[i];
                            y = TJAPlayer3.Skin.Gauge_Soul_Fire_Y[i];
                        }

                        if ( this.db現在のゲージ値[ i ] >= 100.0)
                        {
                            this.ct炎.t進行Loop();

                            TJAPlayer3.Tx.Gauge_Soul_Fire.vc拡大縮小倍率.X = scale;
                            TJAPlayer3.Tx.Gauge_Soul_Fire.vc拡大縮小倍率.Y = scale;

                            TJAPlayer3.Tx.Gauge_Soul_Fire.t2D描画( TJAPlayer3.app.Device, x, y, new Rectangle(soulfire_width * ( this.ct炎.n現在の値 ), 0, soulfire_width, soulfire_height) );
                        }
                    }
                }
                if(TJAPlayer3.Tx.Gauge_Soul != null )
                {
                    //仮置き
                    int soul_height = TJAPlayer3.Tx.Gauge_Soul.szテクスチャサイズ.Height / 2;
                    for( int i = 0; i < TJAPlayer3.ConfigIni.nPlayerCount; i++ )
                    {
                        if (TJAPlayer3.ConfigIni.bAIBattleMode && i == 1) break;

                        int x;
                        int y;
                        if (TJAPlayer3.ConfigIni.bAIBattleMode)
                        {
                            x = TJAPlayer3.Skin.Gauge_Soul_X_AI;
                            y = TJAPlayer3.Skin.Gauge_Soul_Y_AI;
                        }
                        else
                        {
                            x = TJAPlayer3.Skin.Gauge_Soul_X[i];
                            y = TJAPlayer3.Skin.Gauge_Soul_Y[i];
                        }

                        TJAPlayer3.Tx.Gauge_Soul.vc拡大縮小倍率.X = scale;
                        TJAPlayer3.Tx.Gauge_Soul.vc拡大縮小倍率.Y = scale;

                        if ( this.db現在のゲージ値[ i ] >= 80.0)
                        {
                            TJAPlayer3.Tx.Gauge_Soul.t2D描画( TJAPlayer3.app.Device, x, y, new Rectangle( 0, 0, TJAPlayer3.Tx.Gauge_Soul.szテクスチャサイズ.Width, soul_height) );
                        }
                        else
                        {
                            TJAPlayer3.Tx.Gauge_Soul.t2D描画( TJAPlayer3.app.Device, x, y, new Rectangle( 0, soul_height, TJAPlayer3.Tx.Gauge_Soul.szテクスチャサイズ.Width, soul_height) );
                        }
                    }
                }

                //仮置き
                int[] nSoulExplosion = new int[] { 73, 468, 0, 0 };
                for( int d = 0; d < 32; d++ )
                {
                    if( this.st花火状態[d].b使用中 )
                    {
                        this.st花火状態[d].ct進行.t進行();
                        if (this.st花火状態[d].ct進行.b終了値に達した)
                        {
                            this.st花火状態[d].ct進行.t停止();
                            this.st花火状態[d].b使用中 = false;
                        }
                            
                           
                        break;
                    }
                }
                for( int d = 0; d < 32; d++ )
                {
                    if (this.st花火状態2P[d].b使用中)
                    {
                        this.st花火状態2P[d].ct進行.t進行();
                        if (this.st花火状態2P[d].ct進行.b終了値に達した)
                        {
                            this.st花火状態2P[d].ct進行.t停止();
                            this.st花火状態2P[d].b使用中 = false;
                        }
                            
                            
                        break;
                    }
                }
			}
			return 0;
		}


        // その他

        #region [ private ]
        //-----------------
        private CCounter ctGaugeFlash;

        protected STSTATUS[] st花火状態 = new STSTATUS[ 32 ];
        protected STSTATUS[] st花火状態2P = new STSTATUS[ 32 ];
        [StructLayout(LayoutKind.Sequential)]
        protected struct STSTATUS
        {
            public CCounter ct進行;
            public bool isBig;
            public bool b使用中;
            public int nPlayer;
            public int nLane;
        }
		//-----------------
		#endregion
	}
}
