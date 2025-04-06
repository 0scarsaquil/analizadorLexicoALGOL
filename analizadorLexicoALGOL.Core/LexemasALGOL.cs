using System.Collections.Generic;

namespace LexicalAnalyzer.Core;

public static class LexemasALGOL
{
    public static readonly HashSet<string> PalabrasReservadas = new()
    {
        "begin", "end", "if", "then", "else", "for", "do", "while", 
        "procedure", "function", "var", "integer", "real", "boolean",
        "array", "of", "goto", "label", "switch", "case", "comment"
    };

    public static readonly HashSet<string> Operadores = new()
    {
        "+", "-", "*", "/", ":=", "=", "<>", "<", "<=", ">", ">=", "¬", "∧", "∨"
    };

    public static readonly HashSet<string> Delimitadores = new()
    {
        ";", ",", "(", ")", "[", "]", ".", ":", "'", "\""
    };

    public static bool EsPalabraReservada(string lexema) => PalabrasReservadas.Contains(lexema.ToLower());
    public static bool EsOperador(string lexema) => Operadores.Contains(lexema);
    public static bool EsDelimitador(string lexema) => Delimitadores.Contains(lexema);
}
