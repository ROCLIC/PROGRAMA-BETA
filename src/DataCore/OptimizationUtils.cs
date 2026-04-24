using System;
using System.Collections.Generic;
using System.IO;

namespace FiveMTool.DataCore
{
    /// <summary>
    /// Utilidades para optimización de memoria y diagnóstico de archivos.
    /// </summary>
    public static class OptimizationUtils
    {
        /// <summary>
        /// Limpia la caché de assets no utilizados para liberar memoria RAM.
        /// </summary>
        public static void GarbageCollectAssets()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Verifica la integridad de un archivo RPF antes de intentar abrirlo.
        /// </summary>
        public static bool VerifyFileIntegrity(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            
            try 
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[4];
                    fs.Read(header, 0, 4);
                    // Verificar firma "RPF7" o similar
                    return System.Text.Encoding.ASCII.GetString(header) == "RPF7";
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Calcula el nivel de detalle (LOD) óptimo basado en la complejidad de la malla.
        /// </summary>
        public static float CalculateOptimalLodDistance(int vertexCount)
        {
            if (vertexCount > 50000) return 100f;
            if (vertexCount > 10000) return 300f;
            return 1000f;
        }
    }
}
