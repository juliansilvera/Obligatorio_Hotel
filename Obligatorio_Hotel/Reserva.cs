using System;

namespace HotelEstrellaDelMar.Models
{
    public class Reserva
    {
        public int NumeroReserva { get; set; }
        public int NumeroHabitacion { get; set; }
        public Huesped Huesped { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EstaPagada { get; set; }
        public decimal MontoTotal { get; set; }
        public bool CheckInRealizado { get; set; }
        public string TipoPago { get; set; }

        public Huesped Huesped1
        {
            get => default;
            set
            {
            }
        }

        public Habitacion Habitacion
        {
            get => default;
            set
            {
            }
        }

        public Reserva(int numeroReserva, int numeroHabitacion, Huesped huesped, DateTime fechaInicio, DateTime fechaFin)
        {
            NumeroReserva = numeroReserva;
            NumeroHabitacion = numeroHabitacion;
            Huesped = huesped;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EstaPagada = false;
            MontoTotal = 0;
            CheckInRealizado = false;
            TipoPago = "No especificado";
        }

        public void CambiarHabitacion(Habitacion nuevaHabitacion)
        {
            NumeroHabitacion = nuevaHabitacion.Numero;
            Console.WriteLine($"Habitación cambiada a {nuevaHabitacion.Numero}.");
        }

        public void CambiarFechas(DateTime nuevaFechaInicio, DateTime nuevaFechaFin)
        {
            FechaInicio = nuevaFechaInicio;
            FechaFin = nuevaFechaFin;
            Console.WriteLine($"Fechas actualizadas: Desde {FechaInicio.ToShortDateString()} hasta {FechaFin.ToShortDateString()}.");
        }

        public void GenerarComprobante()
        {
            string comprobante = $"Comprobante de Pago:\n" +
                                 $"Reserva ID: {NumeroReserva}\n" +
                                 $"Habitación: {NumeroHabitacion}\n" +
                                 $"Desde: {FechaInicio.ToShortDateString()} Hasta: {FechaFin.ToShortDateString()}\n" +
                                 $"Estado: {(EstaPagada ? "Pagada" : "Pendiente")}\n" +
                                 $"Monto Total: ${MontoTotal}\n" +
                                 $"Tipo de Pago: {TipoPago}\n";

            Console.WriteLine(comprobante);

            try
            {
                System.IO.File.WriteAllText($"Comprobante_Reserva_{NumeroReserva}.txt", comprobante);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el comprobante: {ex.Message}");
            }
        }

        public override string ToString()
        {
            return $"Reserva: {NumeroReserva}, Habitación: {NumeroHabitacion}, Huesped: {Huesped.Nombre} {Huesped.Apellido}, " +
                   $"Desde: {FechaInicio.ToShortDateString()} Hasta: {FechaFin.ToShortDateString()}, " +
                   $"Pagada: {EstaPagada}, Check-In: {(CheckInRealizado ? "Realizado" : "Pendiente")}, " +
                   $"Monto Total: ${MontoTotal}, Tipo de Pago: {TipoPago}";
        }
    }
}
