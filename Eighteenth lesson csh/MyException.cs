using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eighteenth_lesson_csh
{
    public class MyException : Exception
    {
        public MyException(string massage) : base(massage) { }
    }
}
