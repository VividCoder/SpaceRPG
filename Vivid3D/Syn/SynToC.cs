using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid.Structs;
using csscript;
using CSScriptEvaluatorApi;
namespace Vivid.SynWave
{
    public class SynCGame
    {

        public dynamic Link = null;

    }
    public class SynToC
    {

        private FileStream fs;
        private TextWriter tw;
        public int Tab = 0;
        public List<string> osfuncs = new List<string>();
        public SynToC(SynGame game,string outPath="sync/test.cs")
        {

            osfuncs.Add("getTicks");
            osfuncs.Add("rnd");
            osfuncs.Add("rndInt");
            osfuncs.Add("drawImg");
            osfuncs.Add("drawImgCentered");
            osfuncs.Add("drawLine");
            osfuncs.Add("appWidth");
            osfuncs.Add("appHeight");
            

            WriteFile(outPath);

            CompileGame(game);

            EndWrite();

            string cscode = File.ReadAllText(outPath);

            dynamic cs = CSScriptLibrary.CSScript.Evaluator.LoadCode(cscode);




        }

        public void CompileGame(SynGame game)
        {
            Tab = 0;
            WriteHeader();

            WriteGameHost(game);

            Tab = 1;

            foreach (var state in game.Game.States)
            {

                WriteState(state);

            }

            foreach (var m2 in game.Mods)
            {

                foreach (var mod in m2.SubModules)
                {
                    WriteModule(mod);
                }
            }

            Tab = 0;

            WriteFooter();

        }

        private void WriteModule(SynModule mod)
        {
            Tab = 1;
            WriteClassHeader(mod.DirectModule.ModuleName);

            ClassVars.Clear();

            foreach (var v in mod.DirectModule.Vars)
            {

               // WriteVar(v);
                Console.WriteLine("Var:" + v.Name);
                ClassVars.Add(v.Name);
            }

            foreach (var v in mod.DirectModule.StaticVars)
            {

               // WriteStaticVar(v);
                Console.WriteLine("Var:" + v.Name);
                ClassVars.Add(v.Name);

            }




            foreach (var meth in mod.DirectModule.Methods)
            {
                AddVars(meth.Code);
            }

            foreach (var func in mod.DirectModule.StaticFuncs)
            {
                AddVars(func.Code);
            }
            foreach (var v in Vars)
            {
                bool add = true;
                foreach (var cv in ClassVars)
                {
                    if (cv == v)
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    ClassVars.Add(v);
                }
            }

            foreach(var cv in ClassVars)
            {
                Tab = 2;
                Write("public dynamic " + cv + " = null;");

            }
        

            Vars.Clear();


            foreach (var meth in mod.DirectModule.Methods)
            {
                if (meth.FuncName == mod.DirectModule.ModuleName)
                {
                    Tab = 3;
                    WriteNewFunc(meth);
                }
                else
                {
                    Tab = 3;
                    WriteFunc(meth);
                }
            }
            Tab = 2;
            WriteClassFooter();
        }
        public void WriteNewFunc(StructFunc func)
        {
            C("public ");
            if (func.Static)
            {
                C("static ");
            }
            C(func.FuncName + "(");
            WritePars(func.Pars);
            Write(")");
            Write("{");
            WriteCode(func.Code);
          
            Write("}");
        }
        public void WriteFunc(StructFunc func)
        {
            Console.WriteLine("WriteFunc:" + func.Name + "!");
            C("public ");
            if (func.Static)
            {
                C("static ");
            }
            C("dynamic "+func.FuncName + "(");
            WritePars(func.Pars);
            Write(")");
            Write("{");
            if(func.FuncName == "Get")
            {
                int v = 5;
            }
            WriteCode(func.Code);
            Write("return null;");
            Write("}");


        }

        public void WritePars(StructParameters pars)
        {

            int pn = 0;
            foreach(var par in pars.Pars)
            {

                C("dynamic " + par.Name);
                if(par.Init!=null)
                {
                    C(" = ");
                    WriteExpr(par.Init);
                }
                pn++;
                if (pn < pars.Pars.Count)
                {
                    C(",");
                }
            }

        }

        public void WriteStaticVar(Structs.Var v)
        {
            C("public dynamic " + v.Name);
            if (v.Init != null)
            {
                C(" = ");
                WriteExpr(v.Init);
                C(";");
            }
            Write(";");
        }

        public void WriteVar(Structs.Var v)
        {

            
            C("public dynamic " + v.Name);
            if(v.Init != null)
            {
                C(" = ");
                WriteExpr(v.Init);
                C(";");
            }
            Write(";");

        }

        private void WriteGameHost(SynGame game)
        {
            WriteClassHeader("SynGameHost", "SynGameHostC");


            foreach (var state in game.Game.States)
            {

                WriteStateVar(state);

            }

         

            WriteGameNew(game.Game);

            WriteClassFooter();
        }

        public void WriteGameNew(Structs.Game.StructGame game)
        {
            Write("public SynGameHost()");
            Write("{");
            Write("InitState=new " + game.InitState.Name + "();");
            Write("Run();");

            Write("}");
        }

        public void WriteStateVar(Structs.Game.StructGameState state)
        {
            C(" public " + state.Name + " s_" + state.Name + ";");
            Write("");
        }

        public List<string> Vars = new List<string>();

        public void AddVars(StructCode code)
        {
            if (code == null) return;
            foreach(var l in code.Lines)
            {
                if(l is StructForEach)
                {

                    var sfe = l as StructForEach;
                    AddVars(sfe.Code);

                }
                if(l is StructAssign)
                {
                    var sa = l as StructAssign;
                    Vars.Add(sa.Vars[0].Name);
                }
                if(l is StructWhile)
                {
                    var sw = l as StructWhile;
                    AddVars(sw.Code);
                }
                if(l is StructIf)
                {
                    var sif = l as StructIf;
                    AddVars(sif.TrueCode);
                    AddVars(sif.ElseCode);
                    foreach(var eif in sif.ElseIfCode)
                    {
                        AddVars(eif);
                    }
                }
            }
        }

        public void WriteVars()

        {

            var nvars = new List<string>();
            int s1 = 0;
            int s2 = 0;
            foreach(var v2 in Vars)
            {
                bool skip = false;
                s2 = 0;
                foreach(var v3 in Vars)
                {
                    if(v3==v2 && s1 != s2)
                    {
                        skip = true;
                    }
                    s2++;
                }
                if (!skip) {
                    nvars.Add(v2);
                }
                s1++;
            }
            Vars = nvars;

            foreach(var v in Vars)
            {
                Write(" public dynamic " + v + ";");
            }
            Vars.Clear();
        }

        public void WriteState(Structs.Game.StructGameState state)
        {

            WriteClassHeader(state.Name, "SynCState");
            ClassVars.Clear();
            AddVars(state.InitS.Code);
            AddVars(state.UpdateS.Code);
            AddVars(state.DrawS.Code);
            foreach(var nv in Vars)
            {
                ClassVars.Add(nv);
            }
            WriteVars();
            

            Write("     public override void Init()");
            Write("     {");

            WriteCode(state.InitS.Code);
            

            Write("     }");

            Write("     public override void Update()");
            Write("     {");

            WriteCode(state.UpdateS.Code);

            Write("     }");

            Write("     public override void Draw()");
            Write("     {");

            WriteCode(state.DrawS.Code);

            Write("     }");

            WriteClassFooter();

            ClassVars.Clear();

        }

        public List<string> ClassVars = new List<string>();

        public bool IsClassVar(string name)
        {
            foreach(var cv in ClassVars)
            {
                if (cv == name) return true;
            }
            return false;
        }

        public void WriteCode(Structs.StructCode code)
        {
            List<string> def = new List<string>();
            
            foreach(var l in code.Lines)
            {
            
                if(l is StructAssign)
                {
                    var sa = l as StructAssign;

                    if (!ClassVars.Contains(sa.Vars[0].Name) && !def.Contains(sa.Vars[0].Name)) 
                    {

                        Write("dynamic " + sa.Vars[0].Name +  "= null;");
                    }



                    def.Add(sa.Vars[0].Name);



                }
            }

            foreach (var l in code.Lines)
            {
                Tab = 4;
                if(l is StructForEach)
                {

                    var sfe = l as StructForEach;
                    C("dynamic item = ");
                    WriteExpr(sfe.Enumer);
                    C(".first;");
                    Write("while(true)");
                    Write("{");
                                                                  

                    WriteCode(sfe.Code);

                    Write("if(item == item.Next)");
                    Write("{");
                    Write("Console.WriteLine(\"Same\");");

                    Write("}");
                    Write("item = item.Next;");
                                        Write("if(item==null || item.Obj == null)");
                    Write("{");
                    Write("break;");
                    Write("}");

                    Write("}");

                }
                if(l is StructBreak)
                {

                    Write("break;");

                }
                if (l is StructReturn)
                {
                    var lr = l as StructReturn;

                    if (lr.ReturnExp == null)
                    {
                        Write("return;");
                    }
                    else
                    {
                        C("return ");
                        WriteExpr(lr.ReturnExp);
                        Write(";");
                    }
                }
                if(l is StructIf)
                {

                    var sif = l as StructIf;

                    C("if(");
                    WriteExpr(sif.Condition);
                    Write(")");
                    Write("{");
                    WriteCode(sif.TrueCode);
                    Write("}");
                    if (sif.ElseIf.Count > 0)
                    {
                        int ic = 0;
                        foreach(var eif in sif.ElseIf)
                        {
                            C("else if(");
                            WriteExpr(eif);
                            Write(")");
                            Write("{");
                            WriteCode(sif.ElseIfCode[ic]);
                            Write(")");
                            ic++;
                        }
                    }else if (sif.ElseCode != null)
                    {
                        Write("else {");
                        WriteCode(sif.ElseCode);
                        Write("}");
                    }
                    


                }
                if (l is StructWhile)
                {

                    var sw = l as StructWhile;

                    C("while(");
                    WriteExpr(sw.Condition);
                    Write(")");
                    Write("{");
                    WriteCode(sw.Code);
                    Write("};");
                }
                if (l is StructFor)
                {

                    var sf = l as StructFor;

                    C("for(");
                    WriteAssign(sf.Initial, true);
                    C(";");
                    WriteExpr(sf.Condition);
                    C(";");
                    WriteAssign(sf.Inc, true,true);
                    C(")");
                    Write("");
                    Write("{");
                    WriteCode(sf.Code);
                    Write("}");

                }

                if (l is StructFlatCall)
                {
                    Write("");
                    var sfc = l as StructFlatCall;

                    C("         " + sfc.FuncName + "(");
                    WriteCallPars(sfc.CallPars);
                    WriteEnd(");");

                }
                if (l is StructAssign)
                {
                    WriteAssign(l,false);

                }
                if (l is StructClassCall)
                {
                    var scc = l as StructClassCall;

                    foreach (var cc in scc.call)
                    {
                        C(cc);
                    }
                    C("(");
                    if (scc.Pars.Pars.Count>0)
                    {
                        int num = 0;
                        foreach(var pe in scc.Pars.Pars)
                        {
                            WriteExpr(pe);
                            
                            num++;
                            if (num < scc.Pars.Pars.Count)
                            {
                                C(",");
                                
                            }
                        }
                    }
                    C(");");
                    Write("");
                }
            }

        }

        private void WriteAssign(Struct l,bool iline = false,bool nodyn = false)
        {
            var sa = l as StructAssign;
            if(sa.Vars[0].Name =="first")
            {
                int gfg = 2;
            }
            if (IsClassVar(sa.Vars[0].Name))
            {
                int g = 5;
            }
            else
            {
                if (iline)
                {
                    if (nodyn)
                    {
                        C(sa.Vars[0].Name + " = ");
                    }
                    else
                    {
                        C(" dynamic " + sa.Vars[0].Name + " = ");
                    }
                }
                else
                {
                    //Write("");
                    //C(" dynamic " + sa.Vars[0].Name + ";");
                   // Write("");


                }
            }

            if (!iline)
            {
                Write("");





                if (sa.Vars.Count == 1)
                {
                    C(sa.Vars[0].Name + " = ");
                    WriteExpr(sa.Expr[0]);
                    C(";");
                    Write("");
                }
                else
                {
                    int vc = 0;
                    foreach(var v in sa.Vars)
                    {

                        C(v.Name);
                        if (vc < sa.Vars.Count-1)
                        {
                            C(".");
                        }
                        vc++;

                    }
                    C(" = ");
                    WriteExpr(sa.Expr[0]);
                    C(";");
                    Write("");

                }
            }
            else
            {
                
                WriteExpr(sa.Expr[0]);
            }
        }

        public void WriteCallPars(StructCallPars pars)
        {

            int pi = 0;
            foreach(var p in pars.Pars)
            {
                WriteExpr(p);
                pi++;
                if (pi < pars.Pars.Count)
                {
                    C(",");
                }
            }
        }
        string pobj = "";
        public void TestExpr(StructExpr expr)
        {
            if (expr.Expr.Count > 0)
            {
                foreach (var se in expr.Expr)
                {
                    TestExpr(se);
                }
            }
            else
            {
                switch (expr.Type)
                {
                    case ExprType.VarValue:
                    case ExprType.ClassValue:
                        isObj = true;
                        break;
                }
            }
        }
        bool isObj = false;
        public void WriteExpr(StructExpr expr)
        {

            if (expr.Expr.Count > 0)
            {
                int ei = 0;
                foreach(var se in expr.Expr)
                {

                    WriteExpr(se);
                    ei++;
                    if (ei < expr.Expr.Count)
                    {
                        isObj = false;
                        TestExpr(expr.Expr[ei]);
                        //if(expr.Expr[ei].Type 

                    }
                    if (ei < expr.Expr.Count)
                    {
                        
                            if (!osfuncs.Contains(pobj) && pobj!="N" && isObj == true)
                            {
                                C(".");
                            }
                        pobj = "";
                     //   C(",");
                    }
                }
            }
            else
            {
                switch(expr.Type)
                {
                    
                    case ExprType.StringValue:

                        C(" \"" + expr.StringV + "\"");
                        pobj = "N";

                        break;
                    case ExprType.IntValue:
                        C(" " + expr.intV + " ");
                        pobj = "N";
                        break;
                    case ExprType.FloatValue:
                        C(" " + expr.floatV + "f");
                        pobj = "N";
                        break;
                    case ExprType.SubExpr:
                        int oh = 1;

                        break;
                    case ExprType.ClassVar:
                        int oh2 = 2;
                        break;
                    case ExprType.VarValue:

                        C(" " + expr.VarName);
                        if (expr.CallPars != null)
                        {
                            C("(");
                            WriteCallPars(expr.CallPars);
                            C(")");
                        }
                        else
                        {
                            if (osfuncs.Contains(expr.VarName))
                            {
                                C("(");
                                C(")");
                            }
                        }
                        pobj = expr.VarName;
                        break;
                    case ExprType.NewClass:
                        C(" new ");

                        if (expr.NewPars != null)
                        {
                            C(expr.NewClassType);
                            C("(");
                            WriteCallPars(expr.NewPars);
                            C(")");
                            break;
                        }
                        if (expr.CallPars != null)
                        {
                            C(expr.NewClassType);
                            C("(");
                            WriteCallPars(expr.CallPars);
                            C(")");
                        }
                        else
                        {
                            C(expr.NewClassType);
                            C("()");
                        }
                        break;
                    case ExprType.Operator:
                        switch (expr.Op)
                        {
                            case OpType.Null:
                                C(" null ");
                                break;
                            case OpType.LessThan:
                                C(" < ");
                                break;
                            case OpType.MoreThan:
                                C(" > ");
                                break;
                            case OpType.Plus:
                                C(" + ");
                                break;
                            case OpType.Minus:
                                C(" - ");
                                break;
                            case OpType.Divide:
                                C(" / ");
                                break;
                            case OpType.Times:
                                C(" * ");
                                break;
                            case OpType.EqualTo:
                                C(" == ");
                                break;
                        }
                        pobj = "N";
                        break;
                }
            }

        }

        public void C(string text)
        {
            tw.Write(text);
        }

        public void WriteEnd(string text)
        {
            tw.Write(text);
            tw.WriteLine("");
        }

        public void WriteFunc(string name, bool stat = false, string exp = "")
        {

            string func = name;
            string pre = "public";
            if (stat)
            {
                pre = pre + " static";
            }

            string o = pre + " " + func + " (";

           

        }

        public void WriteClassHeader(string className,string extends="")
        {

            Write("");
            Write(" public class " + className+(extends=="" ? " : SynCObject " : " : "+extends));
            Write(" {");

        }

        public void WriteClassFooter()
        {
            Write("");
            Write(" }");
        }

        public void WriteHeader()
        {

            Write("//This output was generated by SynC compiler.");
            Write("using System;");
            Write("using System.IO;");
            Write("using Vivid;");
            Write("using Vivid.App;");
            Write("using Vivid.SynC;");


        }

        public void WriteFooter()
        {
            Write("");
   
        }

        public void Write(string text)
        {
            var pre = "";
            pre = pre.PadLeft(4 * Tab);
            tw.WriteLine(pre+text);

        }
        public void WriteFile(string file)
        {
            fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            tw = new StreamWriter(fs);
        }

        public void EndWrite()
        {
            fs.Flush();
            tw.Flush();
            fs.Close();
        }

    }
}
