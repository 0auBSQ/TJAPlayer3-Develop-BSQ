﻿using System.Drawing;
using FDK;

namespace OpenTaiko;

// Simple class containing functions to simplify readability of CChip elements
class NotesManager {

	#region [Parsing]

	public static Dictionary<string, int> NoteCorrespondanceDictionnary = new Dictionary<string, int>() {
		["0"] = 0, // Empty
		["1"] = 1, // Small Don (Taiko) | Red (right) hit (Konga)
		["2"] = 2, // Small Ka (Taiko) | Yellow (left) hit (Konga)
		["3"] = 3, // Big Don (Taiko) | Pink note (Konga)
		["4"] = 4, // Big Ka (Taiko) | Clap (Konga)
		["5"] = 5, // Small roll start | Konga red roll
		["6"] = 6, // Big roll start | Konga pink roll
		["7"] = 7, // Balloon
		["8"] = 8, // Roll/Balloon end
		["9"] = 9, // Kusudama
		["A"] = 10, // Joint Big Don (2P)
		["B"] = 11, // Joint Big Ka (2P)
		["C"] = 12, // Mine
		["D"] = 13, // ProjectOutfox's Fuse roll
		["E"] = 0, // Unused
		["F"] = 15, // ADLib
		["G"] = 0xF1, // Green (Purple) double hit note
		["H"] = 16, // Konga clap roll | Taiko big roll
		["I"] = 17, // Konga yellow roll | Taiko small roll
	};

	public static bool FastFlankedParsing(string s) {
		if (s[0] >= '0' && s[0] <= '9')
			return true;

		for (int i = 0; i < s.Length; i++) {
			if (GetNoteValueFromChar(s.Substring(i, 1)) == -1
			    && s.Substring(i, 1) != ",")
				return false;
		}

		return true;
	}

	public static int GetNoteValueFromChar(string chr) {
		if (NoteCorrespondanceDictionnary.ContainsKey(chr))
			return NoteCorrespondanceDictionnary[chr];
		return -1;
	}

	public static int GetNoteX(CDTX.CChip pChip, double timems, double scroll, int interval, float play_bpm_time, EScrollMode eScrollMode, bool roll) {
		double hbtime = ((roll ? pChip.fBMSCROLLTime_end : pChip.fBMSCROLLTime) - (play_bpm_time));
		double screen_ratio = OpenTaiko.Skin.Resolution[0] / 1280.0;
		switch (eScrollMode) {
			case EScrollMode.Normal:
				return (int)((timems / 240000.0) * interval * scroll * screen_ratio);
			case EScrollMode.BMScroll: {
				return (int)((hbtime / 16.0) * interval * screen_ratio);
			}
			case EScrollMode.HBScroll: {
				return (int)((hbtime / 16.0) * interval * scroll * screen_ratio);
			}
			default:
				return 0;
		}
	}

	public static int GetNoteY(CDTX.CChip pChip, double timems, double scroll, int interval, float play_bpm_time, EScrollMode eScrollMode, bool roll) {
		double hbtime = ((roll ? pChip.fBMSCROLLTime_end : pChip.fBMSCROLLTime) - (play_bpm_time));
		double screen_ratio = OpenTaiko.Skin.Resolution[1] / 720.0;
		switch (eScrollMode) {
			case EScrollMode.Normal:
				return (int)((timems / 240000.0) * interval * scroll * screen_ratio);
			case EScrollMode.BMScroll: {
				return 0;
			}
			case EScrollMode.HBScroll: {
				return (int)((hbtime / 16.0) * interval * scroll * screen_ratio);
			}
			default:
				return 0;
		}
	}

	#endregion

	#region [Gameplay]

	public static bool IsExpectedPad(int stored, int hit, CDTX.CChip chip, EGameType gt) {
		var inPad = (EPad)hit;
		var onPad = (EPad)stored;

		if (chip == null) return false;

		if (IsBigKaTaiko(chip, gt)) {
			return (inPad == EPad.LBlue && onPad == EPad.RBlue)
			       || (inPad == EPad.RBlue && onPad == EPad.LBlue);
		}

		if (IsBigDonTaiko(chip, gt)) {
			return (inPad == EPad.LRed && onPad == EPad.RRed)
			       || (inPad == EPad.RRed && onPad == EPad.LRed);
		}

		if (IsSwapNote(chip, gt)) {
			bool hitBlue = inPad == EPad.LBlue || inPad == EPad.RBlue;
			bool hitRed = inPad == EPad.LRed || inPad == EPad.RRed;
			bool storedBlue = onPad == EPad.LBlue || onPad == EPad.RBlue;
			bool storedRed = onPad == EPad.LRed || onPad == EPad.RRed;

			return (storedRed && hitBlue)
			       || (storedBlue && hitRed);
		}

		return false;
	}

	#endregion

	#region [General]

	public static bool IsCommonNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 >= 0x11 && chip.nチャンネル番号 < 0x18;
	}
	public static bool IsMine(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x1C;
	}

	public static bool IsDonNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x11 || chip.nチャンネル番号 == 0x13 || chip.nチャンネル番号 == 0x1A;
	}

	public static bool IsKaNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x12 || chip.nチャンネル番号 == 0x14 || chip.nチャンネル番号 == 0x1B;
	}

	public static bool IsSmallNote(CDTX.CChip chip, bool blue) {
		if (chip == null) return false;
		return blue ? chip.nチャンネル番号 == 0x12 : chip.nチャンネル番号 == 0x11;
	}

	public static bool IsSmallNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x12 || chip.nチャンネル番号 == 0x11;
	}

	public static bool IsBigNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return (chip.nチャンネル番号 == 0x13 || chip.nチャンネル番号 == 0x14 || chip.nチャンネル番号 == 0x1A || chip.nチャンネル番号 == 0x1B);
	}

	public static bool IsBigKaTaiko(CDTX.CChip chip, EGameType gt) {
		if (chip == null) return false;
		return (chip.nチャンネル番号 == 0x14 || chip.nチャンネル番号 == 0x1B) && gt == EGameType.Taiko;
	}

	public static bool IsBigDonTaiko(CDTX.CChip chip, EGameType gt) {
		if (chip == null) return false;
		return (chip.nチャンネル番号 == 0x13 || chip.nチャンネル番号 == 0x1A) && gt == EGameType.Taiko;
	}

	public static bool IsClapKonga(CDTX.CChip chip, EGameType gt) {
		if (chip == null) return false;
		return (chip.nチャンネル番号 == 0x14 || chip.nチャンネル番号 == 0x1B) && gt == EGameType.Konga;
	}

	public static bool IsSwapNote(CDTX.CChip chip, EGameType gt) {
		if (chip == null) return false;
		return (
			IsKongaPink(chip, gt)                           // Konga Pink note
			|| IsPurpleNote(chip)                       // Purple (Green) note
		);
	}

	public static bool IsKongaPink(CDTX.CChip chip, EGameType gt) {
		if (chip == null) return false;
		// Purple notes are treated as Pink in Konga
		return (chip.nチャンネル番号 == 0x13 || chip.nチャンネル番号 == 0x1A || IsPurpleNote(chip)) && gt == EGameType.Konga;
	}
	public static bool IsPurpleNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return (chip.nチャンネル番号 == 0x101);
	}

	public static bool IsYellowRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x21;
	}

	public static bool IsClapRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x20;
	}

	public static bool IsKusudama(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x19;
	}

	public static bool IsFuzeRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x1D;
	}

	public static bool IsRollEnd(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x18;
	}

	public static bool IsBalloon(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x17;
	}

	public static bool IsBigRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x16;
	}

	public static bool IsSmallRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x15;
	}

	public static bool IsADLIB(CDTX.CChip chip) {
		if (chip == null) return false;
		return chip.nチャンネル番号 == 0x1F;
	}

	public static bool IsRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return IsBigRoll(chip) || IsSmallRoll(chip) || IsClapRoll(chip) || IsYellowRoll(chip);
	}

	public static bool IsGenericBalloon(CDTX.CChip chip) {
		if (chip == null) return false;
		return IsBalloon(chip) || IsKusudama(chip) || IsFuzeRoll(chip);
	}

	public static bool IsGenericRoll(CDTX.CChip chip) {
		if (chip == null) return false;
		return (0x15 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 0x19) ||
		       (chip.nチャンネル番号 == 0x20 || chip.nチャンネル番号 == 0x21)
		       || chip.nチャンネル番号 == 0x1D;
	}

	public static bool IsMissableNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return (0x11 <= chip.nチャンネル番号 && chip.nチャンネル番号 <= 0x14)
		       || chip.nチャンネル番号 == 0x1A
		       || chip.nチャンネル番号 == 0x1B
		       || chip.nチャンネル番号 == 0x101;
	}

	public static bool IsHittableNote(CDTX.CChip chip) {
		if (chip == null) return false;
		return IsMissableNote(chip)
		       || IsGenericRoll(chip)
		       || IsADLIB(chip)
		       || IsMine(chip);
	}

	#endregion

	#region [Displayables]

	// Flying notes
	public static void DisplayNote(int player, int x, int y, int Lane) {
		EGameType _gt = OpenTaiko.ConfigIni.nGameType[OpenTaiko.GetActualPlayer(player)];

		switch (Lane) {
			case 1:
			case 2:
			case 3:
			case 4:
				OpenTaiko.Tx.Notes[(int)_gt]?.t2D中心基準描画(x, y, new Rectangle(Lane * OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1] * 3, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
				break;
			case 5:
				OpenTaiko.Tx.Note_Swap?.t2D中心基準描画(x, y, new Rectangle(0, OpenTaiko.Skin.Game_Notes_Size[1] * 3, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
				break;
		}
	}

	// Regular display
	public static void DisplayNote(int player, int x, int y, CDTX.CChip chip, int frame, int length = -1) {
		if (OpenTaiko.ConfigIni.eSTEALTH[OpenTaiko.GetActualPlayer(player)] != EStealthMode.Off || !chip.bShow)
			return;

		if (length == -1) {
			length = OpenTaiko.Skin.Game_Notes_Size[0];
		}

		EGameType _gt = OpenTaiko.ConfigIni.nGameType[OpenTaiko.GetActualPlayer(player)];

		int noteType = 1;
		if (IsSmallNote(chip, true)) noteType = 2;
		else if (IsBigDonTaiko(chip, _gt) || IsKongaPink(chip, _gt)) noteType = 3;
		else if (IsBigKaTaiko(chip, _gt) || IsClapKonga(chip, _gt)) noteType = 4;
		else if (IsBalloon(chip)) noteType = 11;

		else if (IsMine(chip)) {
			OpenTaiko.Tx.Note_Mine?.t2D描画(x, y);
			return;
		} else if (IsPurpleNote(chip)) {
			OpenTaiko.Tx.Note_Swap?.t2D描画(x, y, new Rectangle(0, frame, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
			return;
		} else if (IsKusudama(chip)) {
			OpenTaiko.Tx.Note_Kusu?.t2D描画(x, y, new Rectangle(0, frame, length, OpenTaiko.Skin.Game_Notes_Size[1]));
			return;
		} else if (IsADLIB(chip)) {
			var puchichara = OpenTaiko.Tx.Puchichara[PuchiChara.tGetPuchiCharaIndexByName(OpenTaiko.GetActualPlayer(player))];
			if (puchichara.effect.ShowAdlib) {
				OpenTaiko.Tx.Note_Adlib?.tUpdateOpacity(50);
				OpenTaiko.Tx.Note_Adlib?.t2D描画(x, y, new Rectangle(0, frame, length, OpenTaiko.Skin.Game_Notes_Size[1]));
			}
			return;
		}

		OpenTaiko.Tx.Notes[(int)_gt]?.t2D描画(x, y, new Rectangle(noteType * OpenTaiko.Skin.Game_Notes_Size[0], frame, length, OpenTaiko.Skin.Game_Notes_Size[1]));
	}

	// Roll display
	public static void DisplayRoll(int player, int x, int y, CDTX.CChip chip, int frame,
		Color4 normalColor, Color4 effectedColor, int x末端, int y末端) {
		EGameType _gt = OpenTaiko.ConfigIni.nGameType[OpenTaiko.GetActualPlayer(player)];

		if (OpenTaiko.ConfigIni.eSTEALTH[OpenTaiko.GetActualPlayer(player)] != EStealthMode.Off || !chip.bShow)
			return;

		int _offset = 0;
		var _texarr = OpenTaiko.Tx.Notes[(int)_gt];
		int rollOrigin = (OpenTaiko.Skin.Game_Notes_Size[0] * 5);
		float _adjust = OpenTaiko.Skin.Game_Notes_Size[0] / 2.0f;
		float image_size = OpenTaiko.Skin.Game_Notes_Size[0];

		if (IsSmallRoll(chip) || (_gt == EGameType.Taiko && IsYellowRoll(chip))) {
			_offset = 0;
		}
		if (IsBigRoll(chip) || (_gt == EGameType.Taiko && IsClapRoll(chip))) {
			_offset = OpenTaiko.Skin.Game_Notes_Size[0] * 3;
		} else if (IsClapRoll(chip) && _gt == EGameType.Konga) {
			_offset = OpenTaiko.Skin.Game_Notes_Size[0] * 11;
		} else if (IsYellowRoll(chip) && _gt == EGameType.Konga) {
			_offset = OpenTaiko.Skin.Game_Notes_Size[0] * 8;
		} else if (IsFuzeRoll(chip)) {
			_texarr = OpenTaiko.Tx.Note_FuseRoll;
			_offset = -rollOrigin;
		}

		if (_texarr == null) return;

		int index = x末端 - x;


		//var theta = -Math.Atan2(chip.dbSCROLL_Y, chip.dbSCROLL);
		var theta = -Math.Atan2(y末端 - y, x末端 - x);
		// Temporary patch for odd math bug, to fix later, still bugs on katharsis (negative roll)
		if (chip.dbSCROLL_Y == 0)//theta == 0 || theta == -Math.PI)
			theta += 0.00000000001;


		var dist = Math.Sqrt(Math.Pow(x末端 - x, 2) + Math.Pow(y末端 - y, 2)) + 1;
		var div = dist / image_size;
		//var odiv = (index - _adjust + _adjust + 1) / TJAPlayer3.Skin.Game_Notes_Size[0];

		if (OpenTaiko.Skin.Game_RollColorMode != CSkin.RollColorMode.None)
			_texarr.color4 = effectedColor;
		else
			_texarr.color4 = normalColor;

		// Body
		_texarr.vcScaleRatio.X = (float)div;
		_texarr.fZ軸中心回転 = (float)theta;
		//var _x0 = x + _adjust;
		//var _y0 = y + 0f;

		var _center_x = (x + x末端 + image_size) / 2;
		var _center_y = _adjust + (y + y末端) / 2;
		//TJAPlayer3.Tx.Notes[(int)_gt].t2D描画(_x0, _y0, new Rectangle(rollOrigin + TJAPlayer3.Skin.Game_Notes_Size[0] + _offset, 0, TJAPlayer3.Skin.Game_Notes_Size[0], TJAPlayer3.Skin.Game_Notes_Size[1]));
		_texarr.t2D_DisplayImage_RollNote((int)_center_x, (int)_center_y, new Rectangle(rollOrigin + OpenTaiko.Skin.Game_Notes_Size[0] + _offset, 0, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
		//t2D拡大率考慮中央基準描画 t2D中心基準描画

		// Tail
		_texarr.vcScaleRatio.X = 1.0f;

		// Only display the roll tail if the distance is high enough to see the tail texture to avoid math issues
		if (dist > 3) {
			//var _x0 = x末端 + _adjust;
			//var _y0 = y末端 + 0f;
			var _d = _adjust;

			var x1 = x + _adjust;
			var y1 = y + _adjust;
			var x2 = x末端 + _adjust;
			var y2 = y末端 + _adjust;
			var _xc = x2 + (x2 - x1) * _d / dist;
			var _yc = y2 + (y2 - y1) * _d / dist;
			//TJAPlayer3.Tx.Notes[(int)_gt].t2D描画((int)_x0, (int)_y0, 0, new Rectangle(rollOrigin + (TJAPlayer3.Skin.Game_Notes_Size[0] * 2) + _offset, frame, TJAPlayer3.Skin.Game_Notes_Size[0], TJAPlayer3.Skin.Game_Notes_Size[1]));
			_texarr.t2D中心基準描画((int)_xc, (int)_yc, 0, new Rectangle(rollOrigin + (OpenTaiko.Skin.Game_Notes_Size[0] * 2) + _offset, frame, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
		}

		_texarr.fZ軸中心回転 = 0;

		if (OpenTaiko.Skin.Game_RollColorMode == CSkin.RollColorMode.All)
			_texarr.color4 = effectedColor;
		else
			_texarr.color4 = normalColor;

		// Head
		_texarr.t2D描画(x, y, 0, new Rectangle(rollOrigin + _offset, frame, OpenTaiko.Skin.Game_Notes_Size[0], OpenTaiko.Skin.Game_Notes_Size[1]));
		_texarr.color4 = normalColor;
	}

	// SENotes
	public static void DisplaySENotes(int player, int x, int y, CDTX.CChip chip) {
		if (OpenTaiko.ConfigIni.eSTEALTH[OpenTaiko.GetActualPlayer(player)] == EStealthMode.Stealth)
			return;

		EGameType _gt = OpenTaiko.ConfigIni.nGameType[OpenTaiko.GetActualPlayer(player)];

		if (IsMine(chip)) {
			OpenTaiko.Tx.SENotesExtension?.t2D描画(x, y, new Rectangle(0, OpenTaiko.Skin.Game_SENote_Size[1], OpenTaiko.Skin.Game_SENote_Size[0], OpenTaiko.Skin.Game_SENote_Size[1]));
		} else if (IsPurpleNote(chip) && _gt != EGameType.Konga) {
			OpenTaiko.Tx.SENotesExtension?.t2D描画(x, y, new Rectangle(0, 0, OpenTaiko.Skin.Game_SENote_Size[0], OpenTaiko.Skin.Game_SENote_Size[1]));
		} else if (IsFuzeRoll(chip)) {
			OpenTaiko.Tx.SENotesExtension?.t2D描画(x, y, new Rectangle(0, OpenTaiko.Skin.Game_SENote_Size[1] * 2, OpenTaiko.Skin.Game_SENote_Size[0], OpenTaiko.Skin.Game_SENote_Size[1]));
		} else if (IsKusudama(chip)) {
			OpenTaiko.Tx.SENotesExtension?.t2D描画(x, y, new Rectangle(0, OpenTaiko.Skin.Game_SENote_Size[1] * 3, OpenTaiko.Skin.Game_SENote_Size[0], OpenTaiko.Skin.Game_SENote_Size[1]));
		} else {
			OpenTaiko.Tx.SENotes[(int)_gt]?.t2D描画(x, y, new Rectangle(0, OpenTaiko.Skin.Game_SENote_Size[1] * chip.nSenote, OpenTaiko.Skin.Game_SENote_Size[0], OpenTaiko.Skin.Game_SENote_Size[1]));
		}
	}


	#endregion

}
