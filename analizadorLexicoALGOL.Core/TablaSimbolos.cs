using System.Collections.Generic;

namespace LexicalAnalyzer.Core;

public class TablaSimbolos
{
    private readonly List<(string Lexema, string Valor, int Linea, string Token)> _historial = new();
    private readonly Dictionary<string, string> _tokensVariables = new();

    private int contadorIdentificadores = 1;
    private int contadorReservadas = 1;
    private int contadorLiterales = 1;
    private int contadorOperadores = 1;
    private int contadorDelimitadores = 1;
    private int contadorErrores = 1;

    public string GenerarToken(string categoria)
    {
        return categoria switch
        {
            "Identificador" => $"x100{contadorIdentificadores++.ToString("D3")}",
            "Palabra Reservada" => $"x200{contadorReservadas++.ToString("D3")}",
            "Literal" => $"x300{contadorLiterales++.ToString("D3")}",
            "Operador" => $"x400{contadorOperadores++.ToString("D3")}",
            "Delimitador" => $"x500{contadorDelimitadores++.ToString("D3")}",
            "Error Léxico" => $"x600{contadorErrores++.ToString("D3")}",
            _ => $"x999999"
        };
    }

    public void RegistrarVariable(string lexema, string valor, int linea)
    {
        if (!_tokensVariables.ContainsKey(lexema))
        {
            string token = $"x100{contadorIdentificadores++.ToString("D3")}";
            _tokensVariables[lexema] = token;
        }
        _historial.Add((lexema, valor, linea, _tokensVariables[lexema]));
    }

    public void ActualizarValor(string lexema, string valor, int linea)
    {
        if (_tokensVariables.ContainsKey(lexema))
        {
            _historial.Add((lexema, valor, linea, _tokensVariables[lexema]));
        }
    }

    public string? ObtenerTokenVariable(string lexema) =>
        _tokensVariables.TryGetValue(lexema, out var token) ? token : null;

    public List<(string Lexema, string Valor, int Linea, string Token)> ObtenerHistorial() =>
        new(_historial);
}