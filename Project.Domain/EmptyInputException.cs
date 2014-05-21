using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain
{
    class EmptyInputException : Exception
    {
        public override string Message
        {
            get
            {
                return "Input string Empty";
            }
        }
    }
}
