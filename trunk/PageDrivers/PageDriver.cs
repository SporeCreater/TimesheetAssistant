using System.Collections.Generic;

namespace PageDrivers
{
    public abstract class PageDriver
    {
        protected readonly List<WatinControlDriver> _controls = new List<WatinControlDriver>();

        public void Register(WatinControlDriver control)
        {
            _controls.Add(control);
        }

        public string Verify()
        {
            string result = string.Empty;

            foreach (var ctrl in _controls)
            {
                if (!ctrl.Verify())
                {
                    result += ctrl.Id + " ";
                }
            }

            return result;
        }
    }
}