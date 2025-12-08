# 🧮 Sistema de Facturación Electrónica – SRI Ecuador

> Proyecto académico desarrollado en .NET Core con arquitectura Onion y Blazor, con integración a la API del SRI (ambiente de pruebas).

---

## 🚀 Descripción

Este sistema permite gestionar el proceso de facturación electrónica de manera automatizada, incluyendo el registro de clientes, control de stock, generación de facturas y envío de comprobantes al **Servicio de Rentas Internas (SRI)** del Ecuador mediante su API de pruebas.

El proyecto fue desarrollado bajo la metodología **Scrum**, estructurado en **dos sprints**, con separación clara de responsabilidades entre backend y frontend.

---

## 🧩 Tecnologías utilizadas

- **Backend:** .NET Core 8 (API REST)  
- **Frontend:** Blazor Server  
- **Base de Datos:** SQL Server (Entity Framework Core)  
- **Arquitectura:** Onion Architecture  
- **Integración:** API del SRI (entorno sandbox)  
- **Control de versiones:** Git + GitHub

---

## ⚙️ Funcionalidades principales

- 🔐 **Login con roles (Administrador / Empleado)**
- 👥 **Gestión de clientes**
- 📦 **Gestión de productos y stock (FIFO básico)**
- 🧾 **Generación de facturas electrónicas**
- 🌐 **Conexión con API del SRI (modo pruebas)**
- 📈 **Reportes de ventas**
- 🪪 **Firma electrónica de comprobantes**

---

## 🧠 Arquitectura del proyecto

```text
src/
│
├── Application/          # Casos de uso, interfaces de servicios
├── Domain/               # Entidades del dominio
├── Infrastructure/       # Persistencia, repositorios, EF Core
├── WebAPI/               # Endpoints y controladores .NET Core
└── BlazorApp/            # Frontend en Blazor Server
```
## 🚢 Despliegue / Instalación
Requisitos previos
- **.NET SDK 9.x**
- **Node.js 18+ y npm**
- **SQL Server en ejecución (si aplica para tu entorno)**

Pasos
1) Ubicate en la carpeta del proyecto

    ```cmd
    cd sistemaFacturacion
    ```
2) Instala dependencias de Node:

    ```cmd
    npm install
    ```
3) Construye los assets js/css con esbuild:

    ```cmd
    npm run vyzor
    ```
4) Reemplaza style.css por el del gist:
    - Descarga el style.css desde [aqui.](https://gist.github.com/ErickGuerron/3ce4f45b7166357c72d29c419b05e741/7740b316ff9d81090c640637f3ba746a81d983c2)
    - Sustituye tu archivo local style.css (ej.: wwwroot/assets/css/style.css; ajusta la ruta según tu estructura).
    - Importante si usas style.scss: si tu build genera style.css a partir de style.scss, copia también los cambios del gist dentro de style.scss y vuelve a ejecutar:

        ```cmd
        npm run vyzor
        ```
De lo contrario, un build futuro podría sobrescribir tu style.css.

5) Levanta la aplicación (backend/Blazor):

    ```cmd
    dotnet restore
    dotnet build
    dotnet run
    ```

## 👨‍💻 Equipo de desarrollo
- **Anthony** – Fullstack Developer
- **Mabe** – Fullstack Developer
- **Xabier** – Fullstack Developer
- **Erick** – Fullstack Developer

##🧪 Metodología de trabajo

El equipo trabaja en ciclos Scrum de dos semanas.
Cada sprint entrega un conjunto funcional de características que avanza hacia el producto final.

##🧾 Licencia

© 2025 Sistema de Facturación Electrónica – Todos los derechos reservados.
Este proyecto fue desarrollado con fines académicos.
No se permite su distribución, uso comercial o modificación fuera del ámbito autorizado por el equipo desarrollador.

## 🤝 Contribución
Ver el archivo [`CONTRIBUTING.md`](CONTRIBUTING.md) para más detalles sobre cómo colaborar en el proyecto.













