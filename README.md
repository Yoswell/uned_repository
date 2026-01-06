### UNED - Recursos de Apoyo Virtual

Repositorio oficial de apoyo para estudiantes de Informática – UNED Costa Rica.
Este repositorio está diseñado para centralizar ejemplos prácticos de código, material de estudio estructurado y recursos complementarios que apoyen el aprendizaje en asignaturas de programación, redes, seguridad y administración de sistemas.

### Objetivo del repositorio

Brindar a los estudiantes una colección organizada y práctica de recursos que les permita:

* Comprender y aplicar conceptos técnicos mediante ejemplos reales.
* Practicar habilidades de línea de comandos, desarrollo y administración.
* Preparar material para exámenes, proyectos y laboratorios.
* Contar con una referencia rápida para comandos y flujos de trabajo comunes.

-----

### Comandos de compilación

```bash
dotnet clean
# Limpia los archivos binarios y temporales generados en bin/ y obj/

dotnet restore
# Restaura las dependencias NuGet definidas en el .csproj

dotnet build
# Compila el proyecto o solución actual y genera binarios

dotnet run
# Compila (si es necesario) y ejecuta el proyecto actual

dotnet build --configuration Release
# Compila en modo Release para producción

dotnet publish -c Release -o ./publish
# Publica el proyecto compilado en la carpeta ./publish
```

-----

### Crear solución (sln)

```bash
dotnet new sln -n MiSolucion
# Crea un nuevo archivo de solución con el nombre MiSolucion.sln

dotnet sln MiSolucion.sln add ./Proyecto/Proyecto.csproj
# Agrega un proyecto existente a la solución especificada

dotnet sln list
# Lista todos los proyectos agregados a la solución actual

dotnet sln MiSolucion.sln remove ./Proyecto/Proyecto.csproj
# Elimina un proyecto de la solución sin borrarlo del disco
```

-----

### Crear proyectos

```bash
dotnet new console -n MiAppConsola
# Crea un nuevo proyecto de aplicación de consola

dotnet new mvc -n MiAppMVC
# Crea un nuevo proyecto ASP.NET Core MVC con Razor

dotnet new webapi -n MiAppAPI
# Crea un nuevo proyecto ASP.NET Core Web API

dotnet new classlib -n MiLibreria
# Crea un nuevo proyecto de librería de clases (DLL)

dotnet new blazorwasm -n MiAppBlazor
# Crea una aplicación Blazor WebAssembly

dotnet new xunit -n MisTests
# Crea un proyecto de pruebas unitarias con xUnit
```

-----

### Referencias entre proyectos

```bash
dotnet add ./MiAppMVC/MiAppMVC.csproj reference ./MiLibreria/MiLibreria.csproj
# Agrega una referencia desde el proyecto MVC hacia la librería

dotnet add ./MiAppAPI/MiAppAPI.csproj reference ./MiLibreria/MiLibreria.csproj
# Agrega una referencia desde el proyecto API hacia la librería

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
# Agrega un paquete NuGet al proyecto actual
```

-----

### Ejecutar o compilar proyectos específicos sin (cd)

```bash
dotnet run --project ./MiAppConsola/MiAppConsola.csproj
# Ejecuta un proyecto específico dentro de una solución

dotnet build ./MiAppAPI/MiAppAPI.csproj
# Compila un proyecto específico

dotnet test ./MisTests/MisTests.csproj
# Ejecuta las pruebas del proyecto especificado
```

-----

### Comandos de base de datos (SQL Server)

```sql
sqlcmd -S localhost -i ./database/CreateTables.sql
-- Ejecuta el script de creación de tablas

sqlcmd -S localhost -i ./database/InsertSampleData.sql
-- Ejecuta el script de inserción de datos de ejemplo

-- Para proyectos escolares/restaurante
sqlcmd -S localhost -Q "CREATE DATABASE EscuelaDB"
-- Crea la base de datos para el proyecto escolar

sqlcmd -S localhost -Q "CREATE DATABASE RestauranteDB"
-- Crea la base de datos para el proyecto restaurante

sqlcmd -S localhost -Q "SELECT @@VERSION"
-- Muestra la versión de SQL Server
```

-----

### Comandos Git para control de versiones

```bash
git init
# Inicializa un repositorio Git

git add .
# Agrega todos los archivos al staging area

git commit -m "Initial commit"
# Crea un commit con los cambios

git branch -M main
# Renombra la rama principal a 'main'

git remote add origin <url>
# Agrega el repositorio remoto

git push -u origin main
# Sube los cambios al repositorio remoto

git clone <url>
# Clona un repositorio existente

git status
# Muestra el estado del repositorio

git log --oneline
# Muestra el historial de commits en una línea
```

-----

### Comandos útiles para desarrollo

```bash
tree /f
# Muestra la estructura de archivos y carpetas

find . -name "*.cs" -type f
# Encuentra todos los archivos .cs en el directorio actual

find . -name "*.cs" -exec wc -l {} + | tail -1
# Cuenta las líneas totales de código en archivos C#

dotnet restore && dotnet build && dotnet run
# Restaura dependencias, compila y ejecuta en secuencia

set ASPNETCORE_ENVIRONMENT=Development
# Configura el entorno para ASP.NET Core

dotnet run --environment Development
# Ejecuta en modo desarrollo
```

-----

### Comandos para debugging y troubleshooting

```bash
dotnet --info
# Muestra información sobre el SDK y runtime instalados

dotnet --list-sdks
# Lista los SDKs instalados

dotnet --list-runtimes
# Lista los runtimes instalados

dotnet nuget locals all --clear
# Limpia todas las cachés locales de NuGet

dotnet list package
# Lista los paquetes NuGet del proyecto

dotnet list package --outdated
# Muestra paquetes desactualizados

dotnet add package <PackageName> --version <Version>
# Actualiza un paquete específico
```

-----

### Comandos específicos para proyectos escolares y restaurante

```bash
# Para proyecto escolar
cd uned/progra_avanzada/proyectos/proyecto_escolar
dotnet build
dotnet run

# Para proyecto restaurante
cd uned/progra_avanzada/proyectos/proyecto_restaurante
dotnet build
dotnet run

# Ejecutar scripts de BD para ambos proyectos
sqlcmd -S localhost -i uned/progra_avanzada/proyectos/proyecto_escolar/database/CreateTables.sql
sqlcmd -S localhost -i uned/progra_avanzada/proyectos/proyecto_restaurante/database/CreateTables.sql
```

------

**Made with love by VIsh0k**