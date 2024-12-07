﻿using Silk.NET.Input;

namespace FDK;

public abstract class CInputButtonsBase : IInputDevice, IDisposable {
	// Constructor

	public CInputButtonsBase(int nButtonStates) {
		this.ButtonStates = new (bool, int)[nButtonStates];
		this.EventBuffer = new List<STInputEvent>(nButtonStates);
		this.InputEvents = [];
	}


	// メソッド

	public void SetID(int nID) => this.ID = nID;

	#region [ IInputDevice 実装 ]
	//-----------------
	public InputDeviceType CurrentType { get; protected set; }
	public string GUID { get; protected set; }
	public int ID { get; protected set; }
	public string Name { get; protected set; }
	public List<STInputEvent> InputEvents { get; protected set; }
	public string strDeviceName { get; set; }
	public bool useBufferInput { get; set; }

	public void Polling() {
		// clear previous input buffer
		InputEvents.Clear();
		// update per-frame button state, also fill the new input buffer for non-buffered input
		// for buffered input, the input buffer has already been filled.
		for (int i = 0; i < ButtonStates.Length; i++) {
			this.ProcessButtonState(i);
		}
		// swap input buffer
		(this.InputEvents, this.EventBuffer) = (this.EventBuffer, this.InputEvents);
	}

	protected void ProcessButtonState(int idxBtn) {
		if (ButtonStates[idxBtn].isPressed) {
			if (ButtonStates[idxBtn].state >= 1) {
				ButtonStates[idxBtn].state = 2;
			} else {
				ButtonStates[idxBtn].state = 1;
				if (!this.useBufferInput) {
					this.AddPressedEvent(idxBtn);
				}
			}
		} else {
			if (ButtonStates[idxBtn].state <= -1) {
				ButtonStates[idxBtn].state = -2;
			} else {
				ButtonStates[idxBtn].state = -1;
				if (!this.useBufferInput) {
					this.AddReleasedEvent(idxBtn);
				}
			}
		}
	}

	protected void AddReleasedEvent(int idxBtn)
		=> this.EventBuffer.Add(new STInputEvent() {
			nKey = idxBtn,
			Pressed = false,
			Released = true,
			nTimeStamp = SoundManager.PlayTimer.SystemTimeMs, // Use the same timer used in gameplay to prevent desyncs between BGM/chart and input.
			nVelocity = 0,
		});

	protected void AddPressedEvent(int idxBtn)
		=> this.EventBuffer.Add(new STInputEvent() {
			nKey = idxBtn,
			Pressed = true,
			Released = false,
			nTimeStamp = SoundManager.PlayTimer.SystemTimeMs, // Use the same timer used in gameplay to prevent desyncs between BGM/chart and input.
			nVelocity = 0,
		});

	protected void ButtonDown(int idxBtn) {
		if (this.useBufferInput && !this.ButtonStates[idxBtn].isPressed) {
			this.AddPressedEvent(idxBtn);
		}
		this.ButtonStates[idxBtn].isPressed = true;
	}

	protected void ButtonUp(int idxBtn) {
		if (this.useBufferInput && this.ButtonStates[idxBtn].isPressed) {
			this.AddReleasedEvent(idxBtn);
		}
		this.ButtonStates[idxBtn].isPressed = false;
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
			this.InputEvents.Clear();
			this.EventBuffer.Clear();
			this.IsDisposed = true;
		}
	}
	//-----------------
	#endregion


	// その他

	#region [ private ]
	//-----------------
	public List<STInputEvent> EventBuffer;
	public (bool isPressed, int state)[] ButtonStates { get; protected set; }
	private bool IsDisposed;
	//-----------------
	#endregion
}
