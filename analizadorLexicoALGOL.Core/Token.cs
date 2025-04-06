namespace LexicalAnalyzer.Core;

public class Token
{
    public string Lexema { get; set; }
    public string Categoria { get; set; }
    public string CodigoToken { get; set; }
    public int NumeroLinea { get; set; }
    public int Posicion { get; set; }

    public Token(string lexema, string categoria, string codigoToken, int numeroLinea, int posicion)
    {
        Lexema = lexema;
        Categoria = categoria;
        CodigoToken = codigoToken;
        NumeroLinea = numeroLinea;
        Posicion = posicion;
    }

    public override string ToString()
    {
        return $"{Lexema,-20} {Categoria,-20} {CodigoToken}";
    }
}
