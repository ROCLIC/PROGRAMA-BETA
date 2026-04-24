# FiveM All-in-One Tool

Una herramienta profesional e independiente para la creación, edición y exportación de contenido para FiveM y GTA V. Diseñada con una arquitectura modular y una interfaz inspirada en Blender, potenciada con la integración profunda de CodeWalker.

## Estado del Proyecto: Versión Beta Corregida

El proyecto ha sido recientemente sometido a una revisión técnica profunda para resolver conflictos de compilación críticos y optimizar la estructura del repositorio.

### Correcciones Recientes (Abril 2026)
- **Resolución de Conflictos de Tipos (CS0436):** Se han eliminado las redundancias entre el código fuente de `CodeWalker.Core` y referencias externas.
- **Optimización de Proyectos:** Los archivos `.csproj` han sido reconfigurados para usar `net6.0-windows` (x64) y `netstandard2.0`, eliminando dependencias de DLLs residuales y priorizando referencias de proyecto.
- **Corrección de Ambigüedades:** Se han resuelto conflictos de nombres entre `System.Drawing`, `SharpDX` y `System.Threading` (específicamente en los tipos `Color` y `Timer`).
- **Implementación de Interfaces:** Se han completado los métodos requeridos en `YMAPManager` para cumplir con la interfaz `IYMAPSystem`, manteniendo la lógica de negocio intacta.
- **Limpieza de Repositorio:** Se ha implementado un archivo `.gitignore` robusto y se han eliminado binarios residuales para evitar conflictos de caché en el compilador.

## Características Principales

- **GTA Data Core Avanzado:** Integración completa de `CodeWalker.Core` para una lectura e indexación precisa de archivos RPF, YDR, YMAP, YTYP y otros assets de GTA V.
- **Motor de Renderizado 3D (DirectX 11):** Visualización en tiempo real con `SharpDX`, soporte para shaders de CodeWalker, cámara libre (WASD + ratón) y una cuadrícula de referencia.
- **Gizmos de Transformación 3D:** Manipuladores visuales intuitivos (Traslación, Rotación, Escala) para editar objetos directamente en el visor, al estilo Blender.
- **Sistema de Snapping Inteligente:** Rejilla magnética para alineación precisa de objetos y portales MLO, garantizando la perfección en la construcción de interiores.
- **Editor Visual de Portales MLO:** Herramienta dedicada para crear y editar portales MLO de forma visual, conectando habitaciones y gestionando la visibilidad de interiores.
- **Selección de Objetos por Raycasting:** Haz clic directamente sobre los objetos en el visor 3D para seleccionarlos, con resaltado visual para una identificación inmediata.
- **Interfaz de Usuario Refinada:** Diseño oscuro y profesional inspirado en Blender (RGB 45,45,48), con paneles contextuales, barra de herramientas de edición y consola de logs integrada.
- **Universal Exporter:** Exportación directa a YDR, YBN, YTYP y YMAP.

## Estructura del Proyecto

- `src/DataCore`: Núcleo de gestión de archivos, integración CodeWalker y gestión de proyectos.
- `src/RenderEngine`: Motor de visualización (DirectX 11, SharpDX), cámara, shaders y gizmos.
- `src/SceneSystem`: Gestión de objetos, MLO, YMAP, Colisiones, Snapping y Raycasting.
- `src/MeshEditor`: Herramientas de modelado y texturizado.
- `src/UI`: Interfaz de usuario y gestión de eventos.
- `src/External/CodeWalker.Core`: Código fuente completo de CodeWalker integrado como referencia de proyecto.

## Requisitos y Compilación

1. **Entorno:** Requiere .NET 6.0 SDK o superior.
2. **Clonar el Repositorio:**
   ```bash
   git clone https://github.com/ROCLIC/PROGRAMA-BETA.git
   cd PROGRAMA-BETA
   ```
3. **Limpieza y Compilación:**
   Se recomienda realizar una limpieza antes de compilar para asegurar que no existan artefactos antiguos:
   ```bash
   dotnet clean src/FiveMTool.csproj
   dotnet build src/FiveMTool.csproj --configuration Release --runtime win-x64
   ```
   *Nota: La plataforma de destino debe ser x64 debido a las dependencias nativas de CodeWalker y SharpDX.*

## Ejecución y Primeros Pasos

1. **Ejecutar la Aplicación:**
   El ejecutable se genera en `src/bin/x64/Release/net6.0-windows/win-x64/FiveMTool.exe`.
2. **Seleccionar Carpeta de GTA V:**
   Al iniciar, selecciona la carpeta raíz de GTA V (donde está `GTA5.exe`) para que el `RpfManager` pueda indexar los archivos.
3. **Navegación:**
   - **Cámara:** `WASD` + Botón derecho del ratón.
   - **Transformación:** `G` (Mover), `R` (Rotar), `S` (Escalar).

## Créditos

Desarrollado y mantenido por **Manus AI** bajo una arquitectura de software senior para la comunidad de FiveM, utilizando la base técnica de **CodeWalker.Core**.
