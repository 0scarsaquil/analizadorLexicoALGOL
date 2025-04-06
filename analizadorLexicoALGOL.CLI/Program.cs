using LexicalAnalyzer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalyzer.CLI
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Analizador Léxico de ALGOL");
            Console.WriteLine("Ingrese su código (escriba 'FIN' en una nueva línea para terminar):");

            var codigo = LeerEntradaMultilinea();
            var analizador = new AnalizadorLexico();
            var tokens = analizador.Analizar(codigo);
            var historialSimbolos = analizador.ObtenerHistorialSimbolos();

            Console.WriteLine("\nResultados del Análisis Léxico:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Lexema".PadRight(20) + "Categoría".PadRight(20) + "Token");
            Console.WriteLine("--------------------------------------------------");
            
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            Console.WriteLine("\nTabla de Símbolos:");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Lexema".PadRight(20) + "Valor".PadRight(15) + "Línea".PadRight(10) + "Token");
            Console.WriteLine("-----------------------------------------------------------");

            foreach (var entrada in historialSimbolos)
            {
                Console.WriteLine($"{entrada.Lexema.PadRight(20)}{entrada.Valor.PadRight(15)}{entrada.Linea.ToString().PadRight(10)}{entrada.Token}");
            }

        }

        static string LeerEntradaMultilinea()
        {
            var entrada = new StringBuilder();
            string linea;
            
            while ((linea = Console.ReadLine()) != null && linea.Trim().ToUpper() != "FIN")
            {
                entrada.AppendLine(linea);
            }

            return entrada.ToString();
        }
    }
}
