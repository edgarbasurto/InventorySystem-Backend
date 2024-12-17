# InventorySystem-Backend
Este proyecto es una api RESTFul que permite gestionar productos en un sistema de inventario. Está desarrollado con **.NET 8** y **SQL Server**.

---

## **Requisitos previos**
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/) *(opcional si deseas correr SQL Server en contenedor)*

---

## **Instrucciones para configurar y ejecutar el proyecto**

### **1. Configuración de la Base de Datos**

1. Crea una base de datos llamada **`InventoryDb`** utilizando SQL Server.
2. Ejecuta el script **`database.sql`** ubicado en el directorio raíz del proyecto. Esto creará las tablas necesarias y un usuario administrador por defecto.

### **2. Configuración del Backend**

1. Navega al directorio del backend:
   ```bash
   cd InventorySystem.Api
   
2. Restaura las dependencias del proyecto:
    ```bash
   dotnet restore
   
3. Aplica las migraciones (opcional, si no ejecutaste el script SQL):
    ```bash
   dotnet ef database update

4. Corre el servidor:
    ```bash
   dotnet run
   
5. El servidor estará disponible en http://localhost:5057.

