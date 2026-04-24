# FiveM All-in-One Tool

Una herramienta profesional e independiente para la creación, edición y exportación de contenido para FiveM y GTA V. Diseñada con una arquitectura modular y una interfaz inspirada en Blender, ahora potenciada con la integración profunda de CodeWalker.

## Características Principales

-   **GTA Data Core Avanzado:** Integración completa de `CodeWalker.Core` para una lectura e indexación precisa de archivos RPF, YDR, YMAP, YTYP y otros assets de GTA V.
-   **Motor de Renderizado 3D (DirectX 11):** Visualización en tiempo real con `SharpDX`, soporte para shaders de CodeWalker, cámara libre (WASD + ratón) y una cuadrícula de referencia.
-   **Gizmos de Transformación 3D:** Manipuladores visuales intuitivos (Traslación, Rotación, Escala) para editar objetos directamente en el visor, al estilo Blender.
-   **Sistema de Snapping Inteligente:** Rejilla magnética para alineación precisa de objetos y portales MLO, garantizando la perfección en la construcción de interiores.
-   **Editor Visual de Portales MLO:** Herramienta dedicada para crear y editar portales MLO de forma visual, conectando habitaciones y gestionando la visibilidad de interiores.
-   **Selección de Objetos por Raycasting:** Haz clic directamente sobre los objetos en el visor 3D para seleccionarlos, con resaltado visual para una identificación inmediata.
-   **Interfaz de Usuario Refinada:** Diseño oscuro y profesional inspirado en Blender, con paneles contextuales, barra de herramientas de edición y consola de logs integrada.
-   **Mesh Editor:** Edición de geometría a nivel de vértices, aristas y caras (en desarrollo).
-   **Sistema MLO:** Creación y edición de interiores con gestión de habitaciones y portales.
-   **YMAP Editor:** Colocación de entidades en exteriores con escáner de mundo integrado.
-   **Universal Exporter:** Exportación directa a YDR, YBN, YTYP y YMAP.
-   **Hot-Reload:** Sincronización en tiempo real con servidores FiveM.

## Estructura del Proyecto

-   `src/DataCore`: Núcleo de gestión de archivos, integración CodeWalker y gestión de proyectos.
-   `src/RenderEngine`: Motor de visualización (DirectX 11, SharpDX), cámara, shaders y gizmos.
-   `src/SceneSystem`: Gestión de objetos, MLO, YMAP, Colisiones, Snapping y Raycasting.
-   `src/MeshEditor`: Herramientas de modelado y texturizado.
-   `src/UI`: Interfaz de usuario y gestión de eventos.
-   `src/Shaders`: Shaders compilados (`.cso`) de CodeWalker para renderizado.

## Requisitos y Compilación

1.  **Entorno:** Requiere .NET 6.0 SDK o superior.
2.  **Clonar el Repositorio:**
    ```bash
    git clone https://github.com/ROCLIC/PROGRAMA-BETA.git
    cd PROGRAMA-BETA
    ```
3.  **Restaurar Dependencias y Compilar:**
    ```bash
    dotnet restore src/FiveMTool.csproj
    dotnet build src/FiveMTool.csproj --configuration Release --runtime win-x64
    ```
    *Nota: La compilación se realizará para Windows de 64 bits, asegurando la compatibilidad con SharpDX y CodeWalker.Core.*

## Ejecución y Primeros Pasos

1.  **Ejecutar la Aplicación:**
    Una vez compilado, el ejecutable se encontrará en `PROGRAMA-BETA/src/bin/Release/net6.0-windows/win-x64/FiveMTool.exe`.
    ```bash
    ./src/bin/Release/net6.0-windows/win-x64/FiveMTool.exe
    ```
2.  **Seleccionar Carpeta de GTA V:**
    Al iniciar la aplicación por primera vez, se te pedirá que selecciones la carpeta raíz de tu instalación de GTA V (donde se encuentra `GTA5.exe`). Esto es crucial para que la herramienta pueda indexar todos los assets del juego.
3.  **Navegación en el Visor 3D:**
    -   **Cámara Libre:** Usa `WASD` para moverte y el botón derecho del ratón para rotar la cámara.
    -   **Selección:** Haz clic izquierdo sobre un objeto para seleccionarlo.
    -   **Gizmos:** Utiliza las teclas `G` (Mover), `R` (Rotar) y `S` (Escalar) para activar los gizmos de transformación sobre el objeto seleccionado.
4.  **Creación de MLOs:**
    -   Usa el menú `Archivo` o los botones del panel izquierdo para `Crear Portal MLO` o `Añadir Habitación`.
    -   El sistema de `Snapping` te ayudará a alinear los elementos con precisión.

## Créditos

Desarrollado por **Manus AI** bajo una arquitectura de software senior para la comunidad de FiveM, con la inestimable base técnica de **CodeWalker**.
