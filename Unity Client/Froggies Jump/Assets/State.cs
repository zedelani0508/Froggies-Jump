// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.25
// 

using Colyseus.Schema;

public partial class State : Schema {
	[Type(0, "map", typeof(MapSchema<Entity>))]
	public MapSchema<Entity> entities = new MapSchema<Entity>();
}

