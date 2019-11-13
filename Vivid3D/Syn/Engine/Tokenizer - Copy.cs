﻿using System.Collections.Generic;

namespace Vivid
{
    public enum NextType
    {
        Alpha, Num, Space, Sep, Eol, Unknown, Equal, Greater, Less, Not
    }

    public class Tokenizer
    {
        public List<string> Ops = new List<string>();
        public List<string> Types = new List<string>();
        public List<string> Keys = new List<string>();
        public Dictionary<string, CodeToken> ConvTable = new Dictionary<string, CodeToken>();

        public void AddConv(string tok, CodeToken t)
        {
            ConvTable[tok] = t;
        }

        public void AddConv(string tok, TokenClass cls, Token t)
        {
            ConvTable[tok] = new CodeToken(cls, t, tok);
        }

        public Tokenizer()
        {
            Ops.Add("+");
            Ops.Add("-");
            Ops.Add("/");
            Ops.Add("*");
            Ops.Add("^");
            Ops.Add("%");

            Ops.Add("==");
            Ops.Add("=");
            AddConv("%", new CodeToken(TokenClass.Op, Token.Percent, "%"));
            AddConv("+", new CodeToken(TokenClass.Op, Token.Plus, "+"));
            AddConv("-", new CodeToken(TokenClass.Op, Token.Minus, "-"));
            AddConv("/", new CodeToken(TokenClass.Op, Token.Div, "/"));
            AddConv("*", new CodeToken(TokenClass.Op, Token.Multi, "*"));
            AddConv("^", new CodeToken(TokenClass.Op, Token.Pow, "^"));
            AddConv("return", new CodeToken(TokenClass.Flow, Token.Return, "return"));

            AddConv("==", new CodeToken(TokenClass.Op, Token.Equal, "=="));
            AddConv("=", new CodeToken(TokenClass.Op, Token.Equal, "="));
            AddConv("!", new CodeToken(TokenClass.Op, Token.Not, "!"));
            AddConv("<", new CodeToken(TokenClass.Op, Token.Lesser, "<"));
            AddConv(">", new CodeToken(TokenClass.Op, Token.Greater, ">"));
            AddConv("<=", new CodeToken(TokenClass.Op, Token.LessEqual, "<="));
            AddConv(">=", new CodeToken(TokenClass.Op, Token.GreatEqual, ">="));
            AddConv("=", new CodeToken(TokenClass.Assign, Token.Equal, "="));

            Types.Add("vec2");
            Types.Add("vec3");
            Types.Add("vec4");
            Types.Add("matrix4");
            Types.Add("matrix3");
            Types.Add("byte");
            Types.Add("short");
            Types.Add("int");
            Types.Add("long");
            Types.Add("float");
            Types.Add("double");
            Types.Add("string");
            Types.Add("bool");
            AddConv("compute", TokenClass.Compute, Token.Compute);
            AddConv("computestruct", TokenClass.Compute, Token.ComputeStruct);
            AddConv("vec2", TokenClass.Type, Token.Vec2);
            AddConv("vec3", TokenClass.Type, Token.Vec3);
            AddConv("vec4", TokenClass.Type, Token.Vec4);
            AddConv("matrix4", TokenClass.Type, Token.Matrix4);
            AddConv("matrix3", TokenClass.Type, Token.Matrix3);
            AddConv("byte", TokenClass.Type, Token.Byte);
            AddConv("short", TokenClass.Type, Token.Short);
            AddConv("int", TokenClass.Type, Token.Int);
            AddConv("long", TokenClass.Type, Token.Long);
            AddConv("float", TokenClass.Type, Token.Float);
            AddConv("double", TokenClass.Type, Token.Double);
            AddConv("string", TokenClass.Type, Token.String);
            AddConv("bool", TokenClass.Type, Token.Bool);
            AddConv("game", TokenClass.Game, Token.Game);
            AddConv("Game", TokenClass.Game, Token.Game);
            AddConv("Author", TokenClass.Info, Token.Author);
            AddConv("Copyright", TokenClass.Info, Token.Copyright);
            AddConv("Title", TokenClass.Info, Token.Title);
            AddConv("Version", TokenClass.Info, Token.Version);
            AddConv("state", TokenClass.Define, Token.State);
            AddConv("State", TokenClass.Define, Token.State);
            AddConv("End", TokenClass.Flow, Token.End);
            AddConv("Init", TokenClass.State, Token.Init);
            AddConv("init", TokenClass.State, Token.Init);
            AddConv("Sub", TokenClass.Define, Token.Sub);
            AddConv("sub", TokenClass.Define, Token.Sub);
            AddConv("Thread", TokenClass.Define, Token.Thread);
            AddConv("thread", TokenClass.Define, Token.Thread);
            AddConv("Call", TokenClass.Flow, Token.Call);
            AddConv("call", TokenClass.Flow, Token.Call);
            AddConv("Loop", TokenClass.Flow, Token.Loop);
            AddConv("loop", TokenClass.Flow, Token.Loop);
            AddConv("print", TokenClass.Statement, Token.Print);
            AddConv("Print", TokenClass.Statement, Token.Print);
            Keys.Add("if");
            Keys.Add("elseif");
            Keys.Add("end");
            Keys.Add("for");
            Keys.Add("next");
            Keys.Add("while");
            Keys.Add("module");
            Keys.Add("method");
            Keys.Add("function");
            Keys.Add("var");
            AddConv("var", TokenClass.Define, Token.Var);
            AddConv("end", TokenClass.Flow, Token.End);
            AddConv("if", TokenClass.Flow, Token.If);
            AddConv("elseif", TokenClass.Flow, Token.ElseIf);
            AddConv("else", TokenClass.Flow, Token.Else);
            AddConv("for", TokenClass.Flow, Token.For);
            AddConv("next", TokenClass.Flow, Token.Next);
            AddConv("while", TokenClass.Flow, Token.While);
            AddConv("wend", TokenClass.Flow, Token.Wend);
            AddConv("module", TokenClass.Define, Token.Module);
            AddConv("method", TokenClass.Define, Token.Method);
            AddConv("function", TokenClass.Define, Token.Func);
            AddConv("func", TokenClass.Define, Token.Func);
            AddConv("break", TokenClass.Flow, Token.Break);
            AddConv("stop", TokenClass.Flow, Token.Stop);
            AddConv("foreach", TokenClass.Flow, Token.ForEach);
            AddConv("compilestop", TokenClass.Meta, Token.CompileStop);
            AddConv("null", TokenClass.Op, Token.Null);

            Keys.Add(",");
            AddConv(",", TokenClass.Sep, Token.Comma);

            Keys.Add(")");
            Keys.Add("(");
            Keys.Add("[");
            Keys.Add("]");
            AddConv("(", TokenClass.Scope, Token.LeftPara);
            AddConv(")", TokenClass.Scope, Token.RightPara);
            AddConv("[", TokenClass.Array, Token.LeftArray);
            AddConv("]", TokenClass.Array, Token.RightArray);

            Keys.Add("new");
            AddConv("new", TokenClass.New, Token.New);

            Keys.Add("true");
            Keys.Add("false");
            AddConv("true", TokenClass.Bool, Token.True);
            AddConv("false", TokenClass.Bool, Token.False);
        }

        public CodeToken Id(string t)
        {
            foreach (string k in ConvTable.Keys)
            {
                CodeToken at = ConvTable[k];
                if (at.Text == t)
                {
                    return at.Clone();
                }
            }
            return new CodeToken(TokenClass.Id, Token.Id, t);
        }

        public NextType GetNext(string code, int f)
        {
            f++;
            NextType nt = NextType.Unknown;
            if (f >= code.Length)

            {
                return NextType.Eol;
            }
            for (int c = f; f < code.Length; c++)
            {
                if (c >= code.Length)
                {
                    break;
                }

                char cc = code[c];
                string cs = cc.ToString();
                if (cs == " ")
                {
                    return NextType.Space;
                }
                if (cs == ",")
                {
                    return NextType.Sep;
                }
                if (cs[0] >= "a"[0] && cs[0] <= "z"[0])
                {
                    return NextType.Alpha;
                }
                if (cs[0] >= "A"[0] && cs[0] <= "Z"[0])
                {
                    return NextType.Alpha;
                }
                if (cs[0] >= "0"[0] && cs[0] <= "9"[0])
                {
                    return NextType.Num;
                }
                if (cs == "=")
                {
                    return NextType.Equal;
                }
            }
            return nt;
        }

        public TokenStream ParseString2(string code)
        {
            //code = code.Replace ( "   ", "" );

            while (false)
            {
                int find = code.IndexOf("  ");
                if (find == -1)
                {
                    break;
                }

                string code1 = code.Substring(0, find);
                string code2 = code.Substring(find + 3);
                code1 = code1.Trim();
                code2 = code2.Trim();
                code = code1 + " " + code2;
            }

            List<string> elements = new List<string>();
            List<int> elements_s = new List<int>();
            string cur = "";
            bool string_on = false;
            bool num_on = false;
            int c_s = 0;
            bool minus = false;
            string ptok = "";
            for (int c = 0; c < code.Length; c++)
            {
                string ch = code[c].ToString();

                if (ch[0] >= "0"[0] && ch[0] <= "9"[0])
                {
                    if (cur.Length == 0)
                    {
                        if (string_on == false)
                        {
                            num_on = true;
                            if (minus)
                            {
                                cur = "-";
                                minus = false;
                            }
                        }
                    }else if(cur.Length == 1)
                    {

                        if (string_on == false)
                        {

                           

                        }

                    }
                }
                if (ch == "\"")
                {
                    if (!string_on)
                    {
                        string_on = true;
                        if (cur.Length > 0)
                        {
                            ptok = cur;
                            elements.Add(cur);
                            elements_s.Add(c_s);
                        }
                        cur = "";
                        c_s = c;
                        num_on = false;
                        continue;
                    }
                    else
                    {
                        ptok = cur;
                        elements.Add("\"" + cur + "\"");
                        elements_s.Add(c_s);
                        string_on = false;
                        cur = "";
                        c_s = c;
                        num_on = false;
                        continue;
                    }
                }
                if (string_on)
                {
                    cur = cur + ch;
                    continue;
                }

                if (num_on)
                {
                    if (ch == ".")
                    {
                        cur = cur + ch;
                        continue;
                    }
                }

                switch (ch)
                {
                    case "\n":

                        int lg = 0;
                        for (int nn = 0; nn < cur.Length; nn++)
                        {
                            if (cur[nn].ToString() != " " && cur[nn].ToString() != " ")
                            {
                                lg = nn;
                            }
                        }
                        cur = cur.Substring(0, lg);

                        break;

                    case " ":
                        if (cur.Length > 0)
                        {
                            ptok = cur;
                            elements.Add(cur);
                            elements_s.Add(c_s);
                        }
                        cur = "";
                        c_s = c;
                        num_on = false;
                        continue;
                        break;

                    case "+":
                    case "/":
                    case "-":
                    case "*":
                    case "<":
                    case ">":
                    case "=":
                    case ".":
                    case ",":
                    case ":":
                    case "[":
                    case "]":
                    case "(":
                    case ")":
                    case "!":
                    case "{":
                    case "}":
                    case ";":

                        if (ch == "-")
                        {
                            bool skip = true;
                            
                            switch (ptok)
                            {
                                case "(":
                                case "=":
                                case ",":
                                case "+":
                                case "/":
                                case "*":
                                case "-":
                                    if (cur.Length > 0)
                                    {
                                        switch (cur)
                                        {
                                            case "(":
                                            case "=":
                                            case ",":
                                            case "+":
                                            case "/":
                                            case "*":
                                            case "-":
                                                skip = true;
                                                break;
                                        }
                                        break;
                                    }

                                    skip = false;
                                    
                                    break;
                            }
                            if (!skip)
                            {
                                string ch2 = code[c + 1].ToString();
                                ch = "";
                                if (ch2[0] >= "0"[0] && ch2[0] <= "9"[0])
                                {
                                    minus = true;
                                    if (cur.Length > 0)
                                    {
                                        ptok = cur;
                                        elements.Add(cur);
                                        elements_s.Add(c_s);
                                    }


                                    cur = "";
                                    c_s = c;
                                    num_on = false;

                                    break;
                                }
                            }
                        }
                       
                        if (cur.Length > 0)
                        {
                            ptok = cur;
                            elements.Add(cur);
                            elements_s.Add(c_s);
                        }
                        ptok = ch;
                        elements.Add(ch);
                        elements_s.Add(c_s);
                        cur = "";
                        c_s = c;
                        num_on = false;
                        continue;
                        break;
                }
                cur += ch;

               
                // Console.WriteLine ( ch );
            }
            if (cur.Length > 0)
            {
                ptok = cur;
                elements.Add(cur);
                elements_s.Add(c_s);
            }
            foreach (var el in elements)
            {
                if (el == "tokestop")
                {

                }
            }
            List<string> final_elements = new List<string>();
            List<int> final_elements_s = new List<int>();
            int s_i = 0;
            foreach (string ele in elements)
            {
                int c_s2 = elements_s[s_i];
                s_i++;
                System.Console.WriteLine ( "E:" + ele+":" );
                bool keep = false;
                for (int i = 0; i < good.Length; i++)
                {
                    if (ele.Contains(good[i].ToString()))
                    {
                        keep = true;
                        continue;
                    }
                }
                string ne = "";
                ne = ele.Trim();
                if (keep)
                {
                    final_elements.Add(ne);
                    final_elements_s.Add(c_s2);
                }
            }
            TokenStream rs = new TokenStream();
            int s_i2 = 0;
            foreach (string word in final_elements)
            {
                int c_s1 = final_elements_s[s_i2];
                s_i2++;
                CodeToken tok = Id(word);
                if (tok.Text[0].ToString() == "\"")
                {
                    tok.Text = tok.Text.Substring(1, tok.Text.Length - 2);
                    tok.Class = TokenClass.Value;
                    tok.Token = Token.String;
                }
                if (tok.Text == ";")
                {
                    tok.Class = TokenClass.Flow;
                    tok.Token = Token.EndLine;
                }
                if (tok.Text[0] == "-"[0])
                {
                    if (tok.Text.Length > 1)
                    {
                        if (tok.Text[1] >= "0"[0] && tok.Text[1] <= "9"[0])
                        {
                            tok.Class = TokenClass.Value;
                            tok.Token = Token.Int;
                            if (tok.Text.Contains("."))
                            {
                                tok.Token = Token.Float;
                                tok.FVal = float.Parse(tok.Text);
                            }
                        }
                    }
                }
                if (tok.Text[0] >= "0"[0] && tok.Text[0] <= "9"[0])
                {
                    tok.Class = TokenClass.Value;
                    tok.Token = Token.Int;
                    if (tok.Text.Contains("."))
                    {
                        tok.Token = Token.Float;
                        tok.FVal = float.Parse(tok.Text);
                    }
                }
                if (tok.Text == "=")
                {
                    tok.Class = TokenClass.Op;
                    tok.Token = Token.Equal;
                }
                tok.LineC = c_s1;
                //for(int tc = 0l; tc < tok.Token)
                //{
                rs.Add(tok);
                //  Console.WriteLine ( "Tok:" + tok );
            }

            return rs;
        }

        public string good = "!£$%^&*()-=_+-=/*,.<>[]{}(;:)abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public TokenStream ParseString(string code)
        {
            return ParseString2(code);
        }
    }
}