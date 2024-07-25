﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using FDK;
using TJAPlayer3;

namespace TJAPlayer3 {
	[Serializable]
	public class CScoreIni {
		// プロパティ

		[Serializable]
		public class C演奏記録 {
			public int nOkCount;
			public int nBadCount;
			public int nGoodCount;
			public Dan_C[] Dan_C;

			public C演奏記録() {
				Dan_C = new Dan_C[CExamInfo.cMaxExam];
			}

		}

	}
}
