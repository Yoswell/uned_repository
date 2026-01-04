### Proyecto Escolar - Gestión de Estudiantes y Cursos

Este proyecto es una aplicación de consola en C# que demuestra el manejo de datos utilizando SQL Server y ADO.NET. Implementa un sistema básico de gestión escolar con estudiantes, cursos, profesores y matrículas.

-----

### Arquitectura

El proyecto sigue una arquitectura en capas:

* **Models**: Clases de entidad (Student, Course, Teacher, Enrollment)
* **DataAccess**: Repositorios para acceso a datos con ADO.NET
* **Services**: Lógica de negocio (EnrollmentService)
* **Program**: Interfaz de usuario de consola

-----

### Base de Datos

#### Configuración

1. Ejecutar `Database/CreateTables.sql` para crear la base de datos y tablas.
2. Ejecutar `Database/InsertSampleData.sql` para insertar datos de ejemplo.

#### Esquema

* **Estudiantes**: Información de estudiantes
* **Profesores**: Información de profesores
* **Cursos**: Información de cursos con profesor asignado
* **Matriculas**: Relación entre estudiantes y cursos con calificaciones

-----

### Funcionalidades

* CRUD completo para estudiantes y cursos
* Matriculación de estudiantes en cursos
* Asignación de calificaciones
* Validaciones de negocio

-----

### Ejemplos de Código

#### Conexión a Base de Datos

```csharp
using (SqlConnection conn = DatabaseHelper.GetConnection()) {
    // Operaciones de BD
}
```

#### Consulta con Parámetros

```csharp
string query = "SELECT * FROM Estudiantes WHERE EstudianteID = @id";
SqlCommand cmd = new SqlCommand(query, conn);
cmd.Parameters.AddWithValue("@id", studentId);
```

#### Manejo de Excepciones

```csharp
try {
    // Operación
} catch (SqlException ex) {
    Console.WriteLine($"Error de BD: {ex.Message}");
}
```

-----

### Compilación y Ejecución

1. Asegurarse de tener .NET SDK instalado.
2. Configurar la cadena de conexión en `DataAccess/DatabaseHelper.cs`.
3. Compilar: `dotnet build`
4. Ejecutar: `dotnet run`

### Temas Cubiertos

* Programación Orientada a Objetos
* ADO.NET y SQL Server
* Manejo de Excepciones
* Genéricos y Colecciones
* Delegados y Eventos (en ejemplos adicionales)
* Programación Asíncrona (en ejemplos adicionales)

-----

### Seguridad en Aplicaciones Escolares - Consideraciones Críticas

#### Inyección SQL

```csharp
// VULNERABLE
string query = $"SELECT * FROM Estudiantes WHERE Nombre = '{userInput}'";

// SEGURO
string query = "SELECT * FROM Estudiantes WHERE Nombre = @nombre";
SqlCommand cmd = new SqlCommand(query, conn);
cmd.Parameters.AddWithValue("@nombre", userInput);
```

#### Comando del Sistema

```csharp
// VULNERABLE
Process.Start("cmd.exe", "/C " + userCommand);

// SEGURO
// Validar y sanitizar completamente la entrada del usuario
// Considerar alternativas sin ejecución de comandos del sistema
```

#### Manipulación de Archivos

```csharp
// VULNERABLE
string path = "uploads/" + fileName;
File.WriteAllText(path, content);

// SEGURO
string safeFileName = Path.GetFileName(fileName);
string fullPath = Path.Combine("uploads", safeFileName);
if (!Path.GetFullPath(fullPath).StartsWith(Path.GetFullPath("uploads/")))
    throw new SecurityException("Path traversal attempt");
```

-----

### Buenas Prácticas de Seguridad

#### Validación de Entrada

```csharp
public bool ValidateStudentInput(string name, string email) {
    // Validar longitud
    if (string.IsNullOrWhiteSpace(name) || name.Length > 100) return false;
    
    // Validar formato de email
    if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return false;
    
    // Sanitizar entrada
    name = System.Web.HttpUtility.HtmlEncode(name);
    
    return true;
}
```

#### Autenticación y Autorización

```csharp
[Authorize(Roles = "Administrator,Teacher")]
public ActionResult GradeStudent(int studentId, string grade) {
    // Verificar que el profesor tiene permiso para este curso
    if (!_authorizationService.CanGradeStudent(User.Identity.Name, studentId))
        return Forbid();
    
    // Lógica de negocio
}
```

#### Logging Seguro

```csharp
public class SecureLogger {
    public void LogStudentAction(string studentId, string action) {
        // No registrar información sensible
        string safeMessage = $"Student action: {action} by student ID";
        
        // Sanitizar antes de registrar
        safeMessage = safeMessage.Replace("\n", "").Replace("\r", "");
        
        _logger.Info(safeMessage);
    }
}
```

#### Manejo Seguro de Conexiones

```csharp
public class SecureDatabaseHelper {
    private static string GetConnectionString() {
        // Usar ConfigurationManager en lugar de cadenas hardcodeadas
        var builder = new SqlConnectionStringBuilder {
            DataSource = ConfigurationManager.AppSettings["DbServer"],
            InitialCatalog = ConfigurationManager.AppSettings["DbName"],
            UserID = ConfigurationManager.AppSettings["DbUser"],
            Password = ConfigurationManager.AppSettings["DbPassword"],
            IntegratedSecurity = false,
            Encrypt = true, // Habilitar encriptación
            TrustServerCertificate = false
        };
        
        return builder.ConnectionString;
    }
}
```

-----

### Protección Contra Ataques Comunes

#### Cross-Site Scripting (XSS)

```csharp
// En aplicaciones web
[ValidateInput(true)]
public ActionResult SaveComment(string comment) {
    // Codificar salida
    ViewBag.SafeComment = System.Web.HttpUtility.HtmlEncode(comment);
    return View();
}
```

#### Inyección de Dependencias

```csharp
// Usar inyección de dependencias para mejor seguridad
public class StudentService {
    private readonly IStudentRepository _repository;
    
    public StudentService(IStudentRepository repository) {
        _repository = repository; // Inyectado, no instanciado
    }
}
```

#### Rate Limiting

```csharp
[RateLimit(10, 60)] // 10 requests por minuto
public ActionResult EnrollStudent(int courseId) {
    // Lógica de matriculación
}
```

-----

### Configuración Segura

#### appsettings.json Seguro

```json
{
  "ConnectionStrings": {
    "SchoolDb": "Server=.;Database=SchoolDB;Integrated Security=true;Encrypt=true;"
  },
  "Security": {
    "JwtSecret": "[SECRET_KEY_FROM_ENVIRONMENT]",
    "PasswordPolicy": {
      "MinLength": 8,
      "RequireDigit": true,
      "RequireUppercase": true,
      "MaxFailedAttempts": 5
    }
  }
}
```

#### Program.cs con Seguridad

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configuración de seguridad
builder.Services.AddHsts(options => {
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

builder.Services.AddAntiforgery();
builder.Services.AddCors(options => {
    options.AddPolicy("SchoolPolicy", policy => {
        policy.WithOrigins("https://school.example.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

-----

**Proyecto educativo - Solo para fines de aprendizaje**  