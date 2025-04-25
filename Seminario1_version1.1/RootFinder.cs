using System;

/// <summary>
/// Class that provides methods to find zeros (roots) of real functions.
/// </summary>
public class RootFinder
{
    /// <summary>
    /// Default tolerance for the convergence of the methods.
    /// </summary>
    private const double DefaultTolerance = 1e-10;

    /// <summary>
    /// Default maximum number of iterations.
    /// </summary>
    private const int MaxIterations = 1000;

    /// <summary>
    /// List of available methods to find roots.
    /// </summary>
    public enum RootFindingMethod
    {
        Bisection,
        NewtonRaphson
    }

    /// <summary>
    /// Find a zero of the function using the specified method.
    /// </summary>
    /// <param name="function">Function whose zero is to be found.</param>
    /// <param name="method">Method to be used to find the zero.</param>
    /// <param name="initialGuess">Initial value or initial interval depending on the method.</param>
    /// <param name="derivative">Derivative of the function (only required for Newton-Raphson).</param>
    /// <param name="tolerance">Tolerance for convergence.</param>
    /// <param name="maxIterations">Maximum number of allowed iterations.</param>
    /// <returns>An approximation of the zero of the function.</returns>
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
    /// Bisection method to find zeros of functions.
    /// </summary>
    /// <param name="function">Function whose zero is to be found.</param>
    /// <param name="a">Left endpoint of the initial interval.</param>
    /// <param name="b">Right endpoint of the initial interval.</param>
    /// <param name="tolerance">Tolerance for convergence.</param>
    /// <param name="maxIterations">Maximum number of allowed iterations.</param>
    /// <returns>An approximation of the zero of the function.</returns>
    private static double BisectionMethod(
        Func<double, double> function,
        double a,
        double b,
        double tolerance,
        int maxIterations)
    {
        // Verify Bolzano's theorem (necessary condition for the bisection method)
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
    /// Newton-Raphson method for finding function zeros.
    /// </summary>
    /// <param name="function">A function whose zero is to be found.</param>
    /// <param name="derivative">Derived from function.</param>
    /// <param name="initialGuess">Initial approach of zero.</param>
    /// <param name="tolerance">Tolerance for convergence.</param>
    /// <param name="maxIterations">Maximum number of iterations allowed.</param>
    /// <returns>An approximation of the zero of the function.</returns>
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