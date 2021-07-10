import { Room, Client, generateId, Delayed } from "colyseus";
import { Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import { verifyToken, User, IUser } from "@colyseus/social";

// Create a context for this room's state data.
const type = Context.create();

class Entity extends Schema {
  @type("number") velX: number = 0;
  @type("number") velY: number = 0;
  @type("number") velZ: number = 0;
  @type("number") posX: number = 0;
  @type("number") posY: number = 0;
  @type("number") posZ: number = 0;
  @type("string") username: string = "player";
  @type("string") sessionId: string;
}

class Enemy extends Entity {
  @type("number") power: number = Math.random() * 10;
}

class State extends Schema {
  @type({ map: Entity }) entities = new MapSchema<Entity>();
}

/**
 * Demonstrate sending schema data types as messages
 */
class Message extends Schema {
  @type("number") num;
  @type("string") str;
}

export class DemoRoom extends Room {
  onCreate (options: any) {
    console.log("DemoRoom created.", options);

    this.setState(new State());

    this.setMetadata({
      str: "hello",
      number: 10
    });

    this.setPatchRate(1000 / 20);
    this.setSimulationInterval((dt) => this.update(dt));

    this.onMessage(0, (client, message) => {
      client.send(0, message);
    });

    this.onMessage("schema", (client) => {
      const message = new Message();
      message.num = Math.floor(Math.random() * 100);
      message.str = "sending to a single client";
      client.send(message);
    })

    this.onMessage("move", (client, data) => {
      this.state.entities[client.sessionId].velX = data.velX;
      this.state.entities[client.sessionId].velY = data.velY;
      this.state.entities[client.sessionId].posX = data.posX;
      this.state.entities[client.sessionId].posY = data.posY;
    });

    this.onMessage("username", (client, data) => {
      this.state.entities[client.sessionId].username = data.username;
    });

    this.onMessage("move_pos", (client, data) => {
      this.state.entities[client.sessionId].posX = data.posX;
      this.state.entities[client.sessionId].posY = data.posY;
    });

    this.onMessage("move_to_waiting_room", (client, data) => {
      this.state.entities[client.sessionId].posX = data.posX;
      this.state.entities[client.sessionId].posY = data.posY;

      this.broadcast("win", {
        playername : this.state.entities[client.sessionId].username,
      })
    });

    this.onMessage("move_jump", (client, data) => {
      this.state.entities[client.sessionId].velY = data.velY;
    });

    this.onMessage("*", (client, type, message) => {
      console.log(`received message "${type}" from ${client.sessionId}:`, message);
    });
  }

  async onAuth (client, options) {
    console.log("onAuth(), options!", options);
    return await User.findById(verifyToken(options.token)._id);
  }

  populateEnemies () {
    for (let i=0; i<=2; i++) {
      const enemy = new Enemy();
      // enemy.x = Math.random() * 2;
      // enemy.y = Math.random() * 2;
      this.state.entities[generateId()] = enemy;
    }
  }

  onJoin (client: Client, options: any, user: IUser) {
    console.log("client joined!", client.sessionId);
    this.state.entities[client.sessionId] = new Entity();
    this.state.entities[client.sessionId].sessionId = client.sessionId;

    client.send("type", { sessionId: client.sessionId });
  }

  async onLeave (client: Client, consented: boolean) {
    this.state.entities[client.sessionId].connected = false;

    try {
      if (consented) {
        throw new Error("consented leave!");
      }

      console.log("let's wait for reconnection!")
      const newClient = await this.allowReconnection(client, 10);
      console.log("reconnected!", newClient.sessionId);

    } catch (e) {
      console.log("disconnected!", client.sessionId);
      delete this.state.entities[client.sessionId];
    }
  }


  update (dt?: number) {
    // console.log("num clients:", Object.keys(this.clients).length);
  }

  onDispose () {
    console.log("DemoRoom disposed.");
  }

}
