// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 2.0.29
// 

using Colyseus.Schema;
using Action = System.Action;

public partial class GameRoomState : Schema {
	[Type(0, "map", typeof(MapSchema<PlayerSchema>))]
	public MapSchema<PlayerSchema> players = new MapSchema<PlayerSchema>();

	[Type(1, "map", typeof(MapSchema<PlayerSchema>))]
	public MapSchema<PlayerSchema> results = new MapSchema<PlayerSchema>();

	[Type(2, "map", typeof(MapSchema<AppleSchema>))]
	public MapSchema<AppleSchema> apples = new MapSchema<AppleSchema>();

	[Type(3, "ref", typeof(LefttimeSchema))]
	public LefttimeSchema leftTime = new LefttimeSchema();

	/*
	 * Support for individual property change callbacks below...
	 */

	protected event PropertyChangeHandler<MapSchema<PlayerSchema>> __playersChange;
	public Action OnPlayersChange(PropertyChangeHandler<MapSchema<PlayerSchema>> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.players));
		__playersChange += __handler;
		if (__immediate && this.players != null) { __handler(this.players, null); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(players));
			__playersChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<MapSchema<PlayerSchema>> __resultsChange;
	public Action OnResultsChange(PropertyChangeHandler<MapSchema<PlayerSchema>> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.results));
		__resultsChange += __handler;
		if (__immediate && this.results != null) { __handler(this.results, null); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(results));
			__resultsChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<MapSchema<AppleSchema>> __applesChange;
	public Action OnApplesChange(PropertyChangeHandler<MapSchema<AppleSchema>> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.apples));
		__applesChange += __handler;
		if (__immediate && this.apples != null) { __handler(this.apples, null); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(apples));
			__applesChange -= __handler;
		};
	}

	protected event PropertyChangeHandler<LefttimeSchema> __leftTimeChange;
	public Action OnLeftTimeChange(PropertyChangeHandler<LefttimeSchema> __handler, bool __immediate = true) {
		if (__callbacks == null) { __callbacks = new SchemaCallbacks(); }
		__callbacks.AddPropertyCallback(nameof(this.leftTime));
		__leftTimeChange += __handler;
		if (__immediate && this.leftTime != null) { __handler(this.leftTime, null); }
		return () => {
			__callbacks.RemovePropertyCallback(nameof(leftTime));
			__leftTimeChange -= __handler;
		};
	}

	protected override void TriggerFieldChange(DataChange change) {
		switch (change.Field) {
			case nameof(players): __playersChange?.Invoke((MapSchema<PlayerSchema>) change.Value, (MapSchema<PlayerSchema>) change.PreviousValue); break;
			case nameof(results): __resultsChange?.Invoke((MapSchema<PlayerSchema>) change.Value, (MapSchema<PlayerSchema>) change.PreviousValue); break;
			case nameof(apples): __applesChange?.Invoke((MapSchema<AppleSchema>) change.Value, (MapSchema<AppleSchema>) change.PreviousValue); break;
			case nameof(leftTime): __leftTimeChange?.Invoke((LefttimeSchema) change.Value, (LefttimeSchema) change.PreviousValue); break;
			default: break;
		}
	}
}

