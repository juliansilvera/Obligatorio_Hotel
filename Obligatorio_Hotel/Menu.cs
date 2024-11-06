using System.Globalization;

namespace HotelEstrellaDelMar.Models
{
    class Menu
    {
        private Hotel hotel;

        public Menu(Hotel hotel)
        {
            this.hotel = hotel;
        }

        public void MenuPrincipal()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("--- Hotel Estrella del Mar ---");
                Console.WriteLine("1. Mostrar información del hotel");
                Console.WriteLine("2. Registrar usuario");
                Console.WriteLine("3. Iniciar sesión");
                Console.WriteLine("4. Olvidé mi contraseña");
                Console.WriteLine("5. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        MostrarInformacionHotel();
                        PausarConsola();
                        break;
                    case "2":
                        RegistrarUsuario();
                        break;
                    case "3":
                        if (IniciarSesion())
                        {
                            MenuUsuario();
                        }
                        break;
                    case "4":
                        RecuperarContraseña();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        PausarConsola();
                        break;
                }
            }
        }

        private void MostrarInformacionHotel()
        {
            Console.Clear();
            Console.WriteLine("--- Información del Hotel ---");
            Console.WriteLine("Ubicación: Punta del Este, Uruguay");
            Console.WriteLine("Dirección: Parada 4 Playa Mansa, Rb. Dr. Claudio Williman, 20100");
            Console.WriteLine("Categoría: 5 estrellas");
            Console.WriteLine("Servicios ofrecidos:");
            Console.WriteLine("- Parking privado");
            Console.WriteLine("- Piscina al aire libre");
            Console.WriteLine("- Gimnasio");
            Console.WriteLine("- Restaurante");
            Console.WriteLine("- Servicio a la habitación 24 horas");
            Console.WriteLine("- Wi-Fi gratuito");
            Console.WriteLine("Servicios adicionales con costo extra:");
            Console.WriteLine("- Spa");
            Console.WriteLine("- Lavandería");
            Console.WriteLine("- Tours guiados");
        }

        private bool IniciarSesion()
        {
            Console.Clear();
            Console.WriteLine("--- Inicio de Sesión ---");
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            string contraseña = LeerContraseña();

            if (hotel.IniciarSesion(email, contraseña))
            {
                Console.WriteLine($"Bienvenido/a, {hotel.UsuarioActual?.Nombre}.");
                PausarConsola();
                return true;
            }
            else
            {
                Console.WriteLine("Error: Las credenciales no coinciden. Inténtalo de nuevo.");
                PausarConsola();
                return false;
            }
        }

        private void MenuUsuario()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("--- Menú de Usuario ---");
                Console.WriteLine("1. Listar habitaciones disponibles");
                Console.WriteLine("2. Realizar reserva");
                Console.WriteLine("3. Modificar reserva");
                Console.WriteLine("4. Cancelar reserva");
                Console.WriteLine("5. Realizar pago");
                Console.WriteLine("6. Generar Comprobante");
                Console.WriteLine("7. Cambiar contraseña");
                Console.WriteLine("8. Listar huéspedes");
                Console.WriteLine("9. Ver mis reservas");
                Console.WriteLine("10. Habitaciones más reservadas");
                Console.WriteLine("11. Cerrar sesión");
                Console.WriteLine("12. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        hotel.ListarHabitacionesDisponibles();
                        PausarConsola();
                        break;
                    case "2":
                        RealizarReserva();
                        break;
                    case "3":
                        MenuModificarReserva();
                        break;
                    case "4":
                        CancelarReserva();
                        break;
                    case "5":
                        RealizarPagoPorHabitacion();
                        break;
                    case "6":
                        GenerarComprobante();
                        break;
                    case "7":
                        CambiarContraseña();
                        break;
                    case "8":
                        ListarHuespedes();
                        PausarConsola();
                        break;
                    case "9":
                        VerMisReservas();
                        PausarConsola();
                        break;
                    case "10":
                        hotel.MostrarHabitacionesMasReservadas();
                        PausarConsola();
                        break;
                    case "11":
                        hotel.CerrarSesion();
                        Console.WriteLine("Sesión cerrada.");
                        PausarConsola();
                        salir = true;
                        break;
                    case "12":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        PausarConsola();
                        break;
                }
            }
        }

        private void ListarHuespedes()
        {
            var huespedesOrdenados = hotel.Huespedes.OrderBy(h => h.Nombre).ThenBy(h => h.Apellido).ToList();
            if (huespedesOrdenados.Any())
            {
                Console.WriteLine("--- Lista de Huéspedes ---");
                foreach (var huesped in huespedesOrdenados)
                {
                    Console.WriteLine($"ID: {huesped.Id}, Nombre: {huesped.Nombre} {huesped.Apellido}, Email: {huesped.Email}");
                }
            }
            else
            {
                Console.WriteLine("No hay huéspedes registrados.");
            }
        }

        private void VerMisReservas()
        {
            var misReservas = hotel.Reservas.Where(r => r.Huesped.Id == hotel.UsuarioActual?.Id).ToList();
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
                Console.WriteLine("No tienes reservas registradas.");
            }
        }

        private void MenuModificarReserva()
        {
            Console.Clear();
            Console.Write("Ingrese el número de la habitación reservada para modificar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHabitacionReservada))
            {
                bool volver = false;
                while (!volver)
                {
                    Console.Clear();
                    Console.WriteLine("--- Modificar Reserva ---");
                    Console.WriteLine("1. Cambiar Fecha");
                    Console.WriteLine("2. Cambiar Habitación");
                    Console.WriteLine("3. Volver al Menú Principal");
                    Console.Write("Seleccione una opción: ");
                    string opcion = Console.ReadLine() ?? "";

                    switch (opcion)
                    {
                        case "1":
                            CambiarFechaReserva(numeroHabitacionReservada);
                            PausarConsola();
                            break;
                        case "2":
                            CambiarHabitacionReserva(numeroHabitacionReservada);
                            PausarConsola();
                            break;
                        case "3":
                            volver = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida, intenta de nuevo.");
                            PausarConsola();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Número de habitación no válido.");
                PausarConsola();
            }
        }

        private void CambiarFechaReserva(int numeroHabitacionReservada)
        {
            Console.WriteLine("Modificar Fechas de Reserva:");
            DateTime nuevaFechaInicio = SolicitarFecha("Nueva fecha de inicio (dd/MM/yyyy): ");
            DateTime nuevaFechaFin = SolicitarFecha("Nueva fecha de fin (dd/MM/yyyy): ");

            hotel.ModificarReserva(numeroHabitacionReservada, null, nuevaFechaInicio, nuevaFechaFin);
        }

        private void CambiarHabitacionReserva(int numeroHabitacionReservada)
        {
            Console.WriteLine("Modificar Habitación de Reserva:");
            Console.Write("Nuevo número de habitación: ");
            if (int.TryParse(Console.ReadLine(), out int nuevoNumeroHabitacion))
            {
                hotel.ModificarReserva(numeroHabitacionReservada, nuevoNumeroHabitacion, null, null);
            }
            else
            {
                Console.WriteLine("Número de habitación no válido.");
            }
        }

        private void CambiarContraseña()
        {
            Console.Clear();
            Console.WriteLine("--- Cambiar Contraseña ---");
            Console.Write("Ingresa la nueva contraseña: ");
            string nuevaContraseña = LeerContraseña();
            Console.Write("Confirma la nueva contraseña: ");
            string confirmarContraseña = LeerContraseña();

            if (nuevaContraseña == confirmarContraseña)
            {
                hotel.UsuarioActual!.Contraseña = nuevaContraseña;
                Console.WriteLine("Contraseña cambiada exitosamente.");
            }
            else
            {
                Console.WriteLine("Las contraseñas no coinciden. Intenta de nuevo.");
            }
            PausarConsola();
        }

        private void RegistrarUsuario()
        {
            Console.Clear();
            Console.WriteLine("--- Registro de Usuario ---");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email;

            // Validar que el email contenga "@"
            while (true)
            {
                email = Console.ReadLine() ?? "";
                if (email.Contains("@"))
                {
                    break;
                }
                Console.WriteLine("El email debe contener '@'. Intenta de nuevo:");
            }

            string contraseña;
            // Validar que la contraseña tenga un máximo de 20 caracteres
            while (true)
            {
                contraseña = LeerContraseña();
                if (contraseña.Length <= 20)
                {
                    break;
                }
                Console.WriteLine("La contraseña no puede exceder los 20 caracteres. Intenta de nuevo:");
            }

            Console.Write("Pregunta de seguridad (por ejemplo, ¿Cuál es el nombre de tu primera mascota?): ");
            string preguntaSeguridad = Console.ReadLine() ?? "";
            Console.Write("Respuesta de seguridad: ");
            string respuestaSeguridad = Console.ReadLine() ?? "";

            if (hotel.Huespedes.Any(h => h.Email == email))
            {
                Console.WriteLine("Este correo electrónico ya está registrado. Intenta con otro.");
            }
            else
            {
                Huesped nuevoHuesped = new Huesped(hotel.Huespedes.Count + 1, nombre, apellido, email, contraseña, preguntaSeguridad, respuestaSeguridad);
                hotel.RegistrarHuesped(nuevoHuesped);
                Console.WriteLine("Registro exitoso. Ahora puedes iniciar sesión.");
            }
            PausarConsola();
        }

        private void RecuperarContraseña()
        {
            Console.Clear();
            Console.WriteLine("--- Recuperación de Contraseña ---");
            Console.Write("Ingresa tu correo electrónico registrado: ");
            string email = Console.ReadLine() ?? "";

            hotel.RecuperarContraseña(email);
            PausarConsola();
        }

        private void RealizarReserva()
        {
            Console.Write("Ingrese el número de la habitación: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHabitacion))
            {
                DateTime fechaInicio = SolicitarFecha("Fecha de inicio (dd/MM/yyyy): ");
                DateTime fechaFin = SolicitarFecha("Fecha de fin (dd/MM/yyyy): ");
                hotel.RealizarReserva(numeroHabitacion, fechaInicio, fechaFin);
            }
            PausarConsola();
        }

        private void CancelarReserva()
        {
            Console.Write("Ingrese el número de la habitación a cancelar: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHabitacion))
            {
                hotel.CancelarReservaPorHabitacion(numeroHabitacion);
            }
            else
            {
                Console.WriteLine("Número de habitación no válido.");
            }
            PausarConsola();
        }


        private void RealizarPagoPorHabitacion()
        {
            Console.Write("Ingrese el número de la habitación para realizar el pago: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHabitacion))
            {
                hotel.RealizarPagoPorHabitacion(numeroHabitacion);
            }
            PausarConsola();
        }

        private void GenerarComprobante()
        {
            Console.Write("Ingrese el número de la habitación para generar el comprobante: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHabitacion))
            {
                hotel.GenerarComprobantePorHabitacion(numeroHabitacion);
            }
            PausarConsola();
        }

        private DateTime SolicitarFecha(string mensaje)
        {
            DateTime fecha;
            Console.Write(mensaje);
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out fecha))
            {
                Console.WriteLine("Fecha no válida. Intenta nuevamente (formato: dd/MM/yyyy): ");
            }
            return fecha;
        }

        private string LeerContraseña()
        {
            string contraseña = "";
            ConsoleKeyInfo tecla;

            Console.Write("Contraseña: ");
            do
            {
                tecla = Console.ReadKey(intercept: true);
                if (tecla.Key == ConsoleKey.Backspace && contraseña.Length > 0)
                {
                    contraseña = contraseña[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    contraseña += tecla.KeyChar;
                    Console.Write("*");
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return contraseña;
        }

        private void PausarConsola()
        {
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}