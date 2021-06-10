using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.Move
{
    public class MoveCommand : Command
    {
        private MovementCommander commander;

        public MoveCommand()
        {
            Debug.Log("無參建構子");
        }
        public MoveCommand(MovementCommander _commander) : this()
        {
            Debug.Log("Commander inject");
            commander = _commander;
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
