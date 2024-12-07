﻿using Silk.NET.Input;

namespace FDK;

public class CInputJoystick : IInputDevice, IDisposable {
	// Constructor

	public IJoystick Joystick { get; private set; }

	public CInputJoystick(IJoystick joystick) {
		this.Joystick = joystick;
		this.CurrentType = InputDeviceType.Joystick;
		this.GUID = joystick.Index.ToString();
		this.ID = joystick.Index;
		this.Name = joystick.Name;

		this.InputEvents = new List<STInputEvent>(32);

		joystick.ButtonDown += Joystick_ButtonDown;
		joystick.ButtonUp += Joystick_ButtonUp;
	}


	// メソッド

	public void SetID(int nID) {
		this.ID = nID;
	}

	#region [ IInputDevice 実装 ]
	//-----------------
	public InputDeviceType CurrentType {
		get;
		private set;
	}
	public string GUID {
		get;
		private set;
	}
	public int ID {
		get;
		private set;
	}
	public string Name {
		get;
		private set;
	}
	public List<STInputEvent> InputEvents {
		get;
		private set;
	}
	public string strDeviceName {
		get;
		set;
	}

	public void Polling(bool useBufferInput) {
		InputEvents.Clear();

		// BUG: In Silk.NET, GLFW input does not fire events, so we have to poll
		// 			them instead.
		// 			https://github.com/dotnet/Silk.NET/issues/1889
		foreach (var button in Joystick.Buttons) {
			// also, in GLFW the buttons don't have names, so the indices are the names
			ButtonStates[button.Index].isPressed = button.Pressed;
		}

		for (int i = 0; i < ButtonStates.Length; i++) {
			if (ButtonStates[i].isPressed) {
				if (ButtonStates[i].state >= 1) {
					ButtonStates[i].state = 2;
				} else {
					ButtonStates[i].state = 1;

					InputEvents.Add(
						new STInputEvent() {
							nKey = i,
							Pressed = true,
							Released = false,
							nTimeStamp = SoundManager.PlayTimer.SystemTimeMs, // Use the same timer used in gameplay to prevent desyncs between BGM/chart and input.
							nVelocity = 0,
						}
					);
				}
			} else {
				if (ButtonStates[i].state <= -1) {
					ButtonStates[i].state = -2;
				} else {
					ButtonStates[i].state = -1;

					InputEvents.Add(
						new STInputEvent() {
							nKey = i,
							Pressed = false,
							Released = true,
							nTimeStamp = SoundManager.PlayTimer.SystemTimeMs, // Use the same timer used in gameplay to prevent desyncs between BGM/chart and input.
							nVelocity = 0,
						}
					);
				}
			}
		}
	}

	public bool KeyPressed(int nButton) {
		return ButtonStates[nButton].state == 1;
	}
	public bool KeyPressing(int nButton) {
		return ButtonStates[nButton].state >= 1;
	}
	public bool KeyReleased(int nButton) {
		return ButtonStates[nButton].state == -1;
	}
	public bool KeyReleasing(int nButton) {
		return ButtonStates[nButton].state <= -1;
	}
	//-----------------
	#endregion

	#region [ IDisposable 実装 ]
	//-----------------
	public void Dispose() {
		if (!this.IsDisposed) {
			if (this.InputEvents != null) {
				this.InputEvents = null;
			}
			this.IsDisposed = true;
		}
	}
	//-----------------
	#endregion


	// その他

	#region [ private ]
	//-----------------
	public (bool isPressed, int state)[] ButtonStates { get; private set; } = new (bool, int)[18];
	private bool IsDisposed;

	private void Joystick_ButtonDown(IJoystick joystick, Button button) {
		if (button.Name != ButtonName.Unknown) {
			ButtonStates[(int)button.Name].isPressed = true;
		}
	}

	private void Joystick_ButtonUp(IJoystick joystick, Button button) {
		if (button.Name != ButtonName.Unknown) {
			ButtonStates[(int)button.Name].isPressed = false;
		}
	}
	//-----------------
	#endregion
}
