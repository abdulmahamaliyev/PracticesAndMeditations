using System;
using System.Collections.Generic;
using static System.Console;
namespace StateMachine
{
    public enum State
    {
        Sleeping,
        Working,
        Commuting,
        Cooking,
        Relaxing,
        AtHome,

    }

    public enum Trigger
    {
        WorkHasFinished,
        WorkIsStarting,
        Hungry,
        Tired,
    }

    class Program
    {
        private static Dictionary<State, List<(Trigger, State)>> rules =
            new Dictionary<State, List<(Trigger, State)>>
            {
                [State.Sleeping] = new List<(Trigger, State)>
                {
                    (Trigger.WorkIsStarting, State.Commuting),
                    (Trigger.Hungry, State.Cooking)
                },
                [State.Commuting] = new List<(Trigger, State)>
                {
                    (Trigger.Tired, State.Sleeping),
                    (Trigger.WorkIsStarting, State.Working),
                    (Trigger.WorkHasFinished, State.AtHome)
                },
                [State.AtHome] = new List<(Trigger, State)>
                {
                    (Trigger.Hungry, State.Cooking),
                    (Trigger.Tired, State.Sleeping),
                    (Trigger.WorkHasFinished, State.Relaxing)
                },
                [State.Cooking] = new List<(Trigger, State)>
                {
                    (Trigger.Tired, State.Relaxing)
                },
                [State.Working] = new List<(Trigger, State)>
                {
                    (Trigger.WorkHasFinished, State.Commuting),
                    (Trigger.Tired, State.Relaxing),
                },
                [State.Relaxing] = new List<(Trigger, State)>
                {
                    (Trigger.WorkIsStarting, State.Working)
                }
            };


        static void Main(string[] args)
        {
            var state = State.AtHome;

            while(true)
            {
                WriteLine($"I'm currently {state}. ");
                WriteLine("Select a trigger: ");

                for (int i = 0; i < rules[state].Count; i++)
                {
                    var (t, _) = rules[state][i];
                    WriteLine($"{i}, {t}");

                }

                int input = int.Parse(ReadLine());

                var (_, s) = rules[state][input];
                state = s;
            }
        }
    }
}
