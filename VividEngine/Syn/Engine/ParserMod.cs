﻿using Vivid.Structs;

namespace Vivid
{
    public partial class Parser
    {
        public StructModule ParseModule(ref int i)
        {
            Log("Begun parsing module", i, false);
            //Console.WriteLine("Parsing Module:" + Peek(i + 1).Text);
            var name = Peek(i + 1).Text;

            i = i + 2;

            var mod = new StructModule();
            mod.LineI = Peek(i).LineI;
            mod.LineC = Peek(i).LineC;

            mod.ModuleName = name;
            Log("ModuleName:" + name, i, false);

            for (i = i; i < toks.Len; i++)
            {
                var mtok = toks.Tokes[i];

                switch (mtok.Token)
                {
                    case Token.End:
                        return mod;
                        break;

                    case Token.Func:

                        var func = ParseFunc(ref i);

                        if (func.Static)
                        {
                            mod.StaticFuncs.Add(func);
                        }
                        else
                        {
                            mod.Methods.Add(func);
                        }

                        break;

                    case Token.Var:
                        i++;
                        while (true)
                        {
                            var t = Get(i);
                            if (t.Text == ",")
                            {
                                i++;
                                continue;
                            }
                            if (t.Text == ";")
                            {
                                break;
                            }
                            var nv = new Var();
                            nv.Name = t.Text;
                            nv.Value = 0;
                            mod.Vars.Add(nv);
                            i++;
                            if (Get(i).Text == "=")
                            {
                                i++;
                                nv.Init = ParseExp(ref i);
                            }
                        }

                        break;
                }
            }

            return null;
        }
    }
}