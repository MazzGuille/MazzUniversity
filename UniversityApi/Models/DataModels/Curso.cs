namespace UniversityApi.Models.DataModels
{
    public class Curso
    {
        public string Nombre { get; set; } = String.Empty;
        public string DescripcionCorta { get; set; } = String.Empty;
        public string DescripcionLarga { get; set; } = String.Empty;
        public string PublicoObjetivo { get; set; } = String.Empty;
        public string Objetivos { get; set; } = String.Empty;
        public string Requisitos { get; set; } = String.Empty;
        public enum Nivel
        {
            Básico,
            Intermedio,
            Avanzado
        }
    }
}
