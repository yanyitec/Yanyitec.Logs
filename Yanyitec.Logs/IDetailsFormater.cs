using System;
using System.Collections.Generic;
using System.Text;

namespace Yanyitec.Logs
{
    public interface IDetailsFormater
    {
        string Format(object details);
    }
}
