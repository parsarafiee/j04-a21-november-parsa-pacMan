using System;
using System.Collections.Generic;
using System.Threading;

namespace fsm
{
    public delegate bool Condition();
    public delegate void Action();

    public class State
    {
        public string Label { get; set; }
        public Action Action { get; set; }

        public State(string label, Action action)
        {
            this.Label = label;
            this.Action = action;
        }
    }

    internal class Transition
    {
        public State Source { get; set; }
        public State Destination { get; set; }

        public Condition ConditionFunction { get; set; }
    }

    public class FSM
    {
        public string Name { get; set; }
        private List<State> States { get; set; }
        private State InitalState { get; set; }
        private List<State> FinalStates { get; set; }
        private State CurrentState { get; set; }
        private List<Transition> Transitions { get; set; }

        public FSM(string name)
        {
            this.Name = name;
            this.States = new List<State>();
            this.FinalStates = new List<State>();
            this.Transitions = new List<Transition>();
        }
        public void AddState(State state)
        {
            this.States.Add(state);
        }
        public void SetInitialState(State state)
        {
            if (this.States.Contains(state))
            {
                this.InitalState = state;
            }
            else
            {
                throw new Exception("Initial state must be added to FSM first.");
            }
        }
        public void AddFinalState(State state)
        {
            if (this.States.Contains(state))
            {
                this.FinalStates.Add(state);
            }
            else
            {
                throw new Exception("Final state must be added to FSM first.");
            }
        }
        public void AddTransition(State source, State destination, Condition conditionFunction)
        {
            if (this.States.Contains(source) && this.States.Contains(destination))
            {
                this.Transitions.Add(new Transition
                {
                    Source = source,
                    Destination = destination,
                    ConditionFunction = conditionFunction
                });
            }
            else
            {
                throw new Exception("Transition source and destination states must be added to FSM first.");
            }

        }
        public void Start()
        {
            if (this.InitalState != null)
            {
                this.CurrentState = this.InitalState;
            }
            else
            {
                throw new Exception("Unable to start FSM. Need an initial state first.");
            }
        }
        public void Process()
        {
            if (this.CurrentState == null)
            {
                throw new Exception("Unable to process FSM. Start FSM first.");
            }

            //  do nothing if current state is a final state
            if (this.IsOnFinalState())
            {
                return;
            }

            //  do the action for the current state
            this.CurrentState.Action?.Invoke();

            //  try to use a transition to another state
            foreach (Transition transition in this.Transitions)
            {
                if (transition.Source == this.CurrentState && transition.ConditionFunction.Invoke())
                {
                    this.CurrentState = transition.Destination;
                    break;
                }
            }
        }
        public bool IsOnFinalState()
        {
            return this.FinalStates.Contains(this.CurrentState);
        }
        public State GetCurrentState()
        {
            return this.CurrentState;
        }
    }
}


//What we need :
//	-add a state
//	- set state as initial state
//	- set some states as final states
//	- add a transition between 2 states
//		=> how to model the trigger?
//	- start the automaton
//	- go to next state (or stay on current state)
//	-get current state
//	- get if current state is a final state
//	- reset the automaton to inital state

/*  TO DO
 *  
 *  - remove the use of State class ouside the FSM
 *  - use label to identify a state from outside
 *  - make State class internal
 *
 */
