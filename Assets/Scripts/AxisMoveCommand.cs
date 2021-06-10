using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.Move
{
    public class AxisMoveCommand : Command
    {
        private MovementCommander commander;

        public AxisMoveCommand()
        {
            Debug.Log("無參建構子");
        }
        public AxisMoveCommand(MovementCommander _commander) : this()
        {
            Debug.Log("Commander inject");
            commander = _commander;
        }

        public override void Execute()
        {
            Debug.Log("empty function.");
            throw new System.NotImplementedException();
        }

        public override void Execute(object obj)
        {
            commander.AxisMoveExecute(obj);
        }
    }
}
