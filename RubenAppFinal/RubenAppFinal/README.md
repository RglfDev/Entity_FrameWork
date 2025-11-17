# RubenAppFinal - Gestión de Gimnasio

## Descripción del Proyecto

Este proyecto simula una base de datos de un gimnasio, en la cula se han insertado datos desde objetos mediante una semilla
, los cuales se han introducido a traves de EntityFramework Core.

Para la realización de este proyecto se ha utilizado:
- **C# para la creación de las clases, y el código de la app (consultas, configuraciones...).
- **Entity Framework Core para el mapeo de las clases y sus relaciones, así como para las migraciones necesarias para crear la BBDD e inyectar los datos en ella.
- **SQL Server para alojar la base de datos (en local).
- **LINQ para realizar la extracción de datos de la BBDD a traves de consultas.
---

## Estructura del Proyecto

- `Models/`  
  Contiene las clases que representan las entidades del gimnasio: `Socio`, `Clase`, `Entrenador` e `Inscripcion`.

- `Data/`  
  Contiene el contexto de Entity Framework Core `GimnasioContext`, donde también definimos las relaciónes mas importantes de las entidades.

- `Repositories/`  
  Implementa un **Repository genérico** `RepositoryEF<T>` para realizar operaciones CRUD de forma asíncrona.

- `Program.cs`  
  Punto de entrada de la aplicación con el menú principal y los métodos para cada ejercicio/funcionalidad.

---
---


## Instalación y creación del proyecto

1.Creacíon de un proyecto nuevo

  - Iniciaremos el nuevo proyecto eligiendo una Plantilla "Aplicación de Consola de C#" (no confundir con Consola .NET FRAMEWORK)
  - Instalaremos las siguientes librerías:
  
    - `EntityFrameWorkCore v8.0.22`
    - `EntityFramework.SQLServer v8.0.22`
    - `EntityFrameworkTools v8.0.22`
    
  - Creamos tres carpetas: `Models`, `Data` y `Repositories`.

---

2.Creación de las entidades
  - Creamos tres carpetas dentro del proyecto: 
  
    - `Models`
    - `Data`
    - `Repositories`

  - En la carpeta `Models` creamos las clases correspondientes para las entidades que necesitamos:
  
    - `Inscripcion`
    - `Entrenador`
    - `Clase`
    - `Socio`
    
  - Declaramos los atributos pertinentes de cada clase, y un ID único para cada una de ellas.
  - Ahora pasamos a introducir las claves foraneas y las variables de navegación:
  
    - `Socio-Inscripción (1,N)`: Socio contiene un ICollection de Inscripción.
    - `Clase-Inscripción (1,N)`: Clase tiene un ICollection de Inscripción.
    - `Inscripción (tabla intermedia entre socio y clase)`: Inscripción tiene variables de navegación de Socio y Clase.
    - `Entrenador-Clase (1:N)`: contiene un ICollection de Clase.
    
---    

3.Creación de GimnasioContext (el contexto de datos):

Pasamos a crear el Contexto de la base de datos, la cual hará de puente entre la app y la base de datos. Pra ello:
    - Creamos la clase `GimnasioContext` en la carpeta `Data`.
    - Esta clase heredará de `DbContext`, por lo que lo implementamos con `:DbContext` en la clase.
    - Generamos dos constructores. El primero vacío y el segundo que heredará los atributos de la clase DbContextOptions.
    - Pasamos a definir los conjuntos de datos que vamos a manejar (las entidades o clases creadas en `Models`).
    - En la definición de conjunto de datos, definimos los nombres de las tablas de la Base de datos.
    - Definimos la conexión a traves del método protegido OnConfiguring.La cadena de conexión contiene:

   - El servidor donde alojaremos la Base de datos y al que accederemos.
  - El nombre de la base de datos que vamos a crear.
      
    - Mediante la función protegida OnModelCreating vamos a definir tanto las relaciones como la semilla de datos:
    
      - Para definir las relaciones bastará con indicar a cada entidad(modelBuilder.Entity<Ejemplo>()):
      
          - Su variable de navegación(el 1 en la relación que posea) con `.HasOne()`.
          - Su coleción o MUCHOS que posea (el ICollection, su N en la relación) con .WithMany().
          - La clave Foranea que posea de la relación con .HasForeignKey().
          - Ponemos `.OnDelete(DeleteBehavior.Restrict);` para evitar que se borren registros que tambien estén en otras tablas.
        
        - Pasamos a generar la semilla, la cual insertará datos en las tablas a partir de los objetos que creemos nosotros:
        
          - Escogemos la entidad que queramos mediante `modelBuilder.Entity<Ejemplo>`.
          - Aplicamos el método `.HasData()` para poder crear objetos.
          - Creamos los objetos con los atributos de las clases que vayamos a insertar en la base de datos.
          - Generamos nuevos objetos con todas las entidades.
          
---          
          
4.Creación de la Interfáz IRepository

Esta interfáz va a actuar como un contrato que la clase RepositoryEF (la crearemos despues) va a tener que cumplir.
En ella, definiremos los métodos que la otra clase implementará (Los métodos se explican despues).
Es importante recalcar que esta interfáz va a trabajar con `Clases`, por lo que se le indica con `<T> where T : class` que solo va a trabajar con CLASES.
Se definen los métodos pertinentes (se explican en la clase RepositoryEF) y se da por finalizada.

---

5.Creación de la clase RepositoryEF

Esta clase implementa la interfáz IRepository, por lo que debemos traer todos los métodos que se definen en ella.
Aunque no se han explicado anteriormente, procedemos a explicarlos ahora. Las funciones que han sido diseñadas en la interfaz y que debe traer esta clase son:
 
  - Constructor: Inicializa el repositorio con el contexto de la base de datos y recoge el DbSet<> correspondiente a la entidad que maneja.
  - *AddAsync(T entity)*: Añade un objeto al DbSet<> o a la tabla y guarda los cambios en la base de datos.
  - *DeleteAsync(int id)*: Busca un objeto por su ID y si existe lo elimina de la BBDD.
  - *FindAsync(Expression<Func<T, bool>> predicate)*: Permite buscar objetos a traves de un filtro o condición (`Where`) con *LINQ*
  - *GetAllAsync()*: Devuelve todos los registros de la tabla correspondiente.
  - *GetByIdAsync(int id)*: Devuelve un objeto de la tabla por su ID.
  - *UpdateAsync(T entity)*: Actualiza un registro en la BBDD y guarda cambios.
  
Si quieres saber como funcionan las funciones con mayor detenimiento, puedes hacerlo abriendo el fichero `RepositoryEF.cs` de este proyecto.

---

6.Migración de datos

Antes de empezar este paso, se recomienda guardar todos los archivos y *Recompilar el proyecto* para verificar que no nos queda ningún error suelto en el código de la App.
En la consola del *Administrador de Paquetes NuGet* vamos a proceder a crear la primera migración:
  
  - *Add-Migration InitialCreate*: mediante esta linea en consola, generamos un archivo en el cual se detallan las relaciones y la estructura de la BBDD.Este archivo se genera en la carpeta *Migrations*.
  - *Update-Database*: aplica las migraciones pendientes e inyecta los datos de la semilla en la Base de Datos.

Para terminar este paso, simplemente habrá que dirigirse a la base de datos(recuerda que está alojado en nuestro servidor local), y comprobar que todo se ha generado correctamente.

---

7.Definición de Program.cs

Esta clase va a trabajar con tareas del RepositoryEF o *Task*, por lo que habrá que indicarlo en su definición.
Procedimiento:
  
  - Se crea el contexto y se permite aplicar todas las migraciones automáticamente con `MigrateAsync()`
  - Se crean repositorios genericos para cada entidad, los cuales podrán manejar operaciones CRUD.
  - Creamos un menú donde podremos permitir al usuario moverse por consola entre las diferentes opciones.
  - Realizamos las consultas pertinentes que nos pide el ejercicio con LINQ.
  - Es importante recalcar que cada función que se use en esta clase debe ser asíncrona, y cada llamada al contexto a cada repositorio debe ser con *await*.

Con esto estaria terminado el proyecto, podemos hacer comprobaciónes de tres formas:

  - A traves de consultas sencillas LINQ para que traiga datos.
  - Consultando las tablas en la Base de Datos.
  - Consultando la base de datos a traves del *Explorador de Objetos de SQL* de Visual Studio.

---
---

**Muchas gracias por leer hasta el final**.
Espero que este README te sirva de ayuda para comprender este trabajo y poder replicarlo si lo deseas.


**FIN**
