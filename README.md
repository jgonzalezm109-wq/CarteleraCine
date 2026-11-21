# Sistema de Cartelera de Cine (`CarteleraCine`)

Aplicación de consola desarrollada en **C# (.NET 8)** conectada a **SQL Server LocalDB** mediante **ADO.NET (`Microsoft.Data.SqlClient`)** para administrar las funciones de cartelera de un cine. El sistema permite registrar, listar, buscar por Id, actualizar precios y eliminar funciones utilizando consultas parametrizadas, separación por capas (Patrón Repositorio) y manejo controlado de excepciones.


## Estructura del proyecto

- `01_crear_CarteleraDB.sql`: Script SQL que crea la base de datos `CarteleraDB`, la tabla `Funciones` (`Id`, `Pelicula`, `Sala`, `Precio`) y precarga los registros iniciales (`Coco` y `Moana`).
- `Funcion.cs`: Clase de dominio que modela cada registro de la tabla `Funciones` y sobrescribe `ToString()` con el formato `Id. Pelicula (Sala) - Q Precio`.
- `FuncionRepositorio.cs`: Capa de acceso a datos que encapsula la cadena de conexión (`private const string`) y las operaciones CRUD (`Insertar`, `ListarTodos`, `BuscarPorId`, `Actualizar` y `Eliminar`) mediante consultas parametrizadas y bloques `using`.
- `Program.cs`: Capa de presentación con menú interactivo en `do-while`, validaciones de entrada (`TryParse` e `IsNullOrWhiteSpace`) y bloques `try/catch (SqlException)` para controlar errores de base de datos sin cerrar la aplicación.
- `capturas/`: Carpeta con las evidencias de ejecución del sistema.

## Requisitos previos

- **Visual Studio Community 2026** con carga de trabajo de desarrollo de escritorio / consola en **.NET 8.0**.
- **SQL Server LocalDB** (`(localdb)\MSSQLLocalDB`).
- Paquete NuGet **`Microsoft.Data.SqlClient`**.


## Pasos para ejecutar el proyecto

1. **Crear la base de datos y datos iniciales:**
   - Abre SQL Server Management Studio (SSMS) o el Explorador de objetos de SQL Server en Visual Studio conectado a `(localdb)\MSSQLLocalDB`.
   - Abre el archivo `01_crear_CarteleraDB.sql` y ejecútalo de una sola vez para crear `CarteleraDB`, la tabla `Funciones` y los registros iniciales.

2. **Abrir y compilar la solución:**
   - Abre el archivo de solución `CarteleraCine.sln` en **Visual Studio Community 2026**.
   - Verifica que el paquete NuGet `Microsoft.Data.SqlClient` esté restaurado en las dependencias del proyecto.

3. **Ejecutar la aplicación:**
   - Presiona `F5` o `Ctrl + F5` para iniciar el programa de consola y operar mediante el menú de opciones (`1` a `6`).
  
     
- **Curso:** Algoritmos (008) — Universidad Mariano Gálvez de Guatemala, Centro Universitario de Chiquimulilla.
