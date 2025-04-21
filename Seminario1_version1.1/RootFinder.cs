using System;

/// <summary>
/// Clase que proporciona métodos para encontrar ceros (raíces) de funciones reales.
/// </summary>
public class RootFinder
{
    /// <summary>
    /// Tolerancia por defecto para la convergencia de los métodos.
    /// </summary>
    private const double DefaultTolerance = 1e-10;

    /// <summary>
    /// Número máximo de iteraciones por defecto.
    /// </summary>
    private const int MaxIterations = 1000;

    /// <summary>
    /// Enumeración de los métodos disponibles para encontrar raíces.
    /// </summary>
    public enum RootFindingMethod
    {
        Bisection,
        NewtonRaphson
    }

    /// <summary>
    /// Encuentra un cero de la función utilizando el método especificado.
    /// </summary>
    /// <param name="function">Función cuyo cero se desea encontrar.</param>
    /// <param name="method">Método a utilizar para encontrar el cero.</param>
    /// <param name="initialGuess">Valor inicial o intervalo inicial dependiendo del método.</param>
    /// <param name="derivative">Derivada de la función (solo requerida para Newton-Raphson).</param>
    /// <param name="tolerance">Tolerancia para la convergencia.</param>
    /// <param name="maxIterations">Número máximo de iteraciones permitidas.</param>
    /// <returns>Una aproximación del cero de la función.</returns>
    public static double FindRoot(
        Func<double, double> function,
        RootFindingMethod method,
        Tuple<double, double> initialGuess,
        Func<double, double> derivative = null,
        double tolerance = DefaultTolerance,
        int maxIterations = MaxIterations)
    {
        return method switch
        {
            RootFindingMethod.Bisection => BisectionMethod(
                function,
                initialGuess.Item1,
                initialGuess.Item2,
                tolerance,
                maxIterations),

            RootFindingMethod.NewtonRaphson => NewtonRaphsonMethod(
                function,
                derivative ?? throw new ArgumentNullException(nameof(derivative)),
                initialGuess.Item1,
                tolerance,
                maxIterations),

            _ => throw new ArgumentOutOfRangeException(nameof(method), "Método no soportado")
        };
    }

    /// <summary>
    /// Método de bisección para encontrar ceros de funciones.
    /// </summary>
    /// <param name="function">Función cuyo cero se desea encontrar.</param>
    /// <param name="a">Extremo izquierdo del intervalo inicial.</param>
    /// <param name="b">Extremo derecho del intervalo inicial.</param>
    /// <param name="tolerance">Tolerancia para la convergencia.</param>
    /// <param name="maxIterations">Número máximo de iteraciones permitidas.</param>
    /// <returns>Una aproximación del cero de la función.</returns>
    private static double BisectionMethod(
        Func<double, double> function,
        double a,
        double b,
        double tolerance,
        int maxIterations)
    {
        // Verificar el teorema de Bolzano (condición necesaria para el método de bisección)
        if (function(a) * function(b) >= 0)
        {
            throw new ArgumentException("La función debe tener signos opuestos en los extremos del intervalo.");
        }

        double c = a;
        int iterations = 0;

        while ((b - a) / 2 > tolerance && iterations < maxIterations)
        {
            c = (a + b) / 2;

            if (Math.Abs(function(c)) < tolerance)
            {
                break;
            }
            else if (function(c) * function(a) < 0)
            {
                b = c;
            }
            else
            {
                a = c;
            }

            iterations++;
        }

        if (iterations >= maxIterations)
        {
            Console.WriteLine("Advertencia: Se alcanzó el número máximo de iteraciones.");
        }

        return c;
    }

    /// <summary>
    /// Método de Newton-Raphson para encontrar ceros de funciones.
    /// </summary>
    /// <param name="function">Función cuyo cero se desea encontrar.</param>
    /// <param name="derivative">Derivada de la función.</param>
    /// <param name="initialGuess">Aproximación inicial del cero.</param>
    /// <param name="tolerance">Tolerancia para la convergencia.</param>
    /// <param name="maxIterations">Número máximo de iteraciones permitidas.</param>
    /// <returns>Una aproximación del cero de la función.</returns>
    private static double NewtonRaphsonMethod(
        Func<double, double> function,
        Func<double, double> derivative,
        double initialGuess,
        double tolerance,
        int maxIterations)
    {
        double x = initialGuess;
        double xOld;
        int iterations = 0;

        do
        {
            double fx = function(x);
            double dfx = derivative(x);

            if (Math.Abs(dfx) < tolerance)
            {
                throw new InvalidOperationException("Derivada cercana a cero. El método no puede continuar.");
            }

            xOld = x;
            x = x - fx / dfx;
            iterations++;

        } while (Math.Abs(x - xOld) > tolerance && iterations < maxIterations);

        if (iterations >= maxIterations)
        {
            Console.WriteLine("Advertencia: Se alcanzó el número máximo de iteraciones.");
        }

        return x;
    }
}