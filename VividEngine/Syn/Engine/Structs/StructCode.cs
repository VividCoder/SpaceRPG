using System.Collections.Generic;

namespace Vivid.Structs
{
    public class StructCode : Struct
    {
        public List<Struct> Lines = new List<Struct>();
        public List<StructSub> Subs = new List<StructSub>();
        public long LineNum = 0;

        public override string DebugString()
        {
            return ("CodeBody.");
        }

        public dynamic ExecNext()
        {
            var l = Lines[(int)LineNum];

            LineNum++;
            if (LineNum >= Lines.Count)
            {
                LineNum = 0;
            }

            if (l is StructReturn)
            {
                var sr = l as StructReturn;
                if (sr.ReturnExp != null)
                {
                    return sr.ReturnExp.Exec();
                }
                return null;
            }
            l.Exec();

            return null;
        }

        public override dynamic Exec()
        {
            foreach (Struct l in Lines)
            {
                if (l is StructReturn)
                {
                    var sr = l as StructReturn;
                    if (sr.ReturnExp != null)
                    {
                        return sr.ReturnExp.Exec();
                    }
                    return null;
                }
                if(l is StructBreak)
                {

                    return l;

                }
                if(l is StructStop)
                {
                    //SynWave.SynHost.Active.DebugVars();
                    int stopped = 1;

                }
                var rc = l.Exec();
                if (rc != null)
                {
                    return rc;
                }
                if(rc is StructBreak)
                {
                    return rc;
                }
            }
            return null;
        }
    }
}