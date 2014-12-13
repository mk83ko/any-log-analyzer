using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mkko
{
    public enum CliReturnCodes : int
    {
        ExecutionSuccessful = 0,
        FileNotFound = -1,
        MissingParameters = -2,

        UnhandledException = -1904
    }
}
