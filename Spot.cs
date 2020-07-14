using Godot;
using System;

public class Spot : Area2D {

    public bool occupied = false;

    public void _on_Spot_body_entered(Node body) {
        if(body.IsInGroup("boxes")) {
            occupied = true;
        }
    }

    public void _on_Spot_body_exited(Node body) {
        if(body.IsInGroup("boxes")) {
            occupied = false;
        }
    }
}
    
