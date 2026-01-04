### Proyecto Restaurante - Gestión de Restaurante

Este proyecto es una aplicación de consola en C# que demuestra el manejo de datos utilizando SQL Server y ADO.NET. Implementa un sistema básico de gestión de restaurante con clientes, ítems del menú, mesas, pedidos y detalles de pedidos.

-----

### Arquitectura

El proyecto sigue una arquitectura en capas:

* **Models**: Clases de entidad (Customer, MenuItem, Order, OrderItem, Table)
* **DataAccess**: Repositorios para acceso a datos con ADO.NET
* **Services**: Lógica de negocio (OrderService)
* **Program**: Interfaz de usuario de consola

-----

### Base de Datos

#### Configuración

1. Ejecutar `Database/CreateTables.sql` para crear la base de datos y tablas.
2. Ejecutar `Database/InsertSampleData.sql` para insertar datos de ejemplo.

#### Esquema

* **Clientes**: Información de clientes
* **Mesas**: Información de mesas con capacidad y ubicación
* **MenuItems**: Ítems del menú con precio y categoría
* **Pedidos**: Pedidos con cliente, mesa opcional, fecha y estado
* **DetallesPedido**: Detalles de cada pedido con ítems y cantidades

-----

### Funcionalidades

* CRUD completo para clientes, ítems del menú y mesas
* Creación de pedidos con múltiples ítems
* Gestión de estados de pedidos
* Asignación automática de precios y totales
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
string query = "SELECT * FROM Clientes WHERE ClienteID = @id";
SqlCommand cmd = new SqlCommand(query, conn);
cmd.Parameters.AddWithValue("@id", customerId);
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

### Seguridad en Aplicaciones de Restaurante - Consideraciones Críticas

#### Inyección SQL

```csharp
// VULNERABLE
string query = $"SELECT * FROM Clientes WHERE Nombre = '{userInput}'";

// SEGURO
string query = "SELECT * FROM Clientes WHERE Nombre = @nombre";
SqlCommand cmd = new SqlCommand(query, conn);
cmd.Parameters.AddWithValue("@nombre", userInput);
```

#### Validación de Entrada

```csharp
public bool ValidateOrderInput(int customerId, List<OrderItem> items) {
    // Validar que el cliente existe
    if (customerId <= 0) return false;

    // Validar ítems
    foreach (var item in items) {
        if (item.Quantity <= 0 || item.MenuItemID <= 0) return false;
    }

    return true;
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

### Buenas Prácticas de Seguridad

#### Sanitización de Entrada

```csharp
public string SanitizeInput(string input) {
    // Remover caracteres peligrosos
    input = input.Replace("'", "''");
    input = input.Replace("--", "");
    input = input.Replace(";", "");

    // Limitar longitud
    if (input.Length > 100) input = input.Substring(0, 100);

    return input;
}
```

#### Logging Seguro

```csharp
public class SecureLogger {
    public void LogOrderAction(int orderId, string action) {
        // No registrar información sensible
        string safeMessage = $"Order action: {action} for order ID {orderId}";

        // Sanitizar antes de registrar
        safeMessage = safeMessage.Replace("\n", "").Replace("\r", "");

        _logger.Info(safeMessage);
    }
}
```

#### Rate Limiting

```csharp
[RateLimit(5, 60)] // 5 orders por minuto por cliente
public ActionResult PlaceOrder(int customerId) {
    // Lógica de pedido
}
```

-----

### Protección Contra Ataques Comunes

#### Cross-Site Scripting (XSS) en Interfaces Web

```csharp
// En aplicaciones web futuras
[ValidateInput(true)]
public ActionResult ViewOrder(int orderId) {
    // Codificar salida
    ViewBag.SafeOrderDetails = System.Web.HttpUtility.HtmlEncode(orderDetails);
    return View();
}
```

#### Inyección de Dependencias

```csharp
// Usar inyección de dependencias para mejor seguridad
public class OrderService {
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository) {
        _repository = repository; // Inyectado, no instanciado
    }
}
```

#### Autenticación y Autorización

```csharp
[Authorize(Roles = "Manager,Waiter")]
public ActionResult UpdateOrderStatus(int orderId, string status) {
    // Verificar que el usuario tiene permiso para este pedido
    if (!_authorizationService.CanUpdateOrder(User.Identity.Name, orderId))
        return Forbid();

    // Lógica de negocio
}
```

-----

### Configuración Segura

#### appsettings.json Seguro

```json
{
  "ConnectionStrings": {
    "RestauranteDb": "Server=.;Database=RestauranteDB;Integrated Security=true;Encrypt=true;"
  },
  "Security": {
    "JwtSecret": "[SECRET_KEY_FROM_ENVIRONMENT]",
    "OrderPolicy": {
      "MaxItemsPerOrder": 20,
      "MaxOrderValue": 500.00,
      "RequireTableForDineIn": true
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
    options.AddPolicy("RestaurantePolicy", policy => {
        policy.WithOrigins("https://restaurante.example.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

-----

**Proyecto educativo - Solo para fines de aprendizaje**
