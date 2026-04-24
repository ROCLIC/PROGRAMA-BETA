using System;
using System.Threading.Tasks;
using FiveMTool.SceneSystem;
using FiveMTool.MeshEditor;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Interfaz para el sistema de exportación universal.
    /// Convierte los datos internos a formatos nativos de GTA V / FiveM.
    /// </summary>
    public interface IUniversalExporter
    {
        /// <summary>
        /// Exporta una malla editable a formato YDR.
        /// </summary>
        Task<bool> ExportToYDR(EditableMesh mesh, string outputPath);

        /// <summary>
        /// Exporta geometrías de colisión a formato YBN.
        /// </summary>
        Task<bool> ExportToYBN(CollisionGeometry collision, string outputPath);

        /// <summary>
        /// Exporta la definición de un interior a formato YTYP.
        /// </summary>
        Task<bool> ExportToYTYP(IMLOSystem mloSystem, string outputPath);

        /// <summary>
        /// Exporta la disposición de objetos a formato YMAP.
        /// </summary>
        Task<bool> ExportToYMAP(IYMAPSystem ymapSystem, string outputPath);

        /// <summary>
        /// Genera el manifiesto de recurso para FiveM (fxmanifest.lua).
        /// </summary>
        void GenerateManifest(string resourceName, string outputPath);
    }
}
