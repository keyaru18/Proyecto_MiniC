using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniC;

namespace MiniC
{
    public class Tokens
    {
        int _Linea;
        string _Lexema;
        int _Token;

        public Tokens(int linea, string lexema, int token)
        {
            _Linea = linea;
            _Lexema = lexema;
            _Token = token;
        }
        public int Linea { get => _Linea; set => _Linea = value; }
        public string Lexema { get => _Lexema; set => _Lexema = value; }
        public int Token { get => _Token; set => _Token = value; }
    }
    public class AnalizarLexico
    {
        readonly UnidadesLexica UL = new UnidadesLexica();
        List<Tokens> LstTokens = new List<Tokens>(); // creamos una lista de tokens para almacenar los lexemas y sus tokens
        readonly string A = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        readonly string D = "0123456789";
        readonly string AR = "+-*/%";
        int Cont = 0; //variable global que cuenta posicion en el archivo
        int Linea = 1;
        int[] EstadosAceptados = { 1, 3, 5 };
        string Lexema = string.Empty; //char cant = '0';


        // Eliminar espacios en blanco
        // Eliminar comentarios de linea o de bloque
        // Eliminar tabulaciones, saltos de linea \n \r
        // Validar que los símbolos sean válidos
        // Relacionar las líneas de código con errores
        // Generar la tabla de identificadores
        // Generar la lista de tokens (envia a analizador sintactico)

        protected int GetAlfabetoAlfanumerico(char c) // para reconocer que nos está llegando
        {
            if (A.Contains(c))
                return 0; // si es letra retorna columna 1 de la tabla de trancisión
            else if (D.Contains(c))
                return 1; // si es dígito retorna posición 2 de la tabla de trancisión
            else if (c == '_')
                return 2; // si es guion bajo retorna posición 3 de la tabla
            return -1; // si no es ninguna regresa -1
        }

        protected int GetAlfabetoNumero(char c) // para reconocer que nos está llegando
        {
            if (char.IsDigit(c))
                return 0;
            else if (c == '+')
                return 1;
            else if (c == '-')
                return 2;
            else if (c == '=')
                return 3;
            else if (c == '.')
                return 4;
            else if (c == 'E')
                return 5;
            else if (c == 'e')
                return 6;
            return -1;
        }

        protected int GetAlfabetoDiagonal(char c)
        {
            if (c == '/')
                return 0;
            else if (c == '*')
                return 1;
            else if (c == '=')
                return 2;
            return 3;
        }

        protected int GetAlfabetoComillaDoble(char c, char cant)
        {
            if (c == 'f' && cant == '%')
                return 7;
            else if (c == 'd' && cant == '%')
                return 8;
            else if (c == 'c' && cant == '%')
                return 9;
            else if (c == 's' && cant == '%')
                return 10;
            else if (c == 'i' && cant == '%')
                return 11;
            else if (A.Contains(c))
                return 0;
            else if (D.Contains(c))
                return 1;
            else if (c == '\n' || c == '\r' || c == '\a')
                return 2;
            else if (AR.Contains(c))
                return 3;
            else if (c == ' ')
                return 4;
            else if (c == '"')
                return 5;
            else if (c == '%')
                return 6;
            else if (c == '.')
                return 12;
            return -1;
        }

        protected void IdentificadorPalabraReservada(string Archivo)
        {
            char c;
            int Estado = 0;
            int Simbolo;
            string Lexema = string.Empty;
            int[,] TT =
            {
            { 1, -1, 1 },
            { 1, 1, 1 }
        };
            do
            {
                c = Archivo[Cont];
                Simbolo = GetAlfabetoAlfanumerico(c);
                if (Simbolo == -1)
                    break;
                Lexema += c;
                Estado = TT[Estado, Simbolo];
                Cont++;
            } while (Cont < Archivo.Length);

            if (Estado == 1)
            {
                int token = UL.GetTokenPalabra(Lexema);
                if (token == 300)
                    token = 305; // identificador o nombre de tipo
                LstTokens.Add(new Tokens(Linea, Lexema, token));
            }
        }

        protected void AutomataNumeros(string Archivo)
        {
            char c; // del archivo tomamos el caracter donde nos quedamos
            int Estado = 0;
            int Simbolo;
            string Lexema = string.Empty; // decladamos las variable que necesitaremos
            int[,] TT =   // declaramos la tabla de trancisiones
            {
                {5,1,2,-1,4,-1,-1},
                {5,3,-1,3,4,-1,-1},
                {5,-1,3,3,4,-1,-1},
                {-1,-1,-1,-1,-1,-1,-1},
                {5,-1,-1,-1,-1,-1,-1},
                {5,-1,-1,-1,6,8,8},
                {7,-1,-1,-1,-1,-1,-1},
                {7,-1,-1,-1,-1,8,8},
                {10,9,9,-1,-1,-1,-1},
                {10,-1,-1,-1,-1,-1,-1},
                {10,-1,-1,-1,-1,-1,-1}
            };
            do
            {
                c = Archivo[Cont]; // recibo el simbolo
                Simbolo = GetAlfabetoNumero(c);

                if (Simbolo == -1)
                    break; // si no es ninguno de los anteriores rompe el bucle

                Lexema += c; // si corresponde a a alguno de los anteriores continua formando el lexema
                Estado = TT[Estado, Simbolo]; // lee en la tabla de trancisión el nuevo estado al que pasa
                Cont++; //incrementamos el contador para que traiga el siguiente token

            } while (Cont < Archivo.Length); // ciclo infinito que se repite infinitamente

            if (Estado == 1 || Estado == 2 || Estado == 3) // para guardar en la lista de tokens, solo si quedó en el estado 1 que es de finalización
            {
                LstTokens.Add(new Tokens(Linea, Lexema, UL.GetTokenSimbolo(Lexema)));
                Lexema = string.Empty;
            }
            else if (Estado == 5 || Estado == 7 || Estado == 10)
            {
                if (Lexema.Contains("."))
                {
                    LstTokens.Add(new Tokens(Linea, Lexema, 302));
                    Lexema = string.Empty;
                }
                else
                {
                    LstTokens.Add(new Tokens(Linea, Lexema, 301));
                    Lexema = string.Empty;
                }
            }
        }

        protected void GrDiagonal(string Archivo)
        {
            char c = Archivo[Cont];
            //char caux;
            int Simbolo = GetAlfabetoDiagonal(c);
            int Estado = 0;
            string Lexema = string.Empty;
            Lexema += c;

            int[,] TT =
            {
                {  1, -1, -1, -1 },
                {  5,  2,  6, -1 },
                {  2,  3,  2,  2 },
                {  4,  3,  2,  2 },
                {  1, -1, -1, -1 },
                {  5,  5,  5,  5 },
                { -1, -1, -1, -1 }
            };

            do
            {
                c = Archivo[Cont]; // Variable global para especificar la posición del apuntador en el archivo
                Simbolo = GetAlfabetoDiagonal(c);

                if (Estado == 1 && Simbolo == 3)
                    break;
                else if (Estado == 5 && c == '\n')
                {
                    Linea++;
                    break;
                }
                else if (Estado == 6 || Estado == 4)
                    break;

                Lexema += c;
                Estado = TT[Estado, Simbolo];
                Cont++;
            }
            while (Cont < Archivo.Length);

            if (Estado == 2 || Estado == 3 || Estado == 4)
            {
                LstTokens.Add(new Tokens(Linea, Lexema, 304)); // Token para comentario de bloque
            }

            // todo: Verificar cuando el automata se queda en un estado no final. Enviar mensaje de error lexico
        }

        protected void GrAutomataDosSimbolos(string Archivo)
        {
            char caux;
            string Lexema = string.Empty;

            // Verificar que existe un carácter siguiente antes de acceder a él
            if (Cont + 1 < Archivo.Length)
            {
                caux = Archivo[Cont + 1];

                if ("=&><|".Contains(caux))
                {
                    Lexema += caux;
                    Cont++;
                }
            }

            LstTokens.Add(new Tokens(Linea, Lexema, UL.GetTokenSimbolo(Lexema)));
            Cont++;
            Lexema = string.Empty;
        }

        protected void ComillaDoble(string Archivo)
        {
            char c;
            //int Estado = 0;
            string Lexema = string.Empty;

            if (Archivo[Cont] == '"')
            {
                Lexema += '"';
                Cont++;
                while (Cont < Archivo.Length)
                {
                    c = Archivo[Cont];

                    if (c == '\n' || c == '\r')
                    {
                        // Error: comilla no cerrada
                        MessageBox.Show("Error léxico: cadena sin cerrar en línea " + Linea);
                        return;
                    }

                    Lexema += c;
                    Cont++;

                    if (c == '"')
                    {
                        // cadena terminada correctamente
                        LstTokens.Add(new Tokens(Linea, Lexema, 303));
                        return;
                    }
                }

                // Si sale del while sin cerrar comilla
                MessageBox.Show("Error léxico: cadena sin cerrar en línea " + Linea);
            }
        }

        protected void ConstanteHexadecimal(string Archivo)
        {
            string Lexema = string.Empty;
            if (Archivo[Cont] == '0' && Cont + 1 < Archivo.Length && (Archivo[Cont + 1] == 'x' || Archivo[Cont + 1] == 'X'))
            {
                Lexema += Archivo[Cont++];
                Lexema += Archivo[Cont++];
                while (Cont < Archivo.Length && "0123456789abcdefABCDEF".Contains(Archivo[Cont]))
                {
                    Lexema += Archivo[Cont++];
                }
                if (Cont < Archivo.Length && "uUlL".Contains(Archivo[Cont]))
                {
                    Lexema += Archivo[Cont++];
                }
                LstTokens.Add(new Tokens(Linea, Lexema, 306));
            }
        }

        protected void ConstanteOctal(string Archivo)
        {
            string Lexema = string.Empty;
            if (Archivo[Cont] == '0')
            {
                Lexema += Archivo[Cont++];
                while (Cont < Archivo.Length && "01234567".Contains(Archivo[Cont]))
                {
                    Lexema += Archivo[Cont++];
                }
                LstTokens.Add(new Tokens(Linea, Lexema, 307));
            }
        }

        protected void ConstanteCaracter(string Archivo)
        {
            string Lexema = string.Empty;
            if (Archivo[Cont] == '\'')
            {
                Lexema += Archivo[Cont++];
                if (Archivo[Cont] == '\\')
                {
                    Lexema += Archivo[Cont++];
                    if (Cont < Archivo.Length)
                        Lexema += Archivo[Cont++];
                }
                else
                {
                    Lexema += Archivo[Cont++];
                }
                if (Archivo[Cont] == '\'')
                {
                    Lexema += Archivo[Cont++];
                    LstTokens.Add(new Tokens(Linea, Lexema, 308));
                }
                else
                {
                    MessageBox.Show($"Error léxico: carácter sin cerrar en línea {Linea}");
                }
            }
        }

    public List<Tokens> AnalisisLexico(string Archivo)
    {
        char c;
        while (Cont < Archivo.Length)
        {
            c = Archivo[Cont];
            if ("\n\r".Contains(c)) { Linea++; Cont++; }
            else if (c == '/') { Lexema += c; GrDiagonal(Archivo); Lexema = string.Empty; }
            else if (c == '\'') { ConstanteCaracter(Archivo); }
            else if (c == '0' && Cont + 1 < Archivo.Length && (Archivo[Cont + 1] == 'x' || Archivo[Cont + 1] == 'X')) { ConstanteHexadecimal(Archivo); }
            else if (c == '0' && Cont + 1 < Archivo.Length && "01234567".Contains(Archivo[Cont + 1])) { ConstanteOctal(Archivo); }
            else if (char.IsDigit(c) || c == '+' || c == '-' || c == '.') { AutomataNumeros(Archivo); }
            else if ("\t\0 ".Contains(c)) { Cont++; }
            else if (c == '_' || char.IsLetter(c)) { IdentificadorPalabraReservada(Archivo); }
            else if ("(){}[],;:#".Contains(c)) { Lexema += c; LstTokens.Add(new Tokens(Linea, Lexema, UL.GetTokenSimbolo(Lexema))); Lexema = string.Empty; Cont++; }
            else if ("*^=!&".Contains(c)) { Lexema += c; GrAutomataDosSimbolos(Archivo); }
            else if (c == '"') { ComillaDoble(Archivo); }
            else { Lexema += c; LstTokens.Add(new Tokens(Linea, Lexema, UL.GetTokenSimbolo(Lexema))); Lexema = string.Empty; Cont++; }
        }
        return LstTokens;
    }
}
    }

    public class AnalizadorSintactico
    {
        List<Tokens> ListaTokens = new List<Tokens>();
        int Cont = 0;
        int Token;
        bool Compilacion = true;
        UnidadesLexica UL = new UnidadesLexica();
        int Linea = 1;
        protected bool Parea(int Tk, string Lexema)
        {
            if (Cont < ListaTokens.Count && ListaTokens[Cont].Token == Tk)
            {
                Cont++;
                return true;
            }
            else
            {
                MessageBox.Show("Error sintáctico: Se esperaba '" + Lexema + "' en la línea " + ListaTokens[Cont].Linea);
                Compilacion = false;
                return false;
            }
        }


    protected void gramatica_expresion()
    {
        gramatica_termino();
        gramatica_expresionPrima();
    }

    protected void gramatica_expresionPrima()
    {
        int Tk = ListaTokens[Cont].Token;
        if (Tk == UL.GetTokenSimbolo("+") || Tk == UL.GetTokenSimbolo("-"))
        {
            Parea(Tk, "operador suma/resta");
            gramatica_termino();
            gramatica_expresionPrima();
        }
        // Producción vacía (ε) si no hay + o -
    }

    protected void gramatica_termino()
    {
        gramatica_factor();
        gramatica_terminoPrima();
    }

    protected void gramatica_terminoPrima()
    {
        int Tk = ListaTokens[Cont].Token;
        if (Tk == UL.GetTokenSimbolo("*") || Tk == UL.GetTokenSimbolo("/"))
        {
            Parea(Tk, "operador multiplicación/división");
            gramatica_factor();
            gramatica_terminoPrima();
        }
        // Producción vacía (ε)
    }

    protected void gramatica_factor()
    {
        int Tk = ListaTokens[Cont].Token;

        if (Tk == 305) // Identificador
        {
            gramatica_identificador();
        }
        else if (Tk == 301 || Tk == 302 || Tk == 303 || Tk == 306 || Tk == 307 || Tk == 308)
        {
            gramatica_constante();
        }
        else if (Tk == UL.GetTokenSimbolo("("))
        {
            Parea(Tk, "(");
            gramatica_expresion();
            Parea(UL.GetTokenSimbolo(")"), ")");
        }
        else
        {
            MessageBox.Show($"Error sintáctico en línea {Linea}: se esperaba un factor");
        }
    }

    protected void gramatica_identificador()
    {
        if (ListaTokens[Cont].Token == 305)
        {
            Parea(305, "identificador");
        }
        else
        {
            MessageBox.Show($"Error sintáctico en línea {Linea}: se esperaba un identificador");
        }
    }

    protected void gramatica_constante()
    {
        int Tk = ListaTokens[Cont].Token;
        if (Tk == 301)
        {
            Parea(301, "constante entera");
        }
        else if (Tk == 302)
        {
            Parea(302, "constante flotante");
        }
        else if (Tk == 303)
        {
            Parea(303, "constante cadena");
        }
        else if (Tk == 306)
        {
            Parea(306, "constante hexadecimal");
        }
        else if (Tk == 307)
        {
            Parea(307, "constante octal");
        }
        else if (Tk == 308)
        {
            Parea(308, "constante carácter");
        }
        else
        {
            MessageBox.Show($"Error sintáctico en línea {Linea}: constante no reconocida");
        }
    }

    protected void gramatica_declaracion()
    {
        if (ListaTokens[Cont].Lexema == "int" || ListaTokens[Cont].Lexema == "float" || ListaTokens[Cont].Lexema == "char" || ListaTokens[Cont].Lexema == "void")
        {
            Parea(ListaTokens[Cont].Token, "tipo de dato");
            gramatica_identificador();

            if (ListaTokens[Cont].Lexema == "=")
            {
                Parea(UL.GetTokenSimbolo("="), "=");
                gramatica_expresion();
            }

            while (ListaTokens[Cont].Lexema == ",")
            {
                Parea(UL.GetTokenSimbolo(","), ",");
                gramatica_identificador();

                if (ListaTokens[Cont].Lexema == "=")
                {
                    Parea(UL.GetTokenSimbolo("="), "=");
                    gramatica_expresion();
                }
            }

            Parea(UL.GetTokenSimbolo(";"), ";");
        }
        else
        {
            MessageBox.Show($"Error sintáctico en línea {Linea}: se esperaba un tipo de dato para declaración");
        }
    }

    protected void gramatica_return()
    {
        Parea(UL.GetTokenSimbolo("return"), "return");
        gramatica_expresion();
        Parea(UL.GetTokenSimbolo(";"), ";");
    }

    protected void gramatica_asignacion()
    {
        gramatica_identificador();
        Parea(UL.GetTokenSimbolo("="), "=");
        gramatica_expresion();
        Parea(UL.GetTokenSimbolo(";"), ";");
    }

    protected void gramatica_if()
    {
        Parea(UL.GetTokenSimbolo("if"), "if");
        Parea(UL.GetTokenSimbolo("("), "(");
        gramatica_expresion();
        Parea(UL.GetTokenSimbolo(")"), ")");
        gramatica_bloque();
        if (ListaTokens[Cont].Lexema == "else")
        {
            Parea(UL.GetTokenSimbolo("else"), "else");
            gramatica_bloque();
        }
    }

    protected void gramatica_while()
    {
        Parea(UL.GetTokenSimbolo("while"), "while");
        Parea(UL.GetTokenSimbolo("("), "(");
        gramatica_expresion();
        Parea(UL.GetTokenSimbolo(")"), ")");
        gramatica_bloque();
    }

    protected void gramatica_for()
    {
        Parea(UL.GetTokenSimbolo("for"), "for");
        Parea(UL.GetTokenSimbolo("("), "(");
        gramatica_asignacion();
        gramatica_expresion();
        Parea(UL.GetTokenSimbolo(";"), ";");
        gramatica_asignacion();
        Parea(UL.GetTokenSimbolo(")"), ")");
        gramatica_bloque();
    }

    protected void gramatica_funcion()
    {
        gramatica_identificador();
        Parea(UL.GetTokenSimbolo("("), "(");
        if (ListaTokens[Cont].Lexema != ")")
        {
            gramatica_identificador();
            while (ListaTokens[Cont].Lexema == ",")
            {
                Parea(UL.GetTokenSimbolo(","), ",");
                gramatica_identificador();
            }
        }
        Parea(UL.GetTokenSimbolo(")"), ")");
        gramatica_bloque();
    }

    protected void gramatica_bloque()
    {
        if (ListaTokens[Cont].Lexema == "{")
        {
            Parea(UL.GetTokenSimbolo("{"), "{");
            while (ListaTokens[Cont].Lexema != "}")
            {
                gramatica_sentencia();
            }
            Parea(UL.GetTokenSimbolo("}"), "}");
        }
        else
        {
            gramatica_sentencia();
        }
    }

    protected void gramatica_sentencia()
    {
        string lexema = ListaTokens[Cont].Lexema;
        if (lexema == "int" || lexema == "float" || lexema == "char" || lexema == "void")
        {
            gramatica_declaracion();
        }
        else if (lexema == "if")
        {
            gramatica_if();
        }
        else if (lexema == "while")
        {
            gramatica_while();
        }
        else if (lexema == "for")
        {
            gramatica_for();
        }
        else if (lexema == "return")
        {
            gramatica_return();
        }
        else if (ListaTokens[Cont].Token == 305)
        {
            int siguiente = ListaTokens[Cont + 1].Token;
            if (siguiente == UL.GetTokenSimbolo("("))
                gramatica_funcion();
            else
                gramatica_asignacion();
        }
        else
        {
            MessageBox.Show($"Error sintáctico en línea {Linea}: sentencia no válida");
        }
    }

    public void Analizar()
    {
        Cont = 0;
        Linea = 1;
        while (Cont < ListaTokens.Count)
        {
            gramatica_sentencia();
        }
        MessageBox.Show("Análisis sintáctico completado correctamente.");
    }


    protected void gramatica_cadena_constante()
        {
            // Se espera una cadena entre comillas dobles
            if (Parea(303, "cadena constante"))
            {
                // cadena válida, se puede hacer algo adicional si es necesario
            }
        }

        // Bloque de sentencias que solo puede tener cadenas constantes
        protected void gramatica_bloque_sentencias()
        {
            if (Parea(79, "{")) // {
            {
                while (Cont < ListaTokens.Count && ListaTokens[Cont].Token != 80) // mientras no sea }
                {
                    int Tk = ListaTokens[Cont].Token;

                    switch (Tk)
                    {
                        case 303: // cadena constante
                            gramatica_cadena_constante();
                            break;
                        case 304: // comentario de bloque
                            gramatica_comentario();
                            break;
                        default:
                            MessageBox.Show("Error sintáctico: Sentencia no válida en la línea " + ListaTokens[Cont].Linea);
                            Compilacion = false;
                            return;
                    }
                }

                Parea(80, "}"); // cerramos bloque
            }
        }

        protected void gramatica_comentario()
        {
            if (Parea(304, "comentario de bloque"))
            {
                // comentario válido
            }
        }

        protected void gramatica_main()
        {
            if (Parea(220, "main"))
                if (Parea(75, "("))
                    if (Parea(76, ")"))
                        gramatica_bloque_sentencias();
        }

        public void AnalisisSintactico(List<Tokens> Lista)
        {
            ListaTokens = Lista;
            Cont = 0;
            Compilacion = true;

            while (Cont < ListaTokens.Count && Compilacion)
            {
                Token = ListaTokens[Cont].Token;

                switch (Token)
                {
                    case 30: // void
                        Cont++;
                        gramatica_main();
                        break;
                }
            }
        }
    }


