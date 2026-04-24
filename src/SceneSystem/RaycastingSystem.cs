using System;
using System.Numerics;
using System.Collections.Generic;

namespace FiveMTool.SceneSystem
{
    /// <summary>
    /// Sistema de Raycasting para la selección de objetos 3D en el visor.
    /// </summary>
    public class RaycastingSystem
    {
        /// <summary>
        /// Genera un rayo desde la posición del ratón en el espacio de pantalla hacia el espacio 3D.
        /// </summary>
        public static Ray GetRayFromMouse(Vector2 mousePos, Vector2 viewportSize, Matrix4x4 view, Matrix4x4 projection)
        {
            // Convertir coordenadas de pantalla a coordenadas normalizadas (-1 a 1)
            float x = (2.0f * mousePos.X) / viewportSize.X - 1.0f;
            float y = 1.0f - (2.0f * mousePos.Y) / viewportSize.Y;

            // Crear el rayo en el espacio de proyección
            Vector4 rayClip = new Vector4(x, y, -1.0f, 1.0f);
            
            // Transformar al espacio de vista
            Matrix4x4 invProj;
            Matrix4x4.Invert(projection, out invProj);
            Vector4 rayView = Vector4.Transform(rayClip, invProj);
            rayView = new Vector4(rayView.X, rayView.Y, -1.0f, 0.0f);

            // Transformar al espacio de mundo
            Matrix4x4 invView;
            Matrix4x4.Invert(view, out invView);
            Vector4 rayWorld = Vector4.Transform(rayView, invView);
            Vector3 direction = Vector3.Normalize(new Vector3(rayWorld.X, rayWorld.Y, rayWorld.Z));

            // El origen del rayo es la posición de la cámara (extraída de la matriz de vista invertida)
            Vector3 origin = new Vector3(invView.M41, invView.M42, invView.M43);

            return new Ray(origin, direction);
        }

        /// <summary>
        /// Comprueba la intersección entre un rayo y una caja delimitadora (AABB).
        /// </summary>
        public static bool IntersectsAABB(Ray ray, Vector3 min, Vector3 max, out float distance)
        {
            distance = 0;
            float tmin = (min.X - ray.Origin.X) / ray.Direction.X;
            float tmax = (max.X - ray.Origin.X) / ray.Direction.X;

            if (tmin > tmax) { float temp = tmin; tmin = tmax; tmax = temp; }

            float tymin = (min.Y - ray.Origin.Y) / ray.Direction.Y;
            float tymax = (max.Y - ray.Origin.Y) / ray.Direction.Y;

            if (tymin > tymax) { float temp = tymin; tymin = tymax; tymax = temp; }

            if ((tmin > tymax) || (tymin > tmax)) return false;

            if (tymin > tmin) tmin = tymin;
            if (tymax < tmax) tmax = tymax;

            float tzmin = (min.Z - ray.Origin.Z) / ray.Direction.Z;
            float tzmax = (max.Z - ray.Origin.Z) / ray.Direction.Z;

            if (tzmin > tzmax) { float temp = tzmin; tzmin = tzmax; tzmax = temp; }

            if ((tmin > tzmax) || (tzmin > tmax)) return false;

            if (tzmin > tmin) tmin = tzmin;
            if (tzmax < tmax) tmax = tzmax;

            distance = tmin;
            return true;
        }
    }

    public struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
