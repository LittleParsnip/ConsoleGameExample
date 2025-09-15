using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Event
{
    public class UpdateEventArgs : EventArgs
    {
        public TimeSpan DeltaTime { get; }
        public float DeltaSeconds { get; }

        public UpdateEventArgs(TimeSpan deltaTime)
        {
            DeltaTime = deltaTime;
            DeltaSeconds = (float)deltaTime.TotalMilliseconds / 1000.0f;
        }
    }
}
