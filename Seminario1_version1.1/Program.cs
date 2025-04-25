namespace Seminario1_version1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ejemplo 1: Método de bisección para la funcion x² - 2");
            Double root1 = RootFinder.FindRoot(
                function: x => x * x - 2,
                method: RootFinder.RootFindingMethod.Bisection,
                initialGuess: Tuple.Create(1.0, 2.0), // Intervalo [1, 2]
                tolerance: 1e-12);

            Console.WriteLine($"Raíz encontrada (bisección): {root1}");

            Console.WriteLine("Ejemplo 2: Método de Newton-Raphson para la funcion x² - 2");
            Double root2 = RootFinder.FindRoot(
                function: x => x * x - 2,
                method: RootFinder.RootFindingMethod.NewtonRaphson,
                initialGuess: Tuple.Create(1.5, 0.0), // Valor inicial 1.5 (el segundo valor no se usa en este método)
                derivative: x => 2 * x, // Derivada de la función x² - 2 = 2x
                tolerance: 1e-12);

            Console.WriteLine($"Raíz encontrada (Newton-Raphson): {root2}");
        }
    }
}
