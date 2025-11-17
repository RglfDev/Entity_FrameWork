# 💾 Proyectos de Entity Framework: ¡El Puente entre C# y la Base de Datos!

¡Hola! Este repositorio es mi colección de experimentos y trabajos con **Entity Framework (EF)**, la herramienta mágica de Microsoft que nos permite hablar con la base de datos (BD) usando solo código C# (POO).

Aquí verás cómo pasamos de una clase a una tabla, y viceversa, utilizando los tres enfoques principales de desarrollo.

---

## 🛠️ Los Tres Caminos de EF

En estos proyectos, verás implementaciones detalladas de las tres filosofías de trabajo con Entity Framework:

### 1. 🥇 Code First: 

* **¿Qué es?** Empezamos **creando solo las clases de C#** (`Usuario`, `Producto`, etc.).
* **¿Qué se hace?** Crea la base de datos basandose en las clases desarrolladas." Usamos **Migraciones** para que EF genere automáticamente las tablas, columnas y relaciones. Es la forma mas profesional para empezar un proyecto nuevo.

### 2. 🥈 Database First:

* **¿Qué es?** Aquí, **la base de datos ya existe**.
* **¿Qué se hace?** Usamos las herramientas de EF para "escanear" la BD existente y **generar automáticamente las clases de C#** (los *modelos*) que representan esas tablas. Ideal para trabajar en sistemas legados.

### 3. 🥉 Model First:

* **¿Qué es?** Diseñamos visualmente la estructura de nuestra BD en un **archivo `.edmx`** (un diagrama).
* **¿Qué se hace?** Una vez que el diagrama está listo, EF se encarga de dos cosas:
    1. Generar el **código C#** (las clases).
    2. Generar los *scripts* necesarios para **crear la base de datos real** con las tablas del diagrama.

---

## ✨ Aspectos Clave Trabajados

En cada proyecto, se ha puesto especial interés en asegurar la calidad y funcionalidad de la capa de acceso a datos:

* **Creación y Gestión de la BD:** Implementación de contextos (`DbContext`) para conectar y manipular bases de datos SQL Server o SQLite.
* **Seeds (Datos Iniciales):** Inserción de datos de prueba (semillas) de forma automática al crear o migrar la base de datos, para que la aplicación funcione desde el primer momento.
* **Relaciones Complejas:** Manejo de relaciones 1:1, 1:N y N:M (muchos a muchos) y cómo se mapean correctamente entre C# y las tablas.
* **Consultas Eficientes:** Uso de **LINQ** (como en el otro repositorio 😉) para escribir consultas optimizadas que EF transforma en SQL limpio.

¡Muchas gracias por leer hasta el final! Echa un vistazo a los proyectos, estoy abierto a sugerencias.
