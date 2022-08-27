using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class Enum
    {
        public enum Mode
        {
            Asynchronous = 1,
            Synchronous = 0
        }

        public enum Stage
        {
            PreValidation = 10,
            PreOperation = 20,
            PostOperation = 40
        }
    }
}
