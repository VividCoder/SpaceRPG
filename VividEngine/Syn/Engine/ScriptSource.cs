﻿using System.IO;

namespace Vivid
{
    public class ScriptSource
    {
        public string Path = "";
        public TokenStream Tokens = null;

        public ScriptSource(string path)
        {
            Path = path;
            string[] code = File.ReadAllLines(path);

            int ic = 0;
            Tokenizer toker = new Tokenizer();
            TokenStream str = new TokenStream();
            while (true)
            {
                //   System.Console.WriteLine ( "Line:" + code [ ic ] );
                TokenStream tokes = toker.ParseString2(code[ic]);
                foreach (var pt in tokes.Tokes)
                {
                    pt.LineI = ic;
                }

                str.Add(new CodeToken(TokenClass.BeginLine, Token.BeginLine, ""));
                foreach (CodeToken t in tokes.Tokes)

                {
                    str.Add(t);

                    //     System.Console.WriteLine ( "Toke:" + t.Text );
                }

                ic++;
                if (ic == code.Length)
                {
                    break;
                }
            }
            for (int i = 0; i < str.Len; i++)
            {
                //System.Console.WriteLine ( "T:" + str.Tokes [ i ] );
            }

            foreach(var tt in str.Tokes)
            {
                System.Console.WriteLine("TT:" + tt.Text);
            }
            Tokens = str;
        }

        /*
            Path = path;
            //FileStream fs = new FileStream(path, FileMode.Open,
            //  FileAccess.Read);
            //TextReader r = new StreamReader(fs);
            VTokenizer toker = new VTokenizer();
            Tokens = new VTokenStream ( );
            VToken elt = new VToken(TokenClass.Flow, Token.EndLine, "EndLine");
            string[] lines = File.ReadAllLines(path);
            int li = 0;
            while ( true )
            {
                // Console.WriteLine("Pos:" + fs.Position + " Len:" + fs.Length);
                //if (fs.Position >= fs.Length) break;
                if ( li == lines.Length )
                {
                    break;
                }

                string cl = lines[li];
                li++;
                Console.WriteLine ( "LI:" + li );
                if ( li > lines.Length )
                {
                    break;
                }
                //fs.Seek(cl.Length, SeekOrigin.Begin);
                //it = it + cl.Length;

                // if (it > fs.Length) break;
                cl = cl + System.Environment.NewLine;
                // Console.WriteLine("CL:" + cl);
                if ( cl == null || cl == string.Empty || cl == " " || cl == System.Environment.NewLine )
                {
                    continue;
                }
                VTokenStream ts = toker.ParseString(cl);

                //Console.WriteLine("Parsed line. " + ts.Len + " tokens.");
                foreach ( VToken t in ts.Tokes )
                {
                    if ( t.Token != Token.String )
                    {
                        if ( t.Text.Contains ( System.Environment.NewLine ) && t.Text.Contains ( "\r" ) && t.Text.Length < 3 )
                        {
                            continue;
                        }
                    }

                    Tokens.Add ( t );
                }
                Tokens.Add ( elt );
            }
            List<VToken> nk = new List<VToken>();
            // fs.Close();
            Console.WriteLine ( "TokDump" );
            VToken pt = null;
            foreach ( VToken t in Tokens.Tokes )
            {
                if ( pt != null )
                {
                    if ( t.Token == Token.EndLine && pt.Token == Token.EndLine )
                    {
                        continue;
                    }
                }
                //if(t.Token!=)
                if ( t.Token != Token.String )
                {
                    t.Text = t.Text.Replace ( " ", "" );
                    t.Text = t.Text.Replace ( "   ", "" );
                    t.Text = t.Text.Replace ( System.Environment.NewLine, "" );
                }
                if ( t.Text.Length < 1 )
                {
                    continue;
                }
                if ( t.Text == "" )
                {
                    continue;
                }
                if ( t.Text == System.Environment.NewLine )
                {
                    continue;
                }
                if ( t.Text == "  " )
                {
                    continue;
                }
                nk.Add ( t );
                pt = t;
                if ( t.Text == "end" || t.Text == "End" )
                {
                    t.Token = Token.End;
                    t.Class = TokenClass.Flow;
                }
                Console.WriteLine ( "T:" + t.Text + " TOK:" + t.Token );
            }
            Tokens = new VTokenStream
            {
                Tokes = nk
            };
            Console.WriteLine ( "Done." );
            Console.WriteLine ( "Parsed Source:" + Tokens.Len + " tokens." );
        }
        */
    }
}