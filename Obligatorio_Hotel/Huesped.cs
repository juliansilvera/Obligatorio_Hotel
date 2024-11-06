namespace HotelEstrellaDelMar.Models
{
    public class Huesped
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaSeguridad { get; set; }

        public Huesped(int id, string nombre, string apellido, string email, string contraseña, string preguntaSeguridad, string respuestaSeguridad)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contraseña = contraseña;
            PreguntaSeguridad = preguntaSeguridad;
            RespuestaSeguridad = respuestaSeguridad;
        }

        public bool VerificarRespuestaSeguridad(string respuesta)
        {
            return RespuestaSeguridad.Equals(respuesta, StringComparison.OrdinalIgnoreCase);
        }
    }
}
