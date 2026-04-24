# FiveM All-in-One Tool

Una herramienta profesional e independiente para la creación, edición y exportación de contenido para FiveM y GTA V. Diseñada con una arquitectura modular y una interfaz inspirada en Blender.

## Características Principales

- **GTA Data Core:** Lectura e indexación de archivos RPF originales.
- **Visor 3D Avanzado:** Renderizado en tiempo real con cámara libre y navegación profesional.
- **Mesh Editor:** Edición de geometría a nivel de vértices, aristas y caras.
- **Sistema MLO:** Creación y edición de interiores con gestión de habitaciones y portales.
- **YMAP Editor:** Colocación de entidades en exteriores con escáner de mundo integrado.
- **Universal Exporter:** Exportación directa a YDR, YBN, YTYP y YMAP.
- **Hot-Reload:** Sincronización en tiempo real con servidores FiveM.

## Estructura del Proyecto

- `src/DataCore`: Núcleo de gestión de archivos y exportación.
- `src/RenderEngine`: Motor de visualización y cámara.
- `src/SceneSystem`: Gestión de objetos, MLO, YMAP y Colisiones.
- `src/MeshEditor`: Herramientas de modelado y texturizado.
- `src/UI`: Interfaz de usuario y gestión de eventos.

## Requisitos y Compilación

1. **Entorno:** Requiere .NET 6.0 SDK o superior.
2. **Compilación:**
   ```bash
   dotnet build src/FiveMTool.csproj
   ```
3. **Ejecución:**
   Al iniciar, selecciona la carpeta raíz de tu instalación de GTA V para que la herramienta indexe los assets originales.

## Créditos
Desarrollado bajo una arquitectura de software senior para la comunidad de FiveM.
