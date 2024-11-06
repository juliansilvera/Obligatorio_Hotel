using System;

namespace HotelEstrellaDelMar.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int NumeroReserva { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        public Pago(int id, int numeroReserva, decimal monto, DateTime fechaPago)
        {
            Id = id;
            NumeroReserva = numeroReserva;
            Monto = monto;
            FechaPago = fechaPago;
        }

        public void ActualizarMonto(decimal nuevoMonto)
        {
            if (nuevoMonto > 0)
            {
                Monto = nuevoMonto;
                Console.WriteLine("Monto actualizado.");
            }
            else
            {
                Console.WriteLine("Monto no válido.");
            }
        }

        public string ObtenerDetalles()
        {
            return $"Pago ID: {Id}, Reserva ID: {NumeroReserva}, Monto: ${Monto}, Fecha: {FechaPago.ToShortDateString()}";
        }

        public override string ToString()
        {
            return ObtenerDetalles();
        }
    }
}
