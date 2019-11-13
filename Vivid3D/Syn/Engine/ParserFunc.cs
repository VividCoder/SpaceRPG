using Vivid.Structs;

namespace Vivid
{
    public partial class Parser
    {
        public StructFunc ParseFunc(ref int i)
        {
            bool is_static = false;

            Log("Begun parsing function", i, false);

            if (toks.Tokes[i].Token != Token.Func)
            {
                Error(i, "Expecting 'func' definition.");
            }

            if (Peek(i + 1).Text == "static")
            {
                is_static = true;
                i++;
            }

            string func_name = Peek(i + 1).Text;

            if(func_name.Contains("test1"))
            {
                int v = 5;

            }

            Log("Module func:" + func_name + " static:" + is_static, i, false);

            i += 2;

            StructFunc func = new StructFunc();

            func.Static = is_static;
            func.FuncName = func_name;

            var ftok = toks.Tokes[i];

            if (ftok.Text != "(")
            {
                Error(i, "Expected begining of function definition parameters.");
                //    Console.WriteLine("Error, expected (");
            }

            var pars = ParseParameters(ref i);

            Log("ParsPassed:", i);

            var code_body = ParseCodeBody(ref i);
            Log("Parsed code-body.", i);
            func.Code = code_body;
            func.Pars = pars;

            return func;
        }
    }
}