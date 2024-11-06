namespace HotelEstrellaDelMar.Models
{
    public class Habitacion
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioDiario { get; set; }
        public bool EstaDisponible { get; set; }

        public Habitacion(int numero, string tipo, int capacidad, decimal precioDiario)
        {
            Numero = numero;
            Tipo = tipo;
            Capacidad = capacidad;
            PrecioDiario = precioDiario;
            EstaDisponible = true;
        }

        public void CambiarDisponibilidad(bool disponible)
        {
            EstaDisponible = disponible;
        }
    }
}
