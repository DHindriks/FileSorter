using System;
using System.Collections.Generic;
using System.Text;

public interface ISorter
{
    void Sort(Action<string> reportStatus);
}
