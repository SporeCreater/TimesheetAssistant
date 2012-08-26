using System;

namespace Interactors
{
    public class SystemClock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}