using Godot;
using System;

public class Main : Node2D {

    public int spotsCount = 0;
    public int moves = 0;
    public bool gameEnded = false;

    public override void _Process(float delta) {
        GetNode<Label>("MovesLabel").Text = "Moves: " + moves.ToString();

        if(!gameEnded) {
            spotsCount = GetNode("Spots").GetChildCount();
            foreach (Spot i in GetNode("Spots").GetChildren()) {
                if(i.occupied) {
                    spotsCount -= 1;
                }
            }

            if(spotsCount == 0) {
                GetNode<AcceptDialog>("AcceptDialog").Popup_();
                gameEnded = true;
            }
        }
    }

    public void _on_AcceptDialog_confirmed() {
        GetTree().ReloadCurrentScene();
    }
}
