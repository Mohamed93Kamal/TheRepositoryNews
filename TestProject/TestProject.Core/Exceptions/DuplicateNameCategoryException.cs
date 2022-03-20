using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Exceptions
{
    public class DuplicateNameCategoryException : Exception
    {
        public DuplicateNameCategoryException():base("Duplicate Name Category")
        {

        }
    }
}
