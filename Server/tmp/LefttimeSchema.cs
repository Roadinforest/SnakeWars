// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class LefttimeSchema : Schema {
	[Type(0, "number")]
	public float leftTime = default(float);

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<float> __leftTimeChange;
	public Action OnLeftTimeChange(PropertyChangeHandler<float> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.leftTime));
		__leftTimeChange += __handler;
		if (__immediate && this.leftTime != default(float)) { __handler(this.leftTime, default(float)); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(leftTime));
			__leftTimeChange -= __handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(leftTime): __leftTimeChange?.Invoke((float) change.Value, (float) change.PreviousValue); break;
			default: break;
		}
	}
}

