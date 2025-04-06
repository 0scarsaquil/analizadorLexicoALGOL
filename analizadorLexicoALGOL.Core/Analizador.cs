using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LexicalAnalyzer.Core;

public class AnalizadorLexico
{
    private readonly TablaSimbolos _tablaSimbolos = new();
    private int _lineaActual = 1;
    private int _posicionActual = 1;
    private string _ultimaVariable = "";
    private string _ultimoValor = "";
    private string _ultimaPalabraReservada = "";

    public List<Token> Analizar(string codigo)
    {
        var tokens = new List<Token>();
        var codigoLimpio = EliminarComentarios(codigo);
        var lineas = codigoLimpio.Split('\n');

        for (int i = 0; i < lineas.Length; i++)
        {
            _lineaActual = i + 1;
            var lexemas = Tokenizar(lineas[i]);

            foreach (var lexema in lexemas)
            {
                if (string.IsNullOrWhiteSpace(lexema)) continue;

                var token = CrearToken(lexema);
                tokens.Add(token);

                if (lexema == ":=" && _ultimaVariable != "")
                {
                    _tablaSimbolos.ActualizarValor(_ultimaVariable, _ultimoValor, _lineaActual);
                }
            }
        }

        return tokens;
    }

    private string EliminarComentarios(string codigo)
    {
        return Regex.Replace(codigo, @"comment.*?;", string.Empty,
            RegexOptions.Singleline | RegexOptions.IgnoreCase);
    }

    private IEnumerable<string> Tokenizar(string codigo)
    {
        return Regex.Split(codigo, @"(\s+|;|,|\(|\)|\[|\]|:=|<=|>=|<>|[+\-*/=<>¬∧∨])");
    }

    private Token CrearToken(string lexema)
    {
        string categoria;
        string codigoToken;

        if (LexemasALGOL.EsPalabraReservada(lexema))
        {
            categoria = "Palabra Reservada";
            codigoToken = _tablaSimbolos.GenerarToken(categoria);
            _ultimaPalabraReservada = lexema.ToLower();
        }
        else if (LexemasALGOL.EsOperador(lexema))
        {
            categoria = "Operador";
            codigoToken = _tablaSimbolos.GenerarToken(categoria);
            if (lexema == ":=") _ultimoValor = ""; // Reset para nueva asignación
        }
        else if (LexemasALGOL.EsDelimitador(lexema))
        {
            categoria = "Delimitador";
            codigoToken = _tablaSimbolos.GenerarToken(categoria);
        }
        else if (Regex.IsMatch(lexema, @"^[a-zA-Z][a-zA-Z0-9]*$"))
        {
            categoria = "Identificador";
            bool esVariable = _ultimaPalabraReservada == "integer" ||
                            _ultimaPalabraReservada == "real" ||
                            _ultimaPalabraReservada == "boolean";

            if (esVariable)
            {
                _tablaSimbolos.RegistrarVariable(lexema, "", _lineaActual);
                codigoToken = _tablaSimbolos.ObtenerTokenVariable(lexema) ?? _tablaSimbolos.GenerarToken(categoria);
                _ultimaVariable = lexema;
            }
            else
            {
                codigoToken = _tablaSimbolos.GenerarToken(categoria);
            }

            _ultimaPalabraReservada = "";
        }
        else if (Regex.IsMatch(lexema, @"^\d+$"))
        {
            categoria = "Literal";
            codigoToken = _tablaSimbolos.GenerarToken(categoria);
            _ultimoValor = lexema;
        }
        else
        {
            categoria = "Error Léxico";
            codigoToken = _tablaSimbolos.GenerarToken(categoria);
        }

        var token = new Token(lexema, categoria, codigoToken, _lineaActual, _posicionActual);
        _posicionActual += lexema.Length;
        return token;
    }

    public List<(string Lexema, string Valor, int Linea, string Token)> ObtenerHistorialSimbolos() =>
        _tablaSimbolos.ObtenerHistorial();
}