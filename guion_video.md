# Guion de Video: Implementación de GraphQL con HotChocolate

**Duración estimada:** 4-5 minutos

---

## 1. Introducción (0:00 - 0:45)
*   **Visual:** Pantalla principal del proyecto (Frontend).
*   **Audio (Locutor):** "¡Hola! En este video vamos a explorar cómo implementamos GraphQL en nuestro sistema de gestión de productos. Veremos desde la librería utilizada en el backend hasta cómo el frontend se comunica con ella para ofrecer una experiencia dinámica y eficiente."

## 2. La Librería: HotChocolate (0:45 - 1:30)
*   **Visual:** Logotipo de HotChocolate o código del archivo `.csproj`.
*   **Audio:** "Para nuestro backend en .NET 8, hemos elegido **HotChocolate**. Es una de las librerías más robustas y populares para implementar servidores GraphQL en el ecosistema .NET. Nos permite trabajar con un enfoque 'Code-First', lo que significa que definimos nuestro esquema directamente a través de clases C#."

## 3. Instalación (1:30 - 2:00)
*   **Visual:** Terminal ejecutando el comando de instalación.
*   **Audio:** "La instalación es muy sencilla. Solo necesitamos agregar el paquete de ASP.NET Core mediante NuGet. El comando es: `dotnet add package HotChocolate.AspNetCore`. Con esto, ya tenemos todo lo necesario para empezar a configurar nuestro servidor."

## 4. Clases y Componentes Clave (2:00 - 3:30)
*   **Visual:** Fragmentos de código de `Program.cs`, `ProductQueries.cs` y `ProductMutations.cs`.
*   **Audio:** "Los componentes fundamentales se dividen en tres áreas:
    1.  **Configuración:** En `Program.cs`, registramos el servidor de GraphQL y mapeamos el endpoint usando `AddGraphQLServer()` y `MapGraphQL()`.
    2.  **Queries:** En la clase `ProductQueries`, definimos las operaciones de lectura, como `GetProducts`, que permiten al cliente solicitar exactamente los campos que necesita.
    3.  **Mutations:** En `ProductMutations`, manejamos las operaciones de escritura, como `AddProduct`, permitiendo la creación de nuevos registros de forma segura y tipada."

## 5. El Frontend: Cliente GraphQL (3:30 - 4:30)
*   **Visual:** Navegando por el código de `src/services/graphqlClient.js` y la UI del navegador.
*   **Audio:** "Nuestro frontend está construido con **Vite y React**. En lugar de usar una librería pesada, hemos implementado un cliente ligero usando **Axios**. El archivo `graphqlClient.js` centraliza todas las peticiones POST al endpoint `/graphql`, enviando las consultas y mutaciones como strings. ¡Esto nos da un control total sobre las peticiones sin añadir complejidad innecesaria!"

## 6. Cierre (4:30 - 5:00)
*   **Visual:** Pantalla con el enlace al repositorio de GitHub.
*   **Audio:** "Gracias a GraphQL y HotChocolate, hemos logrado una comunicación eficiente y escalable entre nuestro backend y frontend. Todo el código está disponible en nuestro repositorio de GitHub. ¡Hasta la próxima!"
