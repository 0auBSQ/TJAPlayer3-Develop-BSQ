using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FDK;

namespace TJAPlayer3
{
    internal class CLang_es : ILang
    {
        string ILang.GetString(int idx)
        {
            if (!dictionnary.ContainsKey(idx))
                return "[!] No se ha encontrado el índice en el diccionario.";

            return dictionnary[idx];
        }


        private static readonly Dictionary<int, string> dictionnary = new Dictionary<int, string>
        {
            [0] = "Cambia el idioma que se usa\nen el juego y menús.",
            [1] = "Idioma del sistema",
            [2] = "<< Volver al menú",
            [3] = "Volver al menú de la izquierda.",
            [4] = "Recargar lista de canciones",
            [5] = "Recarga y actualiza la lista de canciones.",
            [6] = "Número de jugadores",
            [7] = "Cambia el número de jugadores.\nAjustarlo a 2 permite jugar\ncanciones regulares a dos jugadores dividiendo \nla pantalla a la mitad.",
            [8] = "Modo Kanpeki",
            [9] = "Modo Kanpeki:\nElige el numero de fallos antes de\nque se considere un intento fallido.\nDejar en 0 para deshabilitar el modo Kanpeki.",
            [10] = "Velocidad de la canción",
            [11] = "Cambia la velocidad de la canción.\n" +
                "Por ejemplo, puedes jugar a mitad de\n" +
                "velocidad ajustando el valor PlaySpeed = 0.500\n" +
                "para practicar.\n" +
                "\n" +
                "Nota: También cambia el tono de la canción.\n" +
                "Si Time Stretch está encendido, puede haber\n" +
                "lag de audio si se usa en menos de x0.9.",
            [16] = "Tipo de interfaz",
            [17] = "Puedes cambiar la interfaz de las canciones \nmostradas en la pantalla de selección.\n" +
                "0 : Regular (Diagonal de arriba hacia abajo)\n" +
                "1 : Vertical\n" +
                "2 : Diagonal de abajo hacia arriba\n" +
                "3 : Medio circulo hacia la derecha\n" +
                "4 : Medio circulo hacia la izquierda",
            [18] = "No se está seguro de que hace esto.\nUsa mas capacidad de la CPU,\ny causa lag si la velocidad de juego está\nen menos de x0.9.",
            [19] = "Modo de ventana o pantalla completa.",
            [20] = "Ajuste que proviene de DTXMania.\nEn OpenTaiko este no hace nada.",
            [21] = "Activar el uso de subcarpetas en la\nSELECCIÓN ALEATORIA.",
            [23] = "Usar la reproducción AVI o no.",
            [24] = "Activar BGA (animaciones de fondo) o no.",
            [25] = "Tiempo de retraso(ms) para empezar a reproducir la\ndemo de la música en la pantalla\nSELECCIONAR CANCIÓN.\nPuedes especificar de 0ms a 10000ms",
            [26] = "Ajuste que proviene de DTXMania.\nEn OpenTaiko este no hace nada.",
            [27] = "Si está activado se mostrará información extra en\nla zona de BGA. (FPS, BPM, tiempo total, etc)\nPuedes activar o desactivar los indicadores\npresionando [Del] mientras juegas.",
            [28] = "Ajuste del grado de transparencia del\n juego y fondo.\n\n0=completamente transparente,\n255=sin transparencia",
            [29] = "Desactívalo si no quieres que\nse reproduzca música de fondo.",
            [30] = "Si quieres guardar tus records, actívalo.\nDesactívalo si tus canciones estan en\nun medio de solo lectura (CD-ROM, etc).\nNote that the score files also contain\n 'BGM Adjust' parameter. So if you\n want to keep adjusting parameter,\n you need to set SaveScore=ON.",
            [31] = "Originalmente una opción especial\npara el control de volumen.\nEste ajuste ya no sirve y deberia ser removido.",
            [32] = "Otro ajuste relacionado con BS1770GAIN\ny por lo mismo, inutilizable.",
            [33] = "Actívalo para usar el valor SONGVOL desde\nel .tja, Desactívalo si quieres usar los\ncontroles de volumen del juego.",
            [34] = "Ajusta el volumen de los efectos de sonido.\nDebes reiniciar el juego despues de salir\nde la configuraciónfor para aplicar los cambios",
            [35] = "Ajusta el volumen de las voces de Don-Chan.\nDebes reiniciar el juego despues de salir\nde la configuraciónfor para aplicar los cambios",
            [36] = "Ajusta el volumen de la música.\nDebes reiniciar el juego despues de salir\nde la configuraciónfor para aplicar los cambios",
            [37] = "La cantidad de volumen que cambia\nal presionar las teclas de control de volumen.\nPuedes especificar desde 1 a 20.",
            [38] = "Tiempo antes de que la música comience. (ms)\n",
            [39] = "Si activas esto, se tomará una captura automaticamente\ncuando obtengas un nuevo récord.",
            [40] = "Comparte la información del .tja que estas\njugando en Discord.",
            [41] = "To select joystick input method.\n\nON to use buffer input. No lost/lags.\nOFF to use realtime input. It may\n causes lost/lags for input.\n Moreover, input frequency is\n synchronized with FPS.",
            [42] = "Actívalo para guardar info. de depuración\nen TJAPlayer3.log cuando cierres el juego.\nAquí se guarda informacion del rendimiento y\neventuales errores del simulador.",
            [43] = "Sound output type:\n" +
                "You can choose WASAPI, ASIO or\n" +
                "DShow(DirectShow).\n" +
                "WASAPI can use only after Vista.\n" +
                "ASIO can use on the\n" +
                "\"ASIO-supported\" sound device.\n" +
                "You should use WASAPI or ASIO\n" +
                "to decrease the sound lag.\n" +
                "\n" +
                "Note: Exit CONFIGURATION to make\n" +
                "     the setting take effect.",
            [44] = "Sound buffer size for WASAPI:\n" +
                "You can set from 0 to 99999ms.\n" +
                "Set 0 to use a default sysytem\n" +
                "buffer size.\n" +
                "Smaller value makes smaller lag,\n" +
                "but it may cause sound troubles.\n" +
                "\n" +
                "Note: Exit CONFIGURATION to make\n" +
                "     the setting take effect.",
            [45] = "ASIO device:\n" +
                    "You can choose the sound device\n" +
                    "used with ASIO.\n" +
                    "\n" +
                    "Note: Exit CONFIGURATION to make\n" +
                "     the setting take effect.",
            [46] = "Use OS Timer or not:\n" +
                "If this settings is ON, DTXMania uses\n" +
                "OS Standard timer. It brings smooth\n" +
                "scroll, but may cause some sound lag.\n" +
                "(so AdjustWaves is also avilable)\n" +
                "\n" +
                "If OFF, DTXMania uses its original\n" +
                "timer and the effect is vice versa.\n" +
                "\n" +
                "This settings is avilable only when\n" +
                "you uses WASAPI/ASIO.\n",
            [47] = "Mostrar imágenes del Personaje.\n",
            [48] = "Mostrar imágenes de Dancer.\n",
            [49] = "Mostrar imágenes de Mob.\n",
            [50] = "Mostrar imágenes de Runner.\n",
            [51] = "Mostrar imagen del Footer.\n",
            [52] = "Usar texturas pre-renderizadas.\n",
            [53] = "Mostrar imágenes del PuchiChara.\n",
            [54] = "Skin:\n" +
                "Cambiar Skin.",
            [55] = "Settings for the system key/pad inputs.",
            [56] = "Juego automático",
            [57] = "Para que el carril de P1\n" +
                "se juegue automaticamente.",
            [58] = "Juego Automático P2",
            [59] = "Para que el carril de P2\n" +
                "se juegue automaticamente.",
            [60] = "Redoble Automático",
            [61] = "Si se desactiva, los redobles\n" +
                    "no se completarán en modo automático.",
            [62] = "VelDesplazamiento",
            [63] = "Cambiar la velocidad de desplazamiento\n" +
                "en el carril de las notas\n" +
                "Puedes ajustarlo desde x0.1 a x200.0.\n" +
                "(ScrollSpeed=x0.5 sería la mitad de velocidad)",
            [64] = "Risky",
            [65] = "Risky mode:\n" +
                "Set over 1, in case you'd like to specify\n" +
                " the number of Poor/Miss times to be\n" +
                " FAILED.\n" +
                "Set 0 to disable Risky mode.",
            [66] = "Random",
            [67] = "Notes come randomly.\n\n Part: swapping lanes randomly for each\n  measures.\n Super: swapping chip randomly\n Hyper: swapping randomly\n  (number of lanes also changes)",
            [68] = "Stealth",
            [69] = "DORON:Hidden for NoteImage.\n" +
                "STEALTH:Hidden for NoteImage and SeNotes",
            [70] = "NoInfo",
            [71] = "Hide the song informations.\n",
            [72] = "JUST",
            [73] = "Allow only GOODs, making OKs becoming\n" +
                    "BADs.",
            [74] = "Tight",
            [75] = "It becomes MISS to hit pad without\n" +
                " chip.",
            [76] = "D-MinCombo",
            [77] = "Initial number to show the combo\n" +
                " for the drums.\n" +
                "You can specify from 1 to 99999.",
            [78] = "InputAdjust",
            [79] = "To adjust the input timing.\n" +
                "You can set from -99 to 99ms.\n" +
                "To decrease input lag, set minus value.",
            [80] = "Dificultad por defecto",
            [81] = "Dificultad seleccionada por defecto.\n",
            [82] = "ModoPuntuación",
            [83] = "Score calculation method\n" +
                    "TYPE-A: Old allotment\n" +
                    "TYPE-B: Old case allotment\n" +
                    "TYPE-C: New allotment\n",
            [84] = "Turn on fixed score mode.",
            [85] = "BranchGuide",
            [86] = "Display the referenced value for branches.\n" +
                    "Not effective with auto.",
            [87] = "BranchAnime",
            [88] = "Branch animation type\n" +
                    "TYPE-A: Taiko 7-14\n" +
                    "TYPE-B: Taiko 15+\n" +
                    " \n",
            [89] = "GameMode",
            [90] = "Game mode:\n" +
                    "(Not avaliable for 2P mode)\n" +
                    "TYPE-A: 完走!叩ききりまショー!\n" +
                    "TYPE-B: 完走!叩ききりまショー!(激辛)\n" +
                    " \n",
            [91] = "BigNotesJudge",
            [92] = "Requerir usar los dos lados para golpear las\nnotas grandes.",
            [93] = "JudgeCountDisp",
            [94] = "Show the JudgeCount\n" +
                "(SinglePlay Only)",
            [95] = "KEY CONFIG",
            [96] = "Settings for the drums key/pad inputs.",
            [97] = "Captura",
            [98] = "Botón para capturar:\nPara asignar una tecla a la captura de pantalla.\n (You can use keyboard only. You can't\nuse pads to capture screenshot.",
            [99] = "Rojo izquierdo",
            [10000] = "Drums key assign:\nTo assign key/pads for LeftRed\n button.",
            [10001] = "Rojo derecho",
            [10002] = "Drums key assign:\nTo assign key/pads for RightRed\n button.",
            [10003] = "Azul izquierdo",
            [10004] = "Drums key assign:\nTo assign key/pads for LeftBlue\n button.",
            [10005] = "Azul derecho",
            [10006] = "Drums key assign:\nTo assign key/pads for RightBlue\n button.",
            [10007] = "LeftRed2P",
            [10008] = "Drums key assign:\nTo assign key/pads for RightCymbal\n button.",
            [10009] = "RightRed2P",
            [10010] = "Drums key assign:\nTo assign key/pads for RightRed2P\n button.",
            [10011] = "LeftBlue2P",
            [10012] = "Drums key assign:\nTo assign key/pads for LeftBlue2P\n button.",
            [10013] = "RightBlue2P",
            [10014] = "Drums key assign:\nTo assign key/pads for RightBlue2P\n button.",
            [10018] = "TimeStretch",
            [10019] = "Pantalla completa",
            [10020] = "StageFailed",
            [10021] = "Usar subcarpetas en random",
            [10022] = "Sincronización vertical",
            [10023] = "AVI",
            [10024] = "BGA",
            [10025] = "PreSoundWait",
            [10026] = "PreImageWait",
            [10027] = "Debug Info",
            [10028] = "BG Alpha",
            [10029] = "BGM Sound",
            [10030] = "Guardar Puntuación",
            [10031] = "Apply Loudness Metadata",
            [10032] = "Target Loudness",
            [10033] = "Usar SONGVOL",
            [10034] = "Volumen de efectos",
            [10035] = "Volumen de voces",
            [10036] = "Volumen de la música",
            [10037] = "Aumento de volumen",
            [10038] = "Retardo de la música",
            [10039] = "Autoguardar Resultado",
            [10040] = "SendDiscordPlayingInformation",
            [10041] = "Entrada con buffer",
            [10042] = "TraceLog",
            [10043] = "SoundType",
            [10044] = "WASAPIBufSize",
            [10045] = "DIspositivo ASIO",
            [10046] = "UseOSTimer",
            [10047] = "Mostrar Personaje",
            [10048] = "Mostrar Dancer",
            [10049] = "Mostrar Mob",
            [10050] = "Mostrar Runner",
            [10051] = "Mostrar Footer",
            [10052] = "Renderizado Rapido",
            [10053] = "Mostrar PuchiChara",
            [10054] = "Skin (Full)",
            [10055] = "Teclas del sistema",
            [10084] = "Modo Shinuchi",

            [100] = "Modo Taiko",
            [101] = "Desafíos del Dojo",
            [102] = "Torres Taiko",
            [103] = "Tienda",
            [104] = "Aventura Taiko",
            [105] = "Mi Habitación",
            [106] = "Ajustes",
            [107] = "Salir",

            [150] = "¡Juega tus canciones\nfavoritas a tu propio gusto!",
            [151] = "¡Juega varias canciones seguidas de\npruebas desafiantes\npara obtener el rango Aprobado!",
            [152] = "¡Juega canciones largas con un\nnumero de vidas limitado y llega\na la punta de la torre!",
            [153] = "¡Compra nuevas canciones, petit-chara o personajes\nusando las medallas que ganaste jugando!",
            [154] = "¡Atraviesa varios obstáculos y\ndesbloquea nuevo contenido!",
            [155] = "¡Cambia la información de tu placa\n o el aspecto de tu personaje!",
            [156] = "¡Cambia tu estilo de juego\n o ajustes generales!",
            [157] = "Salir del juego.\n¡Hasta la próxima!",
            
            [200] = "Regresar",
            [201] = "Canciones jugadas recientemente",
            [202] = "¡Juega las canciones que jugaste recientemente!",

            [1000] = "Piso alcanzado",
            [1001] = "P",
            [1002] = "P",
            [1003] = "Puntuación",
            
            [1010] = "Indicador de almas",
            [1011] = "Cantidad de Perfectas",
            [1012] = "Cantidad de Buenas",
            [1013] = "Cantidad de Malas",
            [1014] = "Puntuación",
            [1015] = "Cantidad de redobles",
            [1016] = "Cantidad de golpes",
            [1017] = "Combo",
            [1018] = "Precisión",

            [1030] = "Regresar",
            [1031] = "Petit-Chara",
            [1032] = "Personaje",
            [1033] = "Título de Dan",
            [1034] = "Título de la Nameplate",
        };
    }
}
