// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.25
// 

using Colyseus.Schema;

public partial class Entity : Schema {
	[Type(0, "number")]
	public float velX = default(float);

	[Type(1, "number")]
	public float velY = default(float);

	[Type(2, "number")]
	public float velZ = default(float);

	[Type(3, "number")]
	public float posX = default(float);

	[Type(4, "number")]
	public float posY = default(float);

	[Type(5, "number")]
	public float posZ = default(float);

	[Type(6, "string")]
	public string username = default(string);

	[Type(7, "string")]
	public string sessionId = default(string);
}

