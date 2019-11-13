using Vivid.Structs;

namespace Vivid
{
    public partial class Parser
    {
        public StructClassCall ParseClassStatement(ref int i)
        {
            var scc = new StructClassCall();

            for (i = i; i < toks.Len; i++)
            {
                switch (Get(i).Token)
                {
                    case Token.Id:
                        scc.call.Add(Get(i).Text);
                        break;

                    case Token.LeftPara:
                        Log("Checking.", i);
                        var pars = ParseCallPars(ref i);
                        scc.Pars = pars;

                        i = NextToken(i, Token.RightPara);
                        return scc;
                        break;
                }
            }

            return scc;
        }
    }
}