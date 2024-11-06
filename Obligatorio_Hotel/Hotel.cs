using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelEstrellaDelMar.Models
{
    public class Hotel
    {
        public List<Habitacion> Habitaciones { get; set; } = new();
        public List<Huesped> Huespedes { get; set; } = new();
        public List<Reserva> Reservas { get; set; } = new();
        public List<Pago> Pagos { get; set; } = new();
        public Huesped? UsuarioActual { get; private set; }

        public Hotel()
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            GenerarHabitacionesAleatorias(20);

            var huesped1 = new Huesped(1, "Julian", "Silvera", "julianlenna@gmail.com", "juli123", "¿Cuál es tu color favorito?", "Azul");
            var huesped2 = new Huesped(2, "Carlos", "Rodriguez", "crodriguez@ctc.edu.uy", "carlos123", "¿Cómo se llama tu primera mascota?", "Rocky");
            var huesped3 = new Huesped(3, "Maria", "Gonzalez", "mariagonzalez@gmail.com", "maria123", "¿En qué ciudad naciste?", "Montevideo");

            Huespedes.Add(huesped1);
            Huespedes.Add(huesped2);
            Huespedes.Add(huesped3);

            CrearReservasSimuladas(new List<Huesped> { huesped1, huesped2, huesped3 });
        }

        private void CrearReservasSimuladas(List<Huesped> huespedesSimulados)
        {
            Random random = new Random();
            var habitacionesDisponibles = Habitaciones.Where(h => h.EstaDisponible).OrderBy(h => random.Next()).Take(3).ToList();

            foreach (var habitacion in habitacionesDisponibles)
            {
                var huesped = huespedesSimulados[random.Next(huespedesSimulados.Count)];
                DateTime fechaInicio = DateTime.Today.AddDays(random.Next(1, 10));
                DateTime fechaFin = fechaInicio.AddDays(random.Next(1, 5));

                var reserva = new Reserva(Reservas.Count + 1, habitacion.Numero, huesped, fechaInicio, fechaFin);
                Reservas.Add(reserva);
                habitacion.EstaDisponible = false;
                reserva.MontoTotal = CalcularCostoReserva(reserva);
            }
        }

        private void GenerarHabitacionesAleatorias(int cantidad)
        {
            Random random = new Random();
            string[] tipos = { "Simple", "Doble", "Suite" };
            int[] capacidades = { 1, 2, 4 };
            int[] preciosBase = { 100, 150, 200 };

            int habitacionesPorTipo = cantidad / tipos.Length;
            int idHabitacion = 1;

            for (int tipoIndex = 0; tipoIndex < tipos.Length; tipoIndex++)
            {
                string tipo = tipos[tipoIndex];
                int capacidad = capacidades[tipoIndex];
                int precioBase = preciosBase[tipoIndex];

                for (int i = 0; i < habitacionesPorTipo; i++)
                {
                    int precio = precioBase + random.Next(0, 21);
                    Habitaciones.Add(new Habitacion(idHabitacion++, tipo, capacidad, precio));
                }
            }
        }

        public bool IniciarSesion(string email, string contraseña)
        {
            var usuario = Huespedes.FirstOrDefault(h => h.Email == email && h.Contraseña == contraseña);
            if (usuario != null)
            {
                UsuarioActual = usuario;
                return true;
            }
            return false;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public void MostrarInformacionHotel()
        {
            Console.WriteLine("--- Información del Hotel ---");
            Console.WriteLine("Ubicación: Ciudad, País, Dirección");
            Console.WriteLine("Categoría: 5 estrellas");
            Console.WriteLine("Servicios ofrecidos:");
            Console.WriteLine("- Parking privado");
            Console.WriteLine("- Piscina al aire libre");
            Console.WriteLine("- Gimnasio");
            Console.WriteLine("- Restaurante");
            Console.WriteLine("- Servicio a la habitación 24 horas");
            Console.WriteLine("- Wi-Fi gratuito");
            Console.WriteLine("Servicios adicionales con costo extra: Spa, lavandería, tours guiados");
            Console.WriteLine();
        }

        public void ListarHabitacionesDisponibles()
        {
            var habitacionesDisponibles = Habitaciones.Where(h => h.EstaDisponible).ToList();
            if (habitacionesDisponibles.Any())
            {
                Console.WriteLine("Habitaciones disponibles:");
                foreach (var habitacion in habitacionesDisponibles)
                {
                    Console.WriteLine($"Número: {habitacion.Numero}, Tipo: {habitacion.Tipo}, Capacidad: {habitacion.Capacidad}, Precio: ${habitacion.PrecioDiario}/noche");
                }
            }
            else
            {
                Console.WriteLine("No hay habitaciones disponibles en este momento.");
            }
        }

        public void RealizarReserva(int numeroHabitacion, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin > fechaInicio && (fechaFin - fechaInicio).TotalDays <= 30 && fechaInicio >= DateTime.Today)
            {
                var habitacion = Habitaciones.FirstOrDefault(h => h.Numero == numeroHabitacion && h.EstaDisponible);
                if (habitacion != null)
                {
                    var nuevaReserva = new Reserva(
                        Reservas.Count + 1,
                        habitacion.Numero,
                        UsuarioActual!,
                        fechaInicio,
                        fechaFin
                    );
                    Reservas.Add(nuevaReserva);
                    habitacion.EstaDisponible = false;
                    Console.WriteLine($"Reserva realizada para la habitación {habitacion.Numero} desde {fechaInicio.ToShortDateString()} hasta {fechaFin.ToShortDateString()}.");
                }
                else
                {
                    Console.WriteLine("La habitación no está disponible.");
                }
            }
            else
            {
                Console.WriteLine("Las fechas de reserva no son válidas. Deben ser entre hoy y un máximo de 30 días.");
            }
        }

        public void ModificarReserva(int numeroHabitacionReservada, int? nuevoNumeroHabitacion, DateTime? nuevaFechaInicio, DateTime? nuevaFechaFin)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacionReservada && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                if (nuevoNumeroHabitacion.HasValue && nuevoNumeroHabitacion != numeroHabitacionReservada)
                {
                    var nuevaHabitacion = Habitaciones.FirstOrDefault(h => h.Numero == nuevoNumeroHabitacion && h.EstaDisponible);
                    if (nuevaHabitacion != null)
                    {
                        reserva.CambiarHabitacion(nuevaHabitacion);
                    }
                    else
                    {
                        Console.WriteLine("La nueva habitación no está disponible.");
                        return;
                    }
                }

                if (nuevaFechaInicio.HasValue && nuevaFechaFin.HasValue && nuevaFechaFin > nuevaFechaInicio)
                {
                    reserva.CambiarFechas(nuevaFechaInicio.Value, nuevaFechaFin.Value);
                }
                else if (nuevaFechaInicio.HasValue || nuevaFechaFin.HasValue)
                {
                    Console.WriteLine("Las fechas proporcionadas no son válidas.");
                }
            }
            else
            {
                Console.WriteLine("No se encontró una reserva para el número de habitación especificado.");
            }
        }

        public void CancelarReserva(int numeroReserva)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroReserva == numeroReserva && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                Habitacion habitacion = Habitaciones.First(h => h.Numero == reserva.NumeroHabitacion);
                habitacion.EstaDisponible = true;
                Reservas.Remove(reserva);
                Console.WriteLine("Reserva cancelada exitosamente.");
            }
            else
            {
                Console.WriteLine("Reserva no encontrada.");
            }
        }

        public void VerMisReservas()
        {
            var misReservas = Reservas.Where(r => r.Huesped.Id == UsuarioActual?.Id).ToList();
            if (misReservas.Any())
            {
                Console.WriteLine("--- Mis Reservas ---");
                foreach (var reserva in misReservas)
                {
                    Console.WriteLine(reserva.ToString());
                }
            }
            else
            {
                Console.WriteLine("No tienes reservas realizadas.");
            }
        }

        public void CheckIn(int numeroReserva)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroReserva == numeroReserva && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null && !reserva.CheckInRealizado)
            {
                reserva.CheckInRealizado = true;
                Console.WriteLine("Check-in realizado con éxito.");
            }
            else
            {
                Console.WriteLine(reserva == null ? "Reserva no encontrada." : "El check-in ya fue realizado para esta reserva.");
            }
        }

        public void RealizarPagoPorHabitacion(int numeroHabitacion)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                if (reserva.EstaPagada)
                {
                    Console.WriteLine("La reserva ya ha sido pagada.");
                }
                else
                {
                    reserva.MontoTotal = CalcularCostoReserva(reserva);
                    Console.WriteLine("Seleccione el método de pago:");
                    Console.WriteLine("1. Efectivo");
                    Console.WriteLine("2. Tarjeta");
                    Console.Write("Opción: ");
                    string opcionPago = Console.ReadLine();

                    switch (opcionPago)
                    {
                        case "1":
                            reserva.TipoPago = "Efectivo";
                            ConfirmarPago(reserva);
                            break;
                        case "2":
                            Console.Write("Confirme el pago con tarjeta (S/N): ");
                            string confirmacion = Console.ReadLine();
                            if (confirmacion?.ToUpper() == "S")
                            {
                                reserva.TipoPago = "Tarjeta";
                                ConfirmarPago(reserva);
                            }
                            else
                            {
                                Console.WriteLine("Pago con tarjeta cancelado.");
                            }
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Reserva no encontrada para la habitación especificada.");
            }
        }

        private void ConfirmarPago(Reserva reserva)
        {
            reserva.EstaPagada = true;
            Pago nuevoPago = new Pago(Pagos.Count + 1, reserva.NumeroReserva, reserva.MontoTotal, DateTime.Now);
            Pagos.Add(nuevoPago);
            Console.WriteLine("Pago realizado con éxito.");
            reserva.GenerarComprobante();
        }

        public decimal CalcularCostoReserva(Reserva reserva)
        {
            var habitacion = Habitaciones.FirstOrDefault(h => h.Numero == reserva.NumeroHabitacion);
            return habitacion != null ? habitacion.PrecioDiario * (reserva.FechaFin - reserva.FechaInicio).Days : 0;
        }

        public void GenerarComprobante(int numeroReserva)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroReserva == numeroReserva && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                reserva.GenerarComprobante();
            }
            else
            {
                Console.WriteLine("Reserva no encontrada.");
            }
        }

        public void RegistrarHuesped(Huesped nuevoHuesped)
        {
            Huespedes.Add(nuevoHuesped);
            Console.WriteLine("Huésped registrado exitosamente.");
        }

        public void RecuperarContraseña(string email)
        {
            var huesped = Huespedes.FirstOrDefault(h => h.Email == email);

            if (huesped != null)
            {
                Console.WriteLine($"Pregunta de seguridad: {huesped.PreguntaSeguridad}");
                Console.Write("Respuesta: ");
                string respuesta = Console.ReadLine() ?? "";

                if (huesped.VerificarRespuestaSeguridad(respuesta))
                {
                    string nuevaContraseña = GenerarContraseñaTemporal();
                    huesped.Contraseña = nuevaContraseña;
                    Console.WriteLine($"Contraseña temporal: {nuevaContraseña}");
                    Console.WriteLine("Por favor, inicia sesión y cambia tu contraseña.");
                }
                else
                {
                    Console.WriteLine("Respuesta incorrecta. No se puede recuperar la contraseña.");
                }
            }
            else
            {
                Console.WriteLine("No se encontró un usuario con ese correo electrónico.");
            }
        }

        public void CancelarReservaPorHabitacion(int numeroHabitacion)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                Habitacion habitacion = Habitaciones.First(h => h.Numero == reserva.NumeroHabitacion);
                habitacion.EstaDisponible = true;
                Reservas.Remove(reserva);
                Console.WriteLine("Reserva cancelada exitosamente.");
            }
            else
            {
                Console.WriteLine("No se encontró una reserva para la habitación especificada.");
            }
        }

        public void GenerarComprobantePorHabitacion(int numeroHabitacion)
        {
            var reserva = Reservas.FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion && r.Huesped.Id == UsuarioActual?.Id);
            if (reserva != null)
            {
                reserva.GenerarComprobante();
            }
            else
            {
                Console.WriteLine("No se encontró una reserva para la habitación especificada.");
            }
        }

        public void MostrarHabitacionesMasReservadas()
        {
            var habitacionesReservadas = Reservas
                .GroupBy(r => r.NumeroHabitacion)
                .Select(g => new
                {
                    NumeroHabitacion = g.Key,
                    CantidadReservas = g.Count()
                })
                .OrderByDescending(x => x.CantidadReservas)
                .ToList();

            Console.WriteLine("--- Habitaciones más Reservadas ---");
            if (habitacionesReservadas.Any())
            {
                foreach (var item in habitacionesReservadas)
                {
                    Console.WriteLine($"Habitación {item.NumeroHabitacion} - Reservas: {item.CantidadReservas}");
                }
            }
            else
            {
                Console.WriteLine("No hay reservas registradas.");
            }
        }

        private string GenerarContraseñaTemporal()
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteres, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}