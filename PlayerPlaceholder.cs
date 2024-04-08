using Godot;
using System;

public partial class PlayerPlaceholder : CharacterBody2D

{
	private AnimatedSprite2D animatedSprite2D;

    [Export]
    public int Speed { get; set; } = 100;

	[Export]
	public float DashConstant {get ; set; } = 2;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("Idle");
    }

    public void GetInput()
    {

        Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
        Velocity = inputDirection * 0;

		if(Input.IsActionPressed("Defend")){ //defend has prio over all other actions
			animatedSprite2D.Play("Defend");
		}
		else if(Input.IsActionPressed("Attack")){
			animatedSprite2D.Play("Attack");
		}
		else if(Input.IsActionPressed("Interact")){
			animatedSprite2D.Play("Interact");
		}
		else{
			 //only move if the character is not attacking/defending/interacting
			if(Input.IsActionPressed("Up")){
				animatedSprite2D.Play("MoveUp");
			}
			else if (Input.IsActionPressed("Down")){
				animatedSprite2D.Play("MoveDown");
			}
			else if (Input.IsActionPressed("Left")){
				animatedSprite2D.Play("MoveLeft");
			}
			else if (Input.IsActionPressed("Right")){
				animatedSprite2D.Play("MoveRight");
			}

			if(Input.IsActionPressed("Dash")){
				Velocity = inputDirection * Speed * DashConstant;
			}
			else{
				Velocity = inputDirection * Speed;
			}
		}


    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
    }
}
