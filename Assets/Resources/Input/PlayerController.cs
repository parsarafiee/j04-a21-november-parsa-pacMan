using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using fsm;


enum Direction { Right, Left, Up, Down }
public class PlayerController : MonoBehaviour
{

    private IEnumerator coroutine;

    private PlayerMovement controls;
    FSM fsm = new FSM("Animation");


    static Direction direction = Direction.Right;



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
        coroutine = WaitAndPrint(0.1f);
        StartCoroutine(coroutine);


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

    private IEnumerator WaitAndPrint(float waitTime)
    {
        controls.Main.Movement.performed += ctx => Helper.Move(this.transform, ctx.ReadValue<Vector2>());

        yield return new WaitForSeconds(waitTime);
    }
    void Update()
    {



        if (controls.Main.Movement.ReadValue<Vector2>() == Vector2.down)
        {
            direction = Direction.Down;

        }

        if (controls.Main.Movement.ReadValue<Vector2>() == Vector2.right)
        {
            direction = Direction.Right;
        }

        if (controls.Main.Movement.ReadValue<Vector2>() == Vector2.left)
        {
            direction = Direction.Left;
        }

        if (controls.Main.Movement.ReadValue<Vector2>() == Vector2.up)
        {
            direction = Direction.Up;


        }
        fsm.Process();

    }
}
