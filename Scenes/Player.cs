using Godot;
using Godot.Collections;
using System;

public class Player : KinematicBody2D {
    
    [Export]
    public int gridSize = 16;

    private Dictionary<string, Vector2> inputs = new Dictionary<string, Vector2>();
    private RayCast2D rayCast2D;
    private Main main;

    public override void _Ready() {
        inputs.Add("ui_up", Vector2.Up);
        inputs.Add("ui_down", Vector2.Down);
        inputs.Add("ui_left", Vector2.Left);
        inputs.Add("ui_right", Vector2.Right);
        rayCast2D = GetNode<RayCast2D>("RayCast2D");
        main = GetParent() as Main;
    }

    public override void _UnhandledInput(InputEvent @event) {
        foreach (var entry in inputs) {
            if (@event.IsActionPressed(entry.Key)) {
                Move(entry.Key);
            }
            if(@event.IsActionPressed("reset")) {
                GetTree().ReloadCurrentScene();
            }
        }
    }

    public void Move(string direction) {
        var vectorPosition = inputs[direction] * gridSize;
        rayCast2D.CastTo = vectorPosition;
        rayCast2D.ForceRaycastUpdate();
        if(!rayCast2D.IsColliding()) {
            Position += vectorPosition;
            main.moves += 1;
        } else {
            Godot.Object collider = rayCast2D.GetCollider();
            if(collider.GetType().ToString() == "Box") {
                Box boxCollider = collider as Box;
                if(boxCollider.IsInGroup("boxes")) {
                    if(boxCollider.Move(direction)) {
                        Position += vectorPosition;
                        main.moves += 1;
                    }
                }
            }
        }
    }
}
