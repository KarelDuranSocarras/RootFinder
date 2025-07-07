# RootFinder

Este proyecto, llamado **RootFinder** [README.md], es una aplicación diseñada para **encontrar las raíces (o ceros) de funciones reales**. Proporciona una clase `RootFinder` que implementa dos métodos numéricos populares para este propósito: el **Método de Bisección** y el **Método de Newton-Raphson**.

## Funcionalidades

La clase `RootFinder` contiene la lógica principal para el cálculo de raíces.

### 1. Método de Bisección

*   **Descripción**: Este método es utilizado para encontrar ceros de funciones.
*   **Requisitos**: Requiere un intervalo inicial `[a, b]`. Es **fundamental que la función tenga signos opuestos** en los extremos de este intervalo (es decir, `f(a) * f(b) < 0`), lo cual verifica el Teorema de Bolzano. Si esta condición no se cumple, se lanzará una `ArgumentException`.
*   **Parámetros**: Toma la función (`Func<double, double> function`), el extremo izquierdo (`double a`) y derecho (`double b`) del intervalo inicial, una tolerancia (`double tolerance`) y el número máximo de iteraciones (`int maxIterations`).
*   **Funcionamiento**: El método reduce iterativamente el intervalo, aproximándose a la raíz. Se detiene cuando la longitud del subintervalo es menor que la tolerancia o se alcanza el número máximo de iteraciones.

### 2. Método de Newton-Raphson

*   **Descripción**: Este método también sirve para encontrar ceros de funciones.
*   **Requisitos**: Necesita un valor de aproximación inicial (`double initialGuess`) y la **derivada de la función** (`Func<double, double> derivative`).
*   **Parámetros**: Toma la función (`Func<double, double> function`), su derivada (`Func<double, double> derivative`), una aproximación inicial (`double initialGuess`), una tolerancia (`double tolerance`) y el número máximo de iteraciones (`int maxIterations`).
*   **Funcionamiento**: Utiliza la tangente en el punto actual para encontrar una nueva y mejor aproximación a la raíz. Si la derivada se acerca a cero, se lanza una `InvalidOperationException`. El proceso continúa hasta que la diferencia entre aproximaciones sucesivas es menor que la tolerancia o se alcanza el máximo de iteraciones.

### Parámetros por Defecto

Ambos métodos utilizan parámetros por defecto si no se especifican al llamar a `FindRoot`:
*   **Tolerancia por defecto**: `1e-10`.
*   **Número máximo de iteraciones**: `1000`.

## Estructura del Proyecto

La estructura del directorio del proyecto es la siguiente:

```
└── karelduransocarras-rootfinder/
    ├── README.md
    ├── Seminario1_version1.1.sln
    └── Seminario1_version1.1/
        ├── Program.cs
        ├── RootFinder.cs
        └── Seminario1_version1.1.csproj
```

*   `Seminario1_version1.1.sln`: Archivo de solución de Visual Studio.
*   `Seminario1_version1.1/`: Directorio principal del proyecto.
    *   `Program.cs`: Contiene el punto de entrada de la aplicación y **ejemplos de uso** de la clase `RootFinder`.
    *   `RootFinder.cs`: Define la clase `RootFinder` con los métodos de bisección y Newton-Raphson.
    *   `Seminario1_version1.1.csproj`: Archivo de proyecto C#.

## Requisitos

Para compilar y ejecutar este proyecto, necesitas:

*   **Microsoft Visual Studio**: Versión 17 o superior.
*   **.NET**: El proyecto está configurado para usar `net8.0` [Seminario1_version1.1.csproj].

## Uso / Ejemplos

El archivo `Program.cs` demuestra cómo utilizar los métodos `Bisection` y `NewtonRaphson` para encontrar la raíz de la función **`x² - 2`**.

### Ejemplo 1: Método de Bisección

Encuentra la raíz de `x² - 2` en el intervalo `` con una tolerancia de `1e-12`.

```csharp
// Ejemplo 1: Método de bisección para la funcion x² - 2
Double root1 = RootFinder.FindRoot(
    function: x => x * x - 2,
    method: RootFinder.RootFindingMethod.Bisection,
    initialGuess: Tuple.Create(1.0, 2.0), // Intervalo
    tolerance: 1e-12);
Console.WriteLine($"Raíz encontrada (bisección): {root1}");
```

### Ejemplo 2: Método de Newton-Raphson

Encuentra la raíz de `x² - 2` con un valor inicial de `1.5` y su derivada `2x`, con una tolerancia de `1e-12`.

```csharp
// Ejemplo 2: Método de Newton-Raphson para la funcion x² - 2
Double root2 = RootFinder.FindRoot(
    function: x => x * x - 2,
    method: RootFinder.RootFindingMethod.NewtonRaphson,
    initialGuess: Tuple.Create(1.5, 0.0), // Valor inicial 1.5 (el segundo valor no se usa en este método)
    derivative: x => 2 * x, // Derivada de la función x² - 2 = 2x
    tolerance: 1e-12);
Console.WriteLine($"Raíz encontrada (Newton-Raphson): {root2}");
```
