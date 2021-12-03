using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using fsm;

 enum Direction { Right, Left, Up, Down }
public class PlayerController : MonoBehaviour
{

    private IEnumerator coroutine;

    public float speed =0.1f;

    public Vector2 Vector2;
    private PlayerMovement controls;
    FSM fsm = new FSM("Animation");


    static Direction direction = Direction.Right;

    float timer=0;


    public void MoveRight()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    public void MoveLeft()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, 180);
    }
    public void MoveDown()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, -90);
    }
    public void MoveUp()
    {
        this.transform.localEulerAngles = new Vector3(0, 0, 90);
    }
    public bool MovementRightBool()
    {
        return direction == Direction.Right;

    }
    public bool MovementDownBool()
    {
        return direction == Direction.Down;

    }
    public bool MovementLeftBool()
    {
        return direction == Direction.Left;

    }
    public bool MovementUpBool()
    {
        return direction == Direction.Up;

    }


    void Awake()
    {
        controls = new PlayerMovement();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {

        



        State right = new State("right", MoveRight);
        fsm.AddState(right);
        fsm.SetInitialState(right);

        State down = new State("down", MoveDown);
        fsm.AddState(down);
        State left = new State("left", MoveLeft);
        fsm.AddState(left);

        State up = new State("up", MoveUp);
        fsm.AddState(up);



        fsm.AddTransition(left, right, this.MovementRightBool);
        fsm.AddTransition(up, right, this.MovementRightBool);
        fsm.AddTransition(down, right, this.MovementRightBool);


        fsm.AddTransition(right, left, this.MovementLeftBool);
        fsm.AddTransition(down, left, this.MovementLeftBool);
        fsm.AddTransition(up, left, this.MovementLeftBool);


        fsm.AddTransition(left, up, this.MovementUpBool);
        fsm.AddTransition(right, up, this.MovementUpBool);
        fsm.AddTransition(down, up, this.MovementUpBool);


        fsm.AddTransition(left, down, this.MovementDownBool);
        fsm.AddTransition(up, down, this.MovementDownBool);
        fsm.AddTransition(right, down, this.MovementDownBool);


        fsm.Start();
    }

    void Update()
    {

         MovementPlayerOnDirection(Vector2.right, Direction.Right);
         MovementPlayerOnDirection(Vector2.left, Direction.Left);
         MovementPlayerOnDirection(Vector2.up, Direction.Up);
         MovementPlayerOnDirection(Vector2.down, Direction.Down);




        fsm.Process();

    }

    void MovementPlayerOnDirection(Vector2 vector,Direction dir)
    {
        if (controls.Main.Movement.ReadValue<Vector2>() == vector)
        {
            direction = dir;
            if (Time.time > timer)
            {
                Helper.Move(this.transform, controls.Main.Movement.ReadValue<Vector2>());
                timer = speed + Time.time;
            }
        }

    }
}
